using Domain.Entities;
using Domain.Entities.Clinics;
using Domain.Entities.Leases;
using Domain.Interfaces;
using Domain.Services;
using Domain.ValueObjects;

namespace Application.Services
{
    /// <summary>
    /// Coordinates the lifecycle of appointments, managing the transition from temporary leases to confirmed bookings.
    /// </summary>
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<Lease> _leaseRepository;
        private readonly IRepository<Clinic> _clinicRepository;
        private readonly PractitionerLocationResolver _locationResolver;
        private readonly PractitionerAvailabilityService _availabilityService;
        private readonly IDateTimeProvider _dateTimeProvider;


        /// <summary>
        /// Initializes a new instance of the <see cref="BookingService"/> class.
        /// </summary>
        public BookingService(
            IRepository<Booking> bookingRepository,
            IRepository<Lease> leaseRepository,
            IRepository<Clinic> clinicRepository,
            PractitionerLocationResolver locationResolver,
            PractitionerAvailabilityService availabilityService,
            IDateTimeProvider dateTimeProvider)
        {
            this._bookingRepository = bookingRepository;
            this._leaseRepository = leaseRepository;
            this._clinicRepository = clinicRepository;
            this._locationResolver = locationResolver;
            this._availabilityService = availabilityService;
            this._dateTimeProvider = dateTimeProvider;
        }


        /// <summary>
        /// Attempts to reserve a time slot by validating feasibility against bookings and other active leases.
        /// Matches the 'ReserveRange' step in the system sequence diagram.
        /// </summary>
        /// <param name="practitionerId">Target practitioner identifier.</param>
        /// <param name="roomId">Target room identifier.</param>
        /// <param name="slot">Requested time interval.</param>
        /// <returns>A tuple containing the active lease ID (if successful) and the feasibility metadata.</returns>
        public async Task<(Guid? LeaseId, FeasibilityResult Feasibility)> ReserveSlotAsync(
            Guid practitionerId,
            Guid roomId,
            TimeSlot slot)
        {
            // 1. Resolve requested address.
            Address? requestedAddress = await this.GetAddressForRoomAsync(roomId);

            if (requestedAddress == null)
            {
                return (null, FeasibilityResult.Failure(null, null));
            }

            // 2. Fetch "Neighbors" from BOTH Bookings and Active Leases.
            // This prevents overlapping travel times from multiple receptionists.
            (DateTimeOffset? prevEnd, Address? prevAddr) = await this.GetEffectiveNeighborAsync(practitionerId, slot.StartDateTime, isPrior: true);
            (DateTimeOffset? nextStart, Address? nextAddr) = await this.GetEffectiveNeighborAsync(practitionerId, slot.EndDateTime, isPrior: false);

            // 3. Perform the Feasibility Check using the most restrictive neighbors found.
            FeasibilityResult result = await this._availabilityService.CheckFeasibilityAsync(
                requestedSlot: slot,
                requestedAddress: requestedAddress,
                previousEndTime: prevEnd,
                previousAddress: prevAddr,
                nextStartTime: nextStart,
                nextAddress: nextAddr);

            if (!result.IsFeasible)
            {
                return (null, result);
            }

            // 4. Create and persist the Lease (the 'StoreLease' step in SSD).
            Lease lease = Lease.Create(slot, practitionerId, roomId, this._dateTimeProvider);
            await this._leaseRepository.AddAsync(lease);

            return (lease.Id, result);
        }


        /// <summary>
        /// Finalizes a temporary lease by persisting it as a confirmed Booking record.
        /// Matches the 'ConfirmBooking' step in the system sequence diagram.
        /// </summary>
        /// <param name="leaseId">Identifier of the active reservation.</param>
        /// <param name="customerId">Customer receiving the service.</param>
        /// <param name="treatmentTypeId">Specific treatment being performed.</param>
        /// <param name="finalPrice">Calculated price for the session.</param>
        /// <returns>The created booking if successful; otherwise, null if the lease is invalid or expired.</returns>
        public async Task<Booking?> FinalizeBookingAsync(
            Guid leaseId,
            Guid customerId,
            Guid treatmentTypeId,
            Money finalPrice)
        {
            Lease? lease = await this._leaseRepository.GetByIdAsync(leaseId);

            if (lease == null || lease.IsExpired(this._dateTimeProvider.UtcNow))
            {
                return null;
            }

            Booking booking = new Booking(
                timeSlot: lease.TimeSlot,
                practitionerId: lease.PractitionerId,
                customerId: customerId,
                treatmentTypeId: treatmentTypeId,
                roomId: lease.RoomId,
                finalPrice: finalPrice);

            await this._bookingRepository.AddAsync(booking);
            await this._leaseRepository.DeleteAsync(lease.Id);

            return booking;
        }


        /// <summary>
        /// Identifies the temporally closest neighbor by comparing confirmed bookings and active leases.
        /// </summary>
        private async Task<(DateTimeOffset? Time, Address? Address)> GetEffectiveNeighborAsync(
            Guid practitionerId,
            DateTimeOffset threshold,
            bool isPrior)
        {
            Booking? booking = await this.GetBookingNeighborAsync(practitionerId, threshold, isPrior);
            Lease? lease = await this.GetLeaseNeighborAsync(practitionerId, threshold, isPrior);

            if (booking == null && lease == null)
            {
                return (null, null);
            }

            bool useLease = false;

            if (booking == null)
            {
                useLease = true;
            }
            else if (lease != null)
            {
                // Prior neighbor: select the one ending LATEST (closest to threshold).
                // Next neighbor: select the one starting EARLIEST (closest to threshold).
                useLease = isPrior
                    ? lease.TimeSlot.EndDateTime > booking.TimeSlot.EndDateTime
                    : lease.TimeSlot.StartDateTime < booking.TimeSlot.StartDateTime;
            }

            if (useLease)
            {
                return (
                    isPrior ? lease.TimeSlot.EndDateTime : lease.TimeSlot.StartDateTime,
                    await this.GetAddressForRoomAsync(lease.RoomId)
                );
            }

            return (
                isPrior ? booking.TimeSlot.EndDateTime : booking.TimeSlot.StartDateTime,
                await this.GetAddressForRoomAsync(booking.RoomId)
            );
        }


        /// <summary>
        /// Resolves the physical address associated with a specific room.
        /// </summary>
        private async Task<Address?> GetAddressForRoomAsync(Guid roomId)
        {
            IReadOnlyList<Clinic> clinics = await this._clinicRepository.GetAllAsync();
            Clinic? clinic = clinics.FirstOrDefault(c => c.Rooms.Any(r => r.Id == roomId));

            return clinic?.Address;
        }


        /// <summary>
        /// Retrieves the closest confirmed booking neighbor.
        /// </summary>
        private async Task<Booking?> GetBookingNeighborAsync(Guid id, DateTimeOffset threshold, bool isPrior)
        {
            Domain.Specifications.Specification<Booking> spec = isPrior
                ? new Domain.Specifications.PractitionerPriorBookingSpecification(id, threshold)
                : new Domain.Specifications.PractitionerNextBookingSpecification(id, threshold);

            IReadOnlyList<Booking> results = await this._bookingRepository.FindAsync(spec);

            return results.FirstOrDefault();
        }


        /// <summary>
        /// Retrieves the closest unexpired lease neighbor.
        /// </summary>
        private async Task<Lease?> GetLeaseNeighborAsync(Guid id, DateTimeOffset threshold, bool isPrior)
        {
            IReadOnlyList<Lease> allLeases = await this._leaseRepository.GetAllAsync();
            DateTimeOffset now = this._dateTimeProvider.UtcNow;

            IEnumerable<Lease> activeLeases = allLeases.Where(l => l.PractitionerId == id && !l.IsExpired(now));

            return isPrior
                ? activeLeases.Where(l => l.TimeSlot.EndDateTime <= threshold)
                              .OrderByDescending(l => l.TimeSlot.EndDateTime)
                              .FirstOrDefault()
                : activeLeases.Where(l => l.TimeSlot.StartDateTime >= threshold)
                              .OrderBy(l => l.TimeSlot.StartDateTime)
                              .FirstOrDefault();
        }
    }
}
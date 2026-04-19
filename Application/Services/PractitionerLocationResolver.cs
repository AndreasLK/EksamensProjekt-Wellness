using Domain.Entities;
using Domain.Entities.Clinics;
using Domain.Interfaces;
using Domain.Specifications;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class PractitionerLocationResolver
    {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<Clinic> _clinicRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PractitionerLocationResolver"/> class.
        /// </summary>
        /// <param name="bookingRepository">Data source for practitioner appointments.</param>
        /// <param name="clinicRepository">Data source for clinic and room locations.</param>
        public PractitionerLocationResolver(
            IRepository<Booking> bookingRepository, 
            IRepository<Clinic> clinicRepository)
        {
            this._bookingRepository = bookingRepository;
            this._clinicRepository = clinicRepository;
        }

        /// <summary>
        /// Resolves the clinic address where the practitioner was last located prior to a specific time threshold.
        /// </summary>
        /// <param name="practitionerId">The unique identifier of the practitioner.</param>
        /// <param name="searchThreshold">The temporal point (usually the new appointment's start) used to find the most recent prior booking.</param>
        /// <returns>
        /// The <see cref="Address"/> of the last known clinic location if a prior booking exists; otherwise, null.
        /// </returns>
        public async Task<Address?> GetLastKnownAddressAsync(
            Guid practitionerId, 
            DateTimeOffset searchThreshold
            )
        {
            var spec = new PractitionerPriorBookingSpecification(
                practitionerId: practitionerId,
                searchThreshold: searchThreshold);

            IReadOnlyList<Booking> results = await this._bookingRepository.FindAsync(spec);
            Booking? lastBooking = results.FirstOrDefault();

            if (lastBooking == null)
            {
                return null;
            }

            IReadOnlyList<Clinic> clinics = await this._clinicRepository.GetAllAsync();
            Clinic? clinic = clinics.FirstOrDefault(c => c.Rooms.Any(room => room.Id == lastBooking.RoomId));

            return clinic?.Address;
        }
    }
}

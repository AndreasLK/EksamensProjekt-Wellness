using Domain.ValueObjects;

namespace Domain.Entities
{
    /// <summary>
    /// Represents a confirmed appointment within the BookRight system.
    /// This entity is the permanent record created after a <see cref="Leases.Lease"/> 
    /// is successfully finalized.
    /// </summary>
    public class Booking : Entity
    {
        /// <summary>
        /// The specific time interval for the appointment.
        /// </summary>
        public TimeSlot TimeSlot { get; private set; }


        /// <summary>
        /// The unique identifier of the practitioner performing the treatment.
        /// </summary>
        public Guid PractitionerId { get; private set; }


        /// <summary>
        /// The unique identifier of the customer receiving the treatment.
        /// </summary>
        public Guid CustomerId { get; private set; }


        /// <summary>
        /// The unique identifier of the specific treatment type performed.
        /// </summary>
        public Guid TreatmentTypeId { get; private set; }


        /// <summary>
        /// The unique identifier of the room where the treatment takes place.
        /// </summary>
        public Guid RoomId { get; private set; }


        /// <summary>
        /// The final calculated price after any applicable discounts or surcharges 
        /// have been applied by the price calculator service.
        /// </summary>
        public Money FinalPrice { get; private set; }


        /// <summary>
        /// The timestamp of when this booking was officially confirmed in the system.
        /// </summary>
        public DateTimeOffset ConfirmedAt { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Booking"/> class.
        /// </summary>
        /// <param name="timeSlot">The scheduled time range.</param>
        /// <param name="practitionerId">The practitioner assigned to the booking.</param>
        /// <param name="customerId">The customer receiving the service.</param>
        /// <param name="treatmentTypeId">The type of treatment being performed.</param>
        /// <param name="roomId">The room allocated for the session.</param>
        /// <param name="finalPrice">The final amount to be charged.</param>
        /// <param name="id">Optional identifier for rehydrating existing bookings.</param>
        public Booking(
            TimeSlot timeSlot,
            Guid practitionerId,
            Guid customerId,
            Guid treatmentTypeId,
            Guid roomId,
            Money finalPrice,
            Guid? id = null) : base(id)
        {
            TimeSlot = timeSlot;
            PractitionerId = practitionerId;
            CustomerId = customerId;
            TreatmentTypeId = treatmentTypeId;
            RoomId = roomId;
            FinalPrice = finalPrice;
            ConfirmedAt = DateTimeOffset.UtcNow;
        }

    }
}

namespace Core.Domain.Entities
{
    /// <summary>
    /// Represents a temporary server-side lock on a specific time range.
    /// Used to prevent double-bookings during the appointment creation process.
    /// </summary>
    public class Lease : Entity
    {
        /// <summary>The time range being temporarily reserved.</summary>
        private TimeSlot timeSlot { get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Lease"/> class.
        /// </summary>
        /// <param name="timeSlot">The slot to reserve on the server.</param>
        Lease(TimeSlot timeSlot) : base()
        {
            this.timeSlot = timeSlot;
        }
    }
}

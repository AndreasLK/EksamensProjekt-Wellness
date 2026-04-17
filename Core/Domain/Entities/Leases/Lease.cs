namespace Core.Domain.Entities.Leases
{
    /// <summary>
    /// Represents a temporary server-side lock on specific clinic resources.
    /// This entity acts as a concurrency guard to prevent double-bookings 
    /// while a receptionist is finalizing appointment details.
    /// </summary>
    public class Lease : Entity
    {
        /// <summary>The time range being temporarily reserved.</summary>
        public TimeSlot TimeSlot { get; private set; }


        /// <summary>
        /// The unique identifier of the practitioner whose time is being locked.
        /// </summary>
        public Guid PracticionerId { get; private set; }


        /// <summary>
        /// The unique identifier of the clinic room being locked for this session.
        /// </summary>
        public Guid RoomId { get; private set; }


        /// <summary>
        /// The exact point in time when this lease expires, including timezone offset.
        /// </summary>
        public DateTimeOffset ExpiryTime { get; private set; }


        /// <summary>
        /// Gets a value indicating whether the current UTC time has passed the <see cref="ExpiryTime"/>.
        /// </summary>
        public bool IsExpired => DateTimeOffset.UtcNow > ExpiryTime;


        /// <summary>
        /// Initializes a new instance of the <see cref="Lease"/> class with a specific identity.
        /// Typically used when creating a new lease or rehydrating one from persistence.
        /// </summary>
        /// <param name="timeSlot">The requested time interval to reserve.</param>
        /// <param name="PracticionerId">The ID of the practitioner to lock.</param>
        /// <param name="roomId">The ID of the room to lock.</param>
        /// <param name="expiryTime">The server-calculated expiration timestamp.</param>
        /// <param name="id">The unique identifier for this entity instance.</param>
        Lease(
            TimeSlot timeSlot,
            Guid PracticionerId,
            Guid roomId,
            DateTimeOffset expiryTime,
            Guid id) : base(id)
        {
            this.TimeSlot = timeSlot;
            this.PracticionerId = PracticionerId;
            this.RoomId = roomId;
            this.ExpiryTime = expiryTime;
        }
    }
}

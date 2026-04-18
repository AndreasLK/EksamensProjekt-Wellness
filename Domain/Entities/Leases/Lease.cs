using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Entities.Leases
{
    /// <summary>
    /// Represents a temporary server-side lock on specific clinic resources.
    /// This entity acts as a concurrency guard to prevent double-bookings 
    /// while a receptionist is finalizing appointment details.
    /// </summary>
    public class Lease : Entity
    {
        private static readonly TimeSpan DEFAULT_LEASE_DURATION = TimeSpan.FromSeconds(30);

        /// <summary>The time range being temporarily reserved.</summary>
        public TimeSlot TimeSlot { get; private set; }


        /// <summary>
        /// The unique identifier of the practitioner whose time is being locked.
        /// </summary>
        public Guid PractitionerId { get; private set; }


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
        public bool IsExpired(DateTimeOffset currentDateTimeOffset) => currentDateTimeOffset > ExpiryTime;


        /// <summary>
        /// Extends the lease to a new expiration point, typically used during a heartbeat process.
        /// </summary>
        /// <param name="newExpiryTime">The new timestamp for when the lease should expire.</param>
        /// <remarks>
        /// The new expiry time must be strictly after the current <see cref="ExpiryTime"/> to be valid.
        /// </remarks>
        public void Renew(DateTimeOffset newExpiryTime)
        {
            if (newExpiryTime > ExpiryTime)
            {
                ExpiryTime = newExpiryTime;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Lease"/> class with a specific identity.
        /// Typically used when creating a new lease or rehydrating one from persistence.
        /// </summary>
        /// <param name="timeSlot">The requested time interval to reserve.</param>
        /// <param name="practitionerId">The ID of the practitioner to lock.</param>
        /// <param name="roomId">The ID of the room to lock.</param>
        /// <param name="id">The unique identifier for this entity instance.</param>
        public static Lease Create(
            TimeSlot timeSlot,
            Guid practitionerId,
            Guid roomId,
            IDateTimeProvider timeProvider,
            Guid? id = null)
        {
            DateTimeOffset expiry = timeProvider.UtcNow.Add(DEFAULT_LEASE_DURATION);

            return new Lease(
                timeSlot: timeSlot,
                practitionerId: practitionerId,
                roomId: roomId,
                expiryTime: expiry,
                id: id
                );

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Lease"/> class.
        /// Private constructor ensures the <see cref="Create"/> factory method is used for new instances.
        /// </summary>
        private Lease(
            TimeSlot timeSlot,
            Guid practitionerId,
            Guid roomId,
            DateTimeOffset expiryTime,
            Guid? id) : base(id)
        {
            this.TimeSlot = timeSlot;
            this.PractitionerId = practitionerId;
            this.RoomId = roomId;
            this.ExpiryTime = expiryTime;
        }
    }
}

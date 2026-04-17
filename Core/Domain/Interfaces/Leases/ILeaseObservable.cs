using Core.Domain.Entities.Leases;

namespace Core.Domain.Interfaces.Leases
{
    /// <summary>
    /// Reactive events used to update the UI state based on server broadcasts.
    /// </summary>
    public interface ILeaseObservable
    {
        /// <summary>
        /// Occurs when the server broadcasts that a range has been reserved by any client.
        /// </summary>
        event Action<Lease> OnRangeReserved;


        /// <summary>
        /// Occurs when a previously reserved range becomes free for use, either through 
        /// explicit cancellation or server-side cleanup.
        /// </summary>
        event Action<Guid> OnRangeAvailable;


        /// <summary>
        /// Occurs when a range has been successfully converted from a lease to a permanent booking.
        /// </summary>
        event Action<Guid> OnRangeConfirmed;


        /// <summary>
        /// Occurs when a lease attempt fails, typically due to a detected overlap with 
        /// another active lease or a confirmed booking.
        /// </summary>
        event Action<string> OnError; //Happens if a user tries to reserve a timeslot that is already reserved
    }
}

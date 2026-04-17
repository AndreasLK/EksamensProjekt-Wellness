using Core.Domain.Entities;

namespace Core.Domain.Interfaces.Leases
{
    /// <summary>
    /// Commands that change the state of a lease on the server.
    /// </summary>
    public interface ILeaseCommands
    {
        //Outgoing

        /// <summary>
        /// Requests a temporary lease for a specific timeslot. 
        /// Triggers a conflict check on the server against both RAM and Database storage.
        /// </summary>
        /// <param name="timeSlot">The specific range of time to reserve.</param>
        /// <returns>A task representing the asynchronous reservation request.</returns>
        Task ReserveRangeAsync(TimeSlot timeSlot); //Reserve timeslot for an entry


        /// <summary>
        /// Renews an existing lease to prevent it from being removed by the server's safety cleanup (Reaper).
        /// This acts as the "Heartbeat" signal for an active reservation.
        /// </summary>
        /// <param name="leaseId">The unique identifier of the lease to renew.</param>
        /// <returns>A task representing the asynchronous renewal operation.</returns>
        Task RenewLeaseAsync(Guid leaseId);


        /// <summary>
        /// Transitions a temporary lease into a permanent booking in the database.
        /// Once confirmed, the lease is removed from in-memory storage.
        /// </summary>
        /// <param name="leaseId">The unique identifier of the lease being finalized.</param>
        /// <returns>A task representing the asynchronous confirmation operation.</returns>
        Task ConfirmBookingAsync(Guid leaseId); //TODO: Add booking details


        /// <summary>
        /// Explicitly releases a lease, making the timeslot available for other users immediately.
        /// </summary>
        /// <param name="leaseId">The unique identifier of the lease to cancel.</param>
        /// <returns>A task representing the asynchronous cancellation operation.</returns>
        Task CancelLeaseAsync(Guid leaseId); //If lease unused or 

    }
}

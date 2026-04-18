using Domain.Enums;

namespace Domain.Interfaces.Leases
{
    /// <summary>
    /// Manages the lifecycle of the real-time connection.
    /// </summary>
    public interface ILeaseConnection
    {
        /// <summary>
        /// Initializes the connection to the real-time leasing service.
        /// This should be called before attempting any lease actions.
        /// </summary>
        /// <returns>A task representing the asynchronous initialization operation.</returns>
        Task InitializeAsync();

        /// <summary>
        /// Gets the current state of the connection to the leasing service.
        /// </summary>
        LeaseConnectionStatus Status { get; }
    }
}

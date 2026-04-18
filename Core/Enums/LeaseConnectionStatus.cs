namespace Domain.Enums
{
    /// <summary>
    /// Represents the various states of the connection between the client 
    /// and the real-time leasing service.
    /// </summary>
    public enum LeaseConnectionStatus
    {
        /// <summary>
        /// No active connection exists. This is the default state before initialization.
        /// </summary>
        Disconnected,

        /// <summary>
        /// The client is currently attempting to establish a handshake with the server.
        /// </summary>
        Connecting,

        /// <summary>
        /// A live connection is established. Actions and heartbeats can be sent.
        /// </summary>
        Connected,

        /// <summary>
        /// The connection was lost, and the client is attempting to restore it automatically.
        /// </summary>
        Reconnecting,

        /// <summary>
        /// The connection has encountered a critical failure and cannot proceed without intervention.
        /// </summary>
        Faulted
    }
}

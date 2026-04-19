using Domain.ValueObjects;

namespace Domain.Interfaces
{
    /// <summary>
    /// Manages the transition of temporary leases into permanent database records.
    /// </summary>
    public interface IBookingService
    {

        /// <summary>
        /// Finalizes a temporary lease by attaching customer and treatment details 
        /// and persisting it as a confirmed Booking.
        /// </summary>
        /// <param name="leaseId">The unique identifier of the active in-memory lease.</param>
        /// <param name="customerId">The customer receiving the service.</param>
        /// <param name="treatmentTypeId">The specific treatment being performed.</param>
        /// <param name="finalPrice">The final calculated price for the session.</param>
        /// <returns>True if the booking was successfully persisted; otherwise, false.</returns>
        Task<bool> FinalizeBookingAsync(
            Guid leaseId,
            Guid customerId,
            Guid treatmentTypeId,
            Money finalPrice);
    }
}

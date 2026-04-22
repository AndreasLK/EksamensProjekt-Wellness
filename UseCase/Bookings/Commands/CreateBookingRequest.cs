using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase.Bookings.Commands
{
    /// <summary>
    /// Data transfer object used to submit a new booking request.
    /// Captures the user's intent to start a specific treatment at a specific time.
    /// </summary>
    /// <param name="PractitionerId">The unique ID of the selected practitioner.</param>
    /// <param name="CustomerId">The unique ID of the customer.</param>
    /// <param name="TreatmentTypeId">The treatment variation, which dictates the session duration.</param>
    /// <param name="RoomId">The ID of the room where the session will occur.</param>
    /// <param name="Start">The requested start time.</param>
    public record CreateBookingRequest(
        Guid PractitionerId,
        Guid CustomerId,
        Guid TreatmentTypeId,
        Guid RoomId,
        DateTimeOffset Start);
}

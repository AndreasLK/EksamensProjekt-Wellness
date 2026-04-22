using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase.Bookings.Queries
{
    /// <summary>
    /// Represents a comprehensive booking summary for the calendar grid.
    /// </summary>
    /// <param name="Id">The unique identifier for the booking.</param>
    /// <param name="TimeSlot">The validated temporal interval for the appointment.</param>
    /// <param name="TreatmentName">The display name of the treatment variation.</param>
    /// <param name="PractitionerName">The full name of the assigned practitioner.</param>
    /// <param name="CustomerName">The full name of the customer.</param>
    /// <param name="ClinicName">The name of the clinic location.</param>
    /// <param name="RoomName">The specific name or number of the treatment room.</param>
    public record CalendarBookingDto(
        Guid Id,
        TimeSlot TimeSlot,
        string TreatmentName,
        string PractitionerName,
        string CustomerName,
        string ClinicName,
        string RoomName);
}

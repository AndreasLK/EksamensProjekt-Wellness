using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase.Rooms.Queries
{
    /// <summary>
    /// Represents a treatment room for selection within the calendar or booking forms.
    /// </summary>
    /// <param name="Id">Unique identifier for the room.</param>
    /// <param name="Name">The room name or number (like "Room 101").</param>
    /// <param name="ClinicId">The ID of the parent clinic this room belongs to.</param>
    public record RoomSelectDto(Guid Id, string Name, Guid ClinicId);
}

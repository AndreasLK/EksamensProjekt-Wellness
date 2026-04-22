using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase.Practitioners.Queries
{
    /// <summary>
    /// Represents a practitioner for selection in the booking interface.
    /// </summary>
    /// <param name="Id">Unique identifier.</param>
    /// <param name="FullName">The combined first and last name for display.</param>
    /// <param name="Initials">Shortened name for small calendar cards.</param>
    public record PractitionerSelectDto(
        Guid Id,
        string FullName,
        string Initials);
}

using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase.Clinics.Queries
{
    /// <summary>
    /// Represents a clinic location for selection lists.
    /// </summary>
    /// <param name="Id">Unique identifier for the clinic.</param>
    /// <param name="Name">The display name (e.g., "Vejle Center").</param>
    public record ClinicSelectDto(Guid Id, string Name);
}

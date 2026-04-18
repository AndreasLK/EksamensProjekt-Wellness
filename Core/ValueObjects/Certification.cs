using Domain.Enums;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Represents a specific professional certification held by a practitioner.
    /// A Value Object that links a role to its legal license number.
    /// </summary>
    public record Certification(AuthorizationType Type, string LicenseNumber);
}
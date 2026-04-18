using Domain.ValueObjects.OpeningHours;

namespace Domain.Interfaces
{
    /// <summary>
    /// Defines a contract for any schedule entry that provides operational boundaries.
    /// </summary>
    public interface IOperationalPeriod
    {
        /// <summary>
        /// Gets the time boundaries for this specific period.
        /// </summary>
        TimeWindow Window { get; }
    }
}

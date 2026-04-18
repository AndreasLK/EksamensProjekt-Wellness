using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects.OpeningHours
{
    /// <summary>
    /// Represents a date-specific override to the regular clinic schedule.
    /// </summary>
    public record SpecialOpeningHours : IOperationalPeriod
    {
        /// <summary>
        /// The specific calendar date for the operational override.
        /// </summary>
        public DateOnly Date { get; init; }

        /// <summary>
        /// The operational time boundaries for this specific date.
        /// </summary>
        public TimeWindow Window { get; init; }

        /// <summary>
        /// An optional description of the reason for the override.
        /// </summary>
        /// <example>Christmas Eve, Maintenance, Staff Meeting.</example>
        public string? Description { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialOpeningHours"/> record.
        /// </summary>
        /// <param name="date">The exact calendar date.</param>
        /// <param name="window">The operational time window.</param>
        /// <param name="description">The reason for the override.</param>
        public SpecialOpeningHours(DateOnly date, TimeWindow window, string? description = null)
        {
            Date = date;
            Window = window;
            Description = description;
        }
    }
}

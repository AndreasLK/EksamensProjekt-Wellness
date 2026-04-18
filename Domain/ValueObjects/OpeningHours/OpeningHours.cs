using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects.OpeningHours
{
    /// <summary>
    /// Represents the standard operational period for a specific day of the week.
    /// </summary>
    public record OpeningHours : IOperationalPeriod
    {
        /// <summary>
        /// The specific day of the week this schedule applies to.
        /// </summary>
        public DayOfWeek DayOfWeek { get; init; }


        /// <summary>
        /// The operational time boundaries for this day.
        /// </summary>
        public TimeWindow Window { get; init; }


        /// <summary>
        /// Initializes a new instance of the <see cref="OpeningHours"/> record.
        /// </summary>
        /// <param name="dayOfWeek">The day to define.</param>
        /// <param name="window">The start and end times for the day.</param>
        public OpeningHours(DayOfWeek dayOfWeek, TimeWindow window)
        {
            DayOfWeek = dayOfWeek;
            Window = window;
        }
    }
}

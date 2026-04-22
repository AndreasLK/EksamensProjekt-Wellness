using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Represents a rich, configurable duration specifically for loyalty evaluation periods.
    /// Combines calendar months and days to avoid primitive obsession and provide flexibility.
    /// </summary>
    public record LoyaltyDuration
    {
        /// <summary>The number of full calendar months in the duration.</summary>
        public int Months { get; init; }

        /// <summary>The number of additional days in the duration.</summary>
        public int Days { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoyaltyDuration"/> record.
        /// </summary>
        /// <param name="months">The number of calendar months (must be non-negative).</param>
        /// <param name="days">Optional additional days (must be non-negative).</param>
        public LoyaltyDuration(int months, int days = 0)
        {
            if (months < 0 || days < 0)
            {
                throw new ArgumentException("Duration components cannot be negative.");
            }

            Months = months;
            Days = days;
        }

        /// <summary>
        /// Calculates the starting point of the evaluation window relative to a target date.
        /// Centralizes the business logic for date subtraction.
        /// </summary>
        /// <param name="relativeTo">The date to calculate backward from (usually UtcNow).</param>
        /// <returns>A DateTimeOffset representing the start of the loyalty window.</returns>
        public DateTimeOffset CalculateWindowStart(DateTimeOffset relativeTo)
        {
            return relativeTo.AddMonths(-Months).AddDays(-Days);
        }
    }
}

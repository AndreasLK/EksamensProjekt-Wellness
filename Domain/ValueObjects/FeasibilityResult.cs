using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Describes the outcome of a travel feasibility check, including suggested time constraints.
    /// </summary>
    public class FeasibilityResult
    {
        /// <summary>
        /// Indicates if the practitioner can physically attend the requested slot.
        /// </summary>
        public bool IsFeasible { get; }


        /// <summary>
        /// The earliest point in time the practitioner can arrive at the requested clinic.
        /// </summary>
        public DateTimeOffset? EarliestPossibleArrival { get; }


        /// <summary>
        /// The latest point in time the practitioner can stay at the requested clinic before departing for the next appointment.
        /// </summary>
        public DateTimeOffset? LatestPossibleDeparture { get; }


        private FeasibilityResult(
            bool isFeasible, 
            DateTimeOffset? earliestPossibleArrival = null, 
            DateTimeOffset? latestPossibleDeparture = null)
        {
            this.IsFeasible = isFeasible;
            this.EarliestPossibleArrival = earliestPossibleArrival;
            this.LatestPossibleDeparture = latestPossibleDeparture;
        }

        /// <summary>
        /// Creates a successful feasibility result.
        /// </summary>
        /// <returns>A result indicating the schedule is possible.</returns>
        public static FeasibilityResult Success() => new FeasibilityResult(true);

        /// <summary>
        /// Creates a failed feasibility result containing calculated time constraints.
        /// </summary>
        /// <param name="earliestPossibleArrival">Calculated arrival limit based on prior bookings.</param>
        /// <param name="latestPossibleDeparture">Calculated departure limit based on subsequent bookings.</param>
        /// <returns>A result indicating the schedule is impossible with attached metadata.</returns>
        public static FeasibilityResult Failure(
            DateTimeOffset? earliestPossibleArrival,
            DateTimeOffset? latestPossibleDeparture)
            => new FeasibilityResult(
                isFeasible: false,
                earliestPossibleArrival: earliestPossibleArrival,
                latestPossibleDeparture: latestPossibleDeparture);
    }
}

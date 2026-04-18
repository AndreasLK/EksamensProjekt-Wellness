using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface ITravelTimeService
    {
        /// <summary>
        /// Calculates travel duration. Defaults to a Departure-based calculation starting 'Now'.
        /// </summary>
        /// <param name="origin">Starting address.</param>
        /// <param name="destination">Destination address.</param>
        /// <param name="anchor">Whether to calculate based on Departure or Arrival (default: Departure).</param>
        /// <param name="referenceTime">The time for the calculation. If null, UTC Now is used.</param>
        Task<TimeSpan> GetTravelDurationAsync(
            Address origin,
            Address destination,
            TimeAnchor anchor = TimeAnchor.Departure,
            DateTimeOffset? referenceTime = null
            );
    }
}

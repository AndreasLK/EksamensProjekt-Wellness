using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    /// <summary>
    /// Centralized business logic constants defined by BookRight requirements.
    /// </summary>
    public static class BusinessRules
    {
        /// <summary>
        /// The standardized duration for loyalty evaluation (currently 12 months per case).
        /// Using a rich LoyaltyDuration avoids primitive obsession.
        /// </summary>
        public static readonly LoyaltyDuration LOYALTY_EVALUATION_PERIOD = new LoyaltyDuration(months: 12);

        /// <summary>
        /// Standard travel time between clinics.
        /// </summary>
        public static readonly TimeSpan DEFAULT_TRAVEL_EVALUATION = TimeSpan.FromMinutes(45);
    }
}

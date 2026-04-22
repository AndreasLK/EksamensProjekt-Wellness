using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    /// <summary>
    /// Defines the symbolic reasons for applying a discount.
    /// Used as a type-safe contract to avoid magic strings or integer indexes.
    /// </summary>
    public enum DiscountReason
    {
        /// <summary>No discount applied.</summary>
        None,


        /// <summary>Applied for Bronze tier membership.</summary>
        BronzeMember,


        /// <summary>Applied for Silver tier membership.</summary>
        SilverMember,


        /// <summary>Applied for Gold tier membership.</summary>
        GoldMember,


        /// <summary>Manually applied by a staff member.</summary>
        ManualOverride,


        /// <summary>Applied for Birthday or Birthmonth discounts.</summary>
        Birthday,


        /// <summary>Applied via a specific marketing campaign.</summary>
        Campaign
    }
}

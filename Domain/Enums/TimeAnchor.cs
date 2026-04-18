using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    /// <summary>
    /// Specifies whether the calculation anchor is the departure time or the arrival time.
    /// </summary>
    public enum TimeAnchor
    {
        /// <summary>Calculate based on when the practitioner leaves the origin.</summary>
        Departure,


        /// <summary>Calculate based on when the practitioner must arrive at the destination.</summary>
        Arrival
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects.OpeningHours
{
    public record TimeWindow
    {
        /// <summary>
        /// The exact time of day when the clinic begins operations.
        /// </summary>
        /// <remarks>
        /// If this value is null, it indicates that the clinic is not accepting 
        /// appointments at any time during this specific schedule entry.
        /// </remarks>
        public TimeOnly? OpenTime { get; init; }


        /// <summary>
        /// The exact time of day when the clinic ceases operations.
        /// </summary>
        /// <remarks>
        /// This value must be chronologically after the <see cref="OpenTime"/>. 
        /// If null, the clinic is considered closed for this period.
        /// </remarks>
        public TimeOnly? CloseTime { get; init; }


        /// <summary>
        /// Gets a value indicating whether the clinic is unavailable for the entire period.
        /// </summary>
        /// <value>
        /// True if either <see cref="OpenTime"/> or <see cref="CloseTime"/> are null; 
        /// otherwise, false.
        /// </value>
        public bool IsClosed => OpenTime == null || CloseTime == null;


        /// <summary>
        /// Initializes a new instance of the <see cref="TimeWindow"/> record.
        /// </summary>
        /// <param name="openTime">The time operational availability begins.</param>
        /// <param name="closeTime">The time operational availability ends.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if the <paramref name="openTime"/> is equal to or later than the <paramref name="closeTime"/>.
        /// </exception>
        public TimeWindow(TimeOnly? openTime, TimeOnly? closeTime)
        {
            if (openTime.HasValue && closeTime.HasValue && openTime >= closeTime)
            {
                throw new ArgumentException("The start of operations must occur before the end of operations.");
            }

            OpenTime = openTime;
            CloseTime = closeTime;
        }

        /// <summary>
        /// Determines whether a specific time falls within the current window.
        /// </summary>
        /// <param name="time">The time to check.</param>
        /// <returns>True if the time is within the window; otherwise, false. Returns false if closed.</returns>
        public bool Contains(TimeOnly time)
        {
            if (IsClosed)
            {
                return false;
            }

            return time >= OpenTime.Value && time < CloseTime.Value;
        }

        /// <summary>
        /// Factory method to create an object representing a full day of non-availability.
        /// </summary>
        /// <returns>A <see cref="TimeWindow"/> where both boundaries are null.</returns>
        public static TimeWindow Closed()
        {
            return new TimeWindow(null, null);
        }
    }
}

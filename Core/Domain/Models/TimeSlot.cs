using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Models
{
    public record TimeSlot
    {
        public DateTimeOffset StartDateTime { get; } //DatetimeOffset works accross timezones, otherwise exactly like DateTime
        public DateTimeOffset EndDateTime { get; }

        TimeSlot(DateTimeOffset startDateTime, DateTimeOffset endDateTime) {
            if (startDateTime >= endDateTime)
            {
                throw new ArgumentException("The Start time must be before the End time");
            }

        }
    }
}

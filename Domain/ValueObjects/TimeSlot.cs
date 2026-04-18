namespace Domain.ValueObjects
{
    public record TimeSlot
    {
        /// <summary>The beginning of the reserved time slot.</summary>
        public DateTimeOffset StartDateTime { get; init; }


        /// <summary>The end of the reserved time slot.</summary>
        public DateTimeOffset EndDateTime { get; init; }


        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSlot"/> record.
        /// Validates that the start time occurs before the end time.
        /// </summary>
        /// <param name="startDateTime">The start of the interval.</param>
        /// <param name="endDateTime">The end of the interval.</param>
        public TimeSlot(DateTimeOffset startDateTime, DateTimeOffset endDateTime)
        {
            if (startDateTime >= endDateTime)
            {
                throw new ArgumentException("The Start time must be before the End time");
            }

            this.StartDateTime = startDateTime;
            this.EndDateTime = endDateTime;

        }
    }
}

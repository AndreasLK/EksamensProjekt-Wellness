namespace Core.Domain.Entities
{
    public record TimeSlot
    {
        public DateTimeOffset StartDateTime { get; } //DatetimeOffset works accross timezones, otherwise exactly like DateTime
        public DateTimeOffset EndDateTime { get; }

        TimeSlot(DateTimeOffset StartDateTime, DateTimeOffset EndDateTime)
        {
            if (StartDateTime >= EndDateTime)
            {
                throw new ArgumentException("The Start time must be before the End time");
            }

        }
    }
}

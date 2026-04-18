using Domain.Interfaces;
using Domain.ValueObjects.OpeningHours;

namespace Domain.Entities.Clinics
{
    /// <summary>
    /// Represents a physical clinic location where healthcare treatments are conducted.
    /// Manages operational hours and resource constraints like treatment room capacity.
    /// </summary>
    public class Clinic : Entity
    {
        /// <summary>
        /// The official name of the clinic branch.
        /// </summary>
        public string Name { get; private set; }


        /// <summary>
        /// The physical street address used for location services and travel time calculations.
        /// </summary>
        public string Address { get; private set; }


        /// <summary>
        /// The collection of treatment rooms available at this clinic.
        /// </summary>
        private readonly List<Room> _rooms = new();


        /// <summary>
        /// Gets all rooms assigned to this clinic.
        /// </summary>
        public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();


        /// <summary>
        /// Gets the total number of rooms, providing a quick capacity check.
        /// </summary>
        public int TotalRoomCount => _rooms.Count;


        /// <summary>
        /// The standard recurring weekly schedule for the clinic.
        /// </summary>
        private readonly List<OpeningHours> _weeklyOpeningHours = new();


        /// <summary>
        /// Gets the collection of standard weekly opening hours.
        /// </summary>
        public IReadOnlyCollection<OpeningHours> WeeklyOpeningHours => _weeklyOpeningHours.AsReadOnly();


        /// <summary>
        /// Specific date overrides for holidays or special events.
        /// </summary>
        private readonly List<SpecialOpeningHours> _specialOpeningHours = new();


        /// <summary>
        /// Gets the collection of special date-specific opening hours.
        /// </summary>
        public IReadOnlyCollection<SpecialOpeningHours> SpecialOpeningHours => _specialOpeningHours.AsReadOnly();

        /// <summary>
        /// Initializes a new instance of the <see cref="Clinic"/> class.
        /// </summary>
        /// <param name="name">The display name of the clinic.</param>
        /// <param name="address">The full physical address.</param>
        /// <param name="id">Optional existing identifier for database rehydration.</param>
        /// <exception cref="ArgumentException">Thrown when name or address is empty, or roomCount is non-positive.</exception>
        public Clinic(
            string name,
            string address,
            Guid? id = null) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Clinic name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("Clinic address cannot be empty.");
            }

            Name = name;
            Address = address;
        }

        /// <summary>
        /// Replaces the entire standard weekly schedule for the clinic.
        /// </summary>
        /// <param name="schedule">The new collection of weekly opening hours.</param>
        public void SetWeeklySchedule(IEnumerable<OpeningHours> schedule)
        {
            _weeklyOpeningHours.Clear();
            _weeklyOpeningHours.AddRange(schedule);
        }


        /// <summary>
        /// Adds or updates a special opening hour override for a specific date.
        /// </summary>
        /// <param name="specialHours">The date-specific operational window.</param>
        public void AddSpecialOpeningHours(SpecialOpeningHours specialHours)
        {
            // Remove existing override for the same date if it exists
            _specialOpeningHours.RemoveAll(h => h.Date == specialHours.Date);
            _specialOpeningHours.Add(specialHours);
        }


        /// <summary>
        /// Determines if the clinic is operational at a given point in time.
        /// </summary>
        /// <param name="dateTime">The date and time to verify.</param>
        /// <returns>True if the clinic is open; otherwise, false.</returns>
        /// <remarks>
        /// This method prioritizes <see cref="SpecialOpeningHours"/> over the 
        /// standard <see cref="WeeklyOpeningHours"/>.
        /// </remarks>
        public bool IsOpenAt(DateTimeOffset dateTime)
        {
            var date = DateOnly.FromDateTime(dateTime.Date);
            var time = TimeOnly.FromDateTime(dateTime.DateTime);

            //Checking for Holiday/Special Overrides first
            var special = _specialOpeningHours.FirstOrDefault(h => h.Date == date);
            if (special != null)
            {
                return IsTimeWithinPeriod(special, time);
            }

            //Falls back to Regular Weekly Schedule
            var weekly = _weeklyOpeningHours.FirstOrDefault(h => h.DayOfWeek == dateTime.DayOfWeek);
            return IsTimeWithinPeriod(weekly, time);
        }


        /// <summary>
        /// Internal helper to validate a time against a provided operational period.
        /// </summary>
        /// <param name="period">The schedule entry containing a time window.</param>
        /// <param name="time">The time of day to check.</param>
        /// <returns>True if the period exists and contains the time.</returns>
        private bool IsTimeWithinPeriod(IOperationalPeriod? period, TimeOnly time)
        {
            return period?.Window.Contains(time) ?? false;
        }


        /// <summary>
        /// Adds a new room to the clinic's inventory.
        /// </summary>
        /// <param name="room">The room to add.</param>
        public void AddRoom(Room room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            // Ensures room names are unique within this specific clinic
            if (_rooms.Any(r => r.Name.Equals(room.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"A room with the name '{room.Name}' already exists in this clinic.");
            }

            _rooms.Add(room);
        }
    }
}

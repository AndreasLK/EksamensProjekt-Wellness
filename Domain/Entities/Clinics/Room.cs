using Domain.Interfaces;
using Domain.ValueObjects.OpeningHours;

namespace Domain.Entities.Clinics
{
    /// <summary>
    /// Represents a specific physical treatment or consultation room within a clinic.
    /// Manages its own resource constraints, equipment, and operational filtering.
    /// </summary>
    public class Room : Entity
    {
        /// <summary>
        /// The name or number used to identify the room (e.g., "Room 101" or "Ball Room").
        /// </summary>
        public string Name { get; private set; }


        /// <summary>
        /// The floor space of the room in square meters.
        /// </summary>
        public double SizeInSquareMeters { get; private set; }


        /// <summary>
        /// The maximum number of people the room can safely accommodate for a session.
        /// </summary>
        public int Capacity { get; private set; }


        /// <summary>
        /// The list of specialized equipment currently assigned to this room.
        /// </summary>
        private readonly List<string> _equipment = new();


        /// <summary>
        /// Gets the collection of equipment available in this room.
        /// </summary>
        public IReadOnlyCollection<string> Equipment => _equipment.AsReadOnly();


        /// <summary>
        /// Specific recurring weekly schedule if the room's availability differs 
        /// from the clinic's standard hours.
        /// </summary>
        private readonly List<OpeningHours> _weeklySchedule = new();


        /// <summary>
        /// Gets the room-specific weekly recurring schedule.
        /// </summary>
        public IReadOnlyCollection<OpeningHours> WeeklySchedule => _weeklySchedule.AsReadOnly();


        /// <summary>
        /// One-off overrides for the room, such as maintenance blackouts or seasonal use.
        /// </summary>
        private readonly List<SpecialOpeningHours> _specialOverrides = new();


        /// <summary>
        /// Gets the collection of date-specific overrides for the room.
        /// </summary>
        public IReadOnlyCollection<SpecialOpeningHours> SpecialOverrides => _specialOverrides.AsReadOnly();


        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class.
        /// </summary>
        /// <param name="name">The room identifier.</param>
        /// <param name="sizeInSquareMeters">The physical size.</param>
        /// <param name="capacity">Maximum occupants.</param>
        /// <param name="id">Optional identifier for database rehydration.</param>
        public Room(
            string name,
            double sizeInSquareMeters,
            int capacity,
            Guid? id = null) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Room name cannot be empty.");
            }

            if (sizeInSquareMeters <= 0)
            {
                throw new ArgumentException("Room size must be positive.");
            }

            if (capacity <= 0)
            {
                throw new ArgumentException("Room capacity must be at least one.");
            }

            Name = name;
            SizeInSquareMeters = sizeInSquareMeters;
            Capacity = capacity;
        }


        /// <summary>
        /// Determines if the room itself is operational at a given point in time.
        /// </summary>
        /// <param name="dateTime">The timestamp to verify.</param>
        /// <returns>
        /// True if the room has no restrictions or falls within its specific rules.
        /// False if a room-level rule specifically blocks the time.
        /// </returns>
        /// <remarks>
        /// This logic acts as a secondary filter. The primary check should always be 
        /// against the Clinic's schedule first.
        /// </remarks>
        public bool IsAvailableAt(DateTimeOffset dateTime)
        {
            var date = DateOnly.FromDateTime(dateTime.Date);
            var time = TimeOnly.FromDateTime(dateTime.DateTime);

            // 1. Check Special Overrides (e.g., "Painting" or "Temporary Availability")
            var special = _specialOverrides.FirstOrDefault(s => s.Date == date);
            if (special != null)
            {
                return IsTimeWithinPeriod(special, time);
            }

            // 2. Check Weekly Schedule (e.g., "This room is only used on Mondays")
            var weekly = _weeklySchedule.FirstOrDefault(w => w.DayOfWeek == dateTime.DayOfWeek);
            if (weekly != null)
            {
                return IsTimeWithinPeriod(weekly, time);
            }

            // 3. Fallback: If no room-specific rules exist, the room follows the building hours.
            return true;
        }


        /// <summary>
        /// Assigns a new piece of equipment to the room.
        /// </summary>
        /// <param name="equipmentName">The name of the resource (e.g., "Massage Table").</param>
        public void AddEquipment(string equipmentName)
        {
            if (!string.IsNullOrWhiteSpace(equipmentName) && !_equipment.Contains(equipmentName))
            {
                _equipment.Add(equipmentName);
            }
        }


        /// <summary>
        /// Removes a piece of equipment from the room.
        /// </summary>
        /// <param name="equipmentName">The name of the item to remove.</param>
        /// <returns>True if the item was found and removed; otherwise, false.</returns>
        public bool RemoveEquipment(string equipmentName)
        {
            if (string.IsNullOrWhiteSpace(equipmentName))
            {
                return false;
            }

            return _equipment.Remove(equipmentName);
        }


        /// <summary>
        /// Updates the recurring weekly schedule for the room.
        /// </summary>
        public void SetWeeklySchedule(IEnumerable<OpeningHours> schedule)
        {
            _weeklySchedule.Clear();
            _weeklySchedule.AddRange(schedule);
        }


        /// <summary>
        /// Adds a specific date override for the room.
        /// </summary>
        public void AddOverride(SpecialOpeningHours special)
        {
            _specialOverrides.RemoveAll(o => o.Date == special.Date);
            _specialOverrides.Add(special);
        }


        /// <summary>
        /// Helper to check time boundaries against a specific operational period.
        /// </summary>
        private bool IsTimeWithinPeriod(IOperationalPeriod? period, TimeOnly time)
        {
            return period?.Window.Contains(time) ?? false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Clinics
{
    /// <summary>
    /// Represents a specific physical treatment or consultation room within a clinic.
    /// Manages capacity and specialized equipment availability.
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
    }
}

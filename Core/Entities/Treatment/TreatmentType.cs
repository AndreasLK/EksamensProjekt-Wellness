using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Treatment
{
    /// <summary>
    /// Represents a specific variation of a treatment (e.g., 30 min vs 60 min).
    /// Contains the specific pricing and duration data
    /// </summary>
    public class TreatmentType : Entity
    {
        /// <summary>A descriptive name for the variation (e.g., "Initial Consultation").</summary>
        public string Name { get; private set; }


        /// <summary>The duration of this specific session in minutes.</summary>
        public TimeSpan Duration { get; private set; }


        /// <summary>The base cost in the selected currency.</summary>
        public Money BasePrice { get; private set; }

        /// <summary>The unique identifier of the parent <see cref="TreatmentCategory"/>.</summary>
        public Guid CategoryId { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="TreatmentType"/> class.
        /// </summary>
        /// <param name="name">The variation name.</param>
        /// <param name="duration">Session length.</param>
        /// <param name="price">Cost before discounts.</param>
        /// <param name="categoryId">ID of the parent category.</param>
        public TreatmentType(string name, TimeSpan duration, Money price, Guid categoryId)
        {
            this.Name = name;
            this.Duration = duration;
            this.BasePrice = price;
            this.CategoryId = categoryId;
        }


    }
}

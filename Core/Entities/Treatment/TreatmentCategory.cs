using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Treatment
{
    /// <summary>
    /// Represents a high-level category of service (e.g., "Sportsmassage" or "Fysioterapi").
    /// Holds the authorization rules shared by all time-variations within this category.
    /// </summary>
    public class TreatmentCategory : Entity
    {
        /// <summary>The name of the category.</summary>
        public string Name { get; private set; }


        /// <summary> The professional certification required to perform any treatment in this category.</summary>
        public AuthorizationType RequiredAuthorization { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="TreatmentCategory"/> class.
        /// </summary>
        /// <param name="name">The category name (e.g., Akupunktur).</param>
        /// <param name="requiredAuthorization">The role required to perform this work.</param>
        public TreatmentCategory(string name, AuthorizationType requiredAuthorization) : base()
        {
            Name = name;
            RequiredAuthorization = requiredAuthorization;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase.Practitioners.Queries
{
    /// <summary>
    /// Represents a practitioner for selection in the UI.
    /// </summary>
    /// <param name="Id">The unique identifier for the practitioner.</param>
    /// <param name="FirstName">The practitioner's given name.</param>
    /// <param name="LastName">The practitioner's family name.</param>
    public record PractitionerSelectDto(Guid Id, string FirstName, string LastName)
    {
        /// <summary>
        /// Full name formatted as "First Last".
        /// </summary>
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }


        /// <summary>
        /// Gets the practitioner's initials (like "PP" for Peter Parker).
        /// Calculated here to centralize identity logic and keep the UI clean.
        /// </summary>
        public string Initials
        {
            get
            {
                string firstInitial = string.IsNullOrWhiteSpace(FirstName)
                    ? string.Empty
                    : FirstName[0].ToString();

                string lastInitial = string.IsNullOrWhiteSpace(LastName)
                    ? string.Empty
                    : LastName[0].ToString();

                return $"{firstInitial}{lastInitial}".ToUpper();
            }
        }
    }
}

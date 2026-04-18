using Domain.Entities.Treatment;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Practitioner : Entity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        /// <summary>
        /// A collection of certifications held by the practitioner.
        /// Using a private field and public IReadOnlyCollection maintains encapsulation.
        /// </summary>
        private readonly List<Certification> _certifications = new();
        public IReadOnlyCollection<Certification> Certifications => _certifications.AsReadOnly();

        /// <summary>
        /// Initializes a new instance of the <see cref="Practitioner"/> class.
        /// </summary>
        /// <param name="firstName">The practitioner's first name.</param>
        /// <param name="lastName">The practitioner's last name.</param>
        /// <param name="email">The practitioner's email.</param>
        /// <param name="phone">The practitioner's phone number.</param>
        /// <param name="id">Optional identifier for rehydrating existing practitioners from the database.</param>
        public Practitioner(
            string firstName,
            string lastName,
            string email,
            string phone,
            Guid? id = null) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }

        /// <summary>
        /// Validates if the practitioner holds the authorization required for a specific treatment.
        /// </summary>
        public bool IsAuthorizedFor(TreatmentCategory category)
        {
            return _certifications.Any(c => c.Type == category.RequiredAuthorization);
        }

        public void AddCertification(Certification certification)
        {
            if (!_certifications.Contains(certification))
            {
                _certifications.Add(certification);
            }
        }
    }
}

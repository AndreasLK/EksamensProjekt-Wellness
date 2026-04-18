using Core.Domain.Enums;
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

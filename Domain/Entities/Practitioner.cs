using Domain.Entities.Treatment;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities
{
    /// <summary>
    /// A professional healthcare provider (Behandler) in the BookRight system.
    /// </summary>
    public class Practitioner : Entity
    {
        /// <summary>
        /// The practitioner's first name.
        /// </summary>
        public string FirstName { get; private set; }


        /// <summary>
        /// The practitioner's last name.
        /// </summary>
        public string LastName { get; private set; }


        /// <summary>
        /// The practitioner's professional email address.
        /// </summary>
        public string Email { get; private set; }


        /// <summary>
        /// The practitioner's professional phone number.
        /// </summary>
        public string Phone { get; private set; }


        private readonly List<Certification> _certifications = new();
        /// <summary>
        /// A collection of certifications held by the practitioner.
        /// </summary>
        public IReadOnlyCollection<Certification> Certifications => _certifications.AsReadOnly();


        private readonly List<Guid> _clinicAffiliations = new();
        ///<summary>
        ///The identifier of the specific clinics where a practitioner can go, like "i only work in Vejle"
        /// </summary>
        public IReadOnlyCollection<Guid> ClinicAffiliations => _clinicAffiliations.AsReadOnly();

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


        /// <summary>
        /// Registration logic for adding a new professional certification or license.
        /// </summary>
        /// <param name="certification">The certification details to add.</param>
        public void AddCertification(Certification certification)
        {
            if (!_certifications.Contains(certification))
            {
                _certifications.Add(certification);
            }
        }


        /// <summary>
        /// Removal logic for certifications in cases of expiration or professional status changes.
        /// </summary>
        /// <param name="licenseNumber">Id of certificate to remove.</param>
        public void RemoveCertification(string licenseNumber)
        {
            var certification = _certifications.FirstOrDefault(c => c.LicenseNumber == licenseNumber);
            if (certification != null)
            {
                _certifications.Remove(certification);
            }
        }


        /// <summary>
        /// Association logic for linking the practitioner to a clinic location.
        /// </summary>
        /// <param name="clinicId">The identifier of the clinic to add.</param>
        public void AssignToClinic(Guid clinicId)
        {
            if (!_clinicAffiliations.Contains(clinicId))
            {
                _clinicAffiliations.Add(clinicId);
            }
        }


        /// <summary>
        /// Removal logic for clinic affiliations when a practitioner no longer works at a location.
        /// </summary>
        /// <param name="clinicId">The identifier of the clinic to remove.</param>
        public void RemoveClinicAffiliation(Guid clinicId)
        {
            _clinicAffiliations.Remove(clinicId);
        }


    }
}

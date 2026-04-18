using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Represents a physical address using 2 letter country codes
    /// </summary>
    public record Address
    {
        /// <summary>
        /// The name of the street.
        /// </summary>
        public string StreetName { get; init; }


        /// <summary>
        /// The specific house or building number.
        /// </summary>
        public string StreetNumber { get; init; }


        /// <summary>
        /// An optional letter identifying a sub-division of the street number like 'A' in '12A'.
        /// </summary>
        public string? Letter { get; init; }


        /// <summary>
        /// The postal or zip code. Stored as a string to preserve leading zeros and international formatting.
        /// </summary>
        public string PostalCode { get; init; }


        /// <summary>
        /// 2-letter ISO country code, like "DK" for Denmark
        /// </summary>
        public string CountryCode { get; init; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> record with validated components.
        /// </summary>
        /// <param name="streetName">The name of the street.</param>
        /// <param name="streetNumber">The house or building number.</param>
        /// <param name="postalCode">The postal code as a string.</param>
        /// <param name="countryCode">A valid 2-letter ISO country code.</param>
        /// <param name="letter">The optional street letter. Defaults to null.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if required fields are null/empty or if <paramref name="countryCode"/> is not exactly two letters.
        /// </exception>
        public Address(string streetName, string streetNumber, string postalCode, string countryCode, string? letter = null)
        {
            if (string.IsNullOrWhiteSpace(streetName)) throw new ArgumentException("Street name is required.");
            if (string.IsNullOrWhiteSpace(streetNumber)) throw new ArgumentException("Street number is required.");
            if (string.IsNullOrWhiteSpace(postalCode)) throw new ArgumentException("Postal code is required.");
            if (string.IsNullOrWhiteSpace(countryCode) || !Regex.IsMatch(countryCode, @"^[A-Z]{2}$", RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("Country code must be a valid 2-letter ISO code (e.g., DK).");
            }


            this.StreetName = streetName;
            this.StreetNumber = streetNumber;
            this.PostalCode = postalCode;
            this.CountryCode = countryCode.ToUpper();
            this.Letter = letter;
        }

        /// <summary>
        /// Returns a formatted string representation of the address suitable for Geocoding APIs.
        /// </summary>
        /// <returns>A comma-separated string including street, building, postal code, and country.</returns>
        public override string ToString()
        {
            string building = string.IsNullOrEmpty(Letter) ? StreetNumber : $"{StreetNumber}{Letter}";

            return $"{StreetName} {building}, {PostalCode}, {CountryCode}";
        }
    }
}

using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase.Bookings.Queries
{
    /// <summary>
    /// Detailed booking information for the receptionist's context window.
    /// Includes financial data using the Money Value Object.
    /// </summary>
    /// <param name="Id">Unique identifier of the booking.</param>
    /// <param name="TimeSlot">The reserved time interval.</param>
    /// <param name="TreatmentName">The name of the treatment.</param>
    /// <param name="PractitionerFullName">The full name of the assigned provider.</param>
    /// <param name="CustomerFullName">The full name of the customer.</param>
    /// <param name="BasePrice">The price of the treatment.</param>
    public record BookingDetailsDto(
        Guid Id,
        TimeSlot TimeSlot,
        string TreatmentName,
        string PractitionerFullName,
        string CustomerFullName,
        Money BasePrice)
    {
        /// <summary>
        /// Gets a formatted price string as per Section 5 of the styleguide.
        /// </summary>
        public string PriceDisplay
        {
            get
            {
                return $"{BasePrice.Amount} {BasePrice.Currency}";
            }
        }
    }
}

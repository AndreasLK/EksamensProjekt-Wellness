using Domain.Enums;
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
    /// <param name="BasePrice">The original price before any discounts.</param>
    /// <param name="FinalPrice">The actual price the customer must pay.</param>
    /// <param name="DiscountReason">Reason for discount.</param>
    public record BookingDetailsDto(
        Guid Id,
        TimeSlot TimeSlot,
        string TreatmentName,
        string PractitionerFullName,
        string CustomerFullName,
        Money BasePrice,
        Money FinalPrice,
        DiscountReason DiscountReason)
    {
        /// <summary>
        /// Value indicating whether the customer received a discount.
        /// </summary>
        public bool HasDiscount
        {
            get
            {
                return FinalPrice.Amount < BasePrice.Amount;
            }
        }


        /// <summary>
        /// Gets the calculated savings. Returns 0 if currencies are mismatched.
        /// </summary>
        public decimal SavingsAmount
        {
            get
            {
                return BasePrice.Amount - FinalPrice.Amount;
            }
        }
    }
}

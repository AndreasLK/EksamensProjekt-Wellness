using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    /// <summary>
    /// Defines the business logic for managing and validating wellness clinic bookings.
    /// </summary>
    public interface IBookingService
    {

        /// <summary>
        /// Validates and creates a new booking if all constraints are met.
        /// </summary>
        /// <param name="practitionerId">The practitioner performing the service.</param>
        /// <param name="clinicId">The target clinic location.</param>
        /// <param name="roomId">The specific room requested.</param>
        /// <param name="start">The requested start time.</param>
        /// <param name="duration">The length of the treatment as a <see cref="TimeSpan"/>.</param>
        /// <returns>True if the booking was created successfully; otherwise, false.</returns>
        Task<bool> CreateBookingAsync(Guid practitionerId, Guid clinicId, Guid roomId, DateTimeOffset start, TimeSpan duration);
    }
}

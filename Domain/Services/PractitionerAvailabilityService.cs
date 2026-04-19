using Domain.Entities;
using Domain.Entities.Clinics;
using Domain.Enums;
using Domain.Interfaces;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class PractitionerAvailabilityService
    {
        private readonly ITravelTimeService _travelTimeService;


        /// <summary>
        /// Initializes a new instance of the <see cref="PractitionerAvailabilityService"/> class.
        /// </summary>
        /// <param name="travelTimeService">Strategy used to calculate travel durations.</param>
        public PractitionerAvailabilityService(
            ITravelTimeService travelTimeService)
        {
            this._travelTimeService = travelTimeService;
        }


        /// <summary>
        /// Validates schedule feasibility considering surrounding appointments.
        /// </summary>
        /// <param name="requestedSlot">Proposed time interval.</param>
        /// <param name="requestedAddress">Proposed clinic location.</param>
        /// <param name="previousEndTime">End time of immediate preceding appointment.</param>
        /// <param name="previousAddress">Location of immediate preceding appointment.</param>
        /// <param name="nextStartTime">Start time of immediate subsequent appointment.</param>
        /// <param name="nextAddress">Location of immediate subsequent appointment.</param>
        /// <returns>True if travel transitions are physically possible; otherwise, false.</returns>
        public async Task<bool> IsScheduleFeasibleAsync(
            TimeSlot requestedSlot,
            Address requestedAddress,
            DateTimeOffset? previousEndTime,
            Address? previousAddress,
            DateTimeOffset? nextStartTime,
            Address? nextAddress)
        {

            //Check if practitioner can get from previous booking to requested booking
            if (previousEndTime.HasValue && previousAddress != null)
            {
                if (! await this.CanTravelInTimeAsync(
                    start: previousAddress,
                    end: requestedAddress,
                    departure: previousEndTime.Value,
                    arrival: requestedSlot.StartDateTime,
                    anchor: TimeAnchor.Arrival))
                {
                    return false;
                }
            }

            //Check if practitioner can get from requested booking to next booking
            if (nextStartTime.HasValue && nextAddress != null)
            {
                if (! await this.CanTravelInTimeAsync(
                    start: requestedAddress,
                    end: nextAddress,
                    departure: requestedSlot.EndDateTime,
                    arrival: nextStartTime.Value,
                    anchor: TimeAnchor.Departure)

                    )
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// Evaluates if travel between two points is possible within the allotted time window.
        /// </summary>
        private async Task<bool> CanTravelInTimeAsync(
            Address start,
            Address end,
            DateTimeOffset departure,
            DateTimeOffset arrival,
            TimeAnchor anchor)
        {
            // Skips calculation if locations are identical.
            if (start.Equals(end))
            {
                return true;
            }

            DateTimeOffset referenceTime = (anchor == TimeAnchor.Arrival) ? arrival : departure;

            TimeSpan travelDuration = await this._travelTimeService.GetTravelDurationAsync(
                start,
                end,
                anchor,
                referenceTime);

            return departure.Add(travelDuration) <= arrival;
        }
    }
}

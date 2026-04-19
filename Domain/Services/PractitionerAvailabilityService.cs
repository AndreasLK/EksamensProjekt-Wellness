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
    /// <summary>
    /// Enforces physical travel constraints and identifies availability windows for practitioners.
    /// </summary>
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
        /// <returns>A result containing success status and calculated temporal buffers.</returns>
        public async Task<FeasibilityResult> CheckFeasibilityAsync(
            TimeSlot requestedSlot,
            Address requestedAddress,
            DateTimeOffset? previousEndTime,
            Address? previousAddress,
            DateTimeOffset? nextStartTime,
            Address? nextAddress)
        {
            DateTimeOffset? earliestArrival = null;
            DateTimeOffset? latestDeparture = null;

            //Check if practitioner can get from previous booking to requested booking
            if (previousEndTime.HasValue && previousAddress != null)
            {
                TimeSpan travelTime = await this.CalculateTravelDurationAsync(
                    start: previousAddress,
                    end: requestedAddress,
                    time: previousEndTime.Value,
                    anchor: TimeAnchor.Arrival);
                earliestArrival = previousEndTime.Value.Add(travelTime);
            }

            //Check if practitioner can get from requested booking to next booking
            if (nextStartTime.HasValue && nextAddress != null)
            {
                TimeSpan travelTime = await this.CalculateTravelDurationAsync(
                    start: requestedAddress,
                    end: nextAddress,
                    time: nextStartTime.Value,
                    anchor: TimeAnchor.Departure);
                latestDeparture = nextStartTime.Value.Subtract(travelTime);
            }

            bool isFeasible = true;

            if (earliestArrival.HasValue && earliestArrival > requestedSlot.StartDateTime)
            {
                isFeasible = false;
            }

            if (latestDeparture.HasValue && requestedSlot.EndDateTime > latestDeparture)
            {
                isFeasible = false;
            }

            return isFeasible
                ? FeasibilityResult.Success()
                : FeasibilityResult.Failure(
                    earliestPossibleArrival: earliestArrival, 
                    latestPossibleDeparture: latestDeparture);
        }


        /// <summary>
        /// Resolves the physical travel duration between two addresses.
        /// </summary>
        private async Task<TimeSpan> CalculateTravelDurationAsync(
            Address start,
            Address end,
            DateTimeOffset time,
            TimeAnchor anchor
            )
        {
            if (start.Equals(end))
            {
                return TimeSpan.Zero;
            }

            return await this._travelTimeService.GetTravelDurationAsync(
                origin: start,
                destination: end,
                anchor: anchor,
                referenceTime: time);
        }
    }
}

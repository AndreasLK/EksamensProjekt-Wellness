using Domain.Entities;
using Domain.Common;
using System.Linq.Expressions;

namespace Domain.Specifications
{
    public class PractitionerPriorBookingSpecification : Specification<Booking>
    {
        
        private readonly Guid _practitionerId;
        private readonly DateTimeOffset _searchThreshold;

        /// <summary>
        /// Initializes specification with practitioner identity and prior-time threshold.
        /// </summary>
        /// <param name="practitionerId">Target practitioner identifier.</param>
        /// <param name="searchThreshold">Cutoff time for prior appointments.</param>
        public PractitionerPriorBookingSpecification(
            Guid practitionerId, 
            DateTimeOffset searchThreshold)
        {
            _practitionerId = practitionerId;
            _searchThreshold = searchThreshold;

            ApplyOrderByDescending(booking => booking.TimeSlot.EndDateTime);
            ApplyTake(QueryConstants.SINGLE_RESULT_LIMIT);
        }


        /// <summary>
        /// Translates criteria into LINQ expression for database evaluation.
        /// </summary>
        public override Expression<Func<Booking, bool>> ToExpression()
        {
            return booking => booking.PractitionerId == _practitionerId
                              && booking.TimeSlot.EndDateTime <= _searchThreshold;
        }
    }
}

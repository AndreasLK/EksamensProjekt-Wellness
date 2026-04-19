using Domain.Common;
using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Specifications
{
    public class PractitionerNextBookingSpecification : Specification<Booking>
    {
        private readonly Guid _practitionerId;
        private readonly DateTimeOffset _searchThreshold;

        public PractitionerNextBookingSpecification(
            Guid practitionerId,
            DateTimeOffset searchThreshold)
        {
            this._practitionerId = practitionerId;
            this._searchThreshold = searchThreshold;

            ApplyOrderBy(booking => booking.TimeSlot.StartDateTime);
            ApplyTake(QueryConstants.SINGLE_RESULT_LIMIT);
        }


        /// <summary>
        /// Translates criteria into LINQ expression for database evaluation.
        /// </summary>
        public override Expression<Func<Booking, bool>> ToExpression()
        {
            return booking => booking.PractitionerId == _practitionerId
                              && booking.TimeSlot.StartDateTime >= _searchThreshold;
        }
    }
}

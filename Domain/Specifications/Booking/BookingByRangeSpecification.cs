using Domain.ValueObjects;
using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Specifications.Booking
{
    public class BookingByRangeSpecification : Specification<Entities.Booking>
    {
        private readonly TimeSlot _range;
        private readonly List<Guid> _roomIds;

        public BookingByRangeSpecification(
            TimeSlot range, 
            List<Guid> roomIds)
        {
            this._range = range;
            this._roomIds = roomIds;

            ApplyOrderBy(b => b.TimeSlot.StartDateTime);
        }


        public override Expression<Func<Entities.Booking, bool>> ToExpression()
        {
            return booking => _roomIds.Contains(booking.RoomId) &&
                              booking.TimeSlot.StartDateTime >= _range.StartDateTime &&
                              booking.TimeSlot.EndDateTime <= _range.EndDateTime;
        }
    }
}

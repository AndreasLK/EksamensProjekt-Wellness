using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Specifications.Booking
{
    public class RoomOverlapSpecification : Specification<Entities.Booking>
    {
        private readonly Guid _roomId;
        private readonly TimeSlot _proposedSlot;

        public RoomOverlapSpecification(Guid roomId, TimeSlot proposedSlot)
        {
            this._roomId = roomId;
            this._proposedSlot = proposedSlot;
        }

        public override Expression<Func<Entities.Booking, bool>> ToExpression()
        {
            return b => b.RoomId == _roomId &&
                        b.TimeSlot.StartDateTime < _proposedSlot.EndDateTime &&
                        _proposedSlot.StartDateTime < b.TimeSlot.EndDateTime;
        }
    }
}

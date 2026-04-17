using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Models
{
    public class Lease: Entity
    {
        private TimeSlot timeSlot { get; }
        Lease(TimeSlot timeSlot) : base() {
            this.timeSlot = timeSlot;
        }
    }
}

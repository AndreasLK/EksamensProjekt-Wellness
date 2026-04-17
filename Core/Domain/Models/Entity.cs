using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Models
{
    public class Entity
    {
        public Guid id { get; }
        public Entity(Guid? id = null) => this.id = id ?? Guid.NewGuid();
    }
}

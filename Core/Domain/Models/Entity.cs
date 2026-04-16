using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Models
{
    public class Entity
    {
        public Guid Id { get; }
        public Entity(Guid? id = null) => Id = id ?? Guid.NewGuid();
    }
}

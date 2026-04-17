namespace Core.Domain.Entities
{
    /// <summary>
    /// Base class for all Domain Entities. 
    /// Ensures a consistent Identity (Id) across the domain.
    /// </summary>
    public class Entity
    {
        public Guid Id { get; }
        public Entity(Guid? id = null) => this.Id = id ?? Guid.NewGuid();
    }
}

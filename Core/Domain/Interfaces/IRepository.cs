using Core.Domain.Entities;

namespace Core.Domain.Interfaces
{
    /// <summary>
    /// A generic repository interface for managing Domain Entities.
    /// Follows the CRUD pattern for persistent storage.
    /// </summary>
    /// <typeparam name="T">The type of Entity, must inherit from <see cref="Entity"/>.</typeparam>
    public interface IRepository<T> where T : Entity
    {
        /// <summary>Persists a new entity to the data store.</summary>
        /// <returns>The created entity with its assigned ID.</returns>
        Task<T> CreateAsync(T item);

        /// <summary>Retrieves an entity by its unique identifier.</summary>
        /// <returns>The entity if found; otherwise, null.</returns>
        Task<T?> GetByIdAsync(Guid id);

        /// <summary>Retrieves all entities of type T from the data store.</summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>Updates an existing entity's data.</summary>
        Task UpdateAsync(T item);

        /// <summary>Removes an entity from the data store by its ID.</summary>
        Task DeleteAsync(Guid id);
    }
}

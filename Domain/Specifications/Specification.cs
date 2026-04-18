using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Specifications
{
    public abstract class Specification<T>
    {
        /// <summary>
        /// This is the 'Blueprint' of our search. It returns a LINQ expression 
        /// that the database driver (EF Core) can translate directly into SQL.
        /// </summary>
        /// <returns>A piece of C# code that acts as a filter (e.g., x => x.Id == 5).</returns>
        public abstract Expression<Func<T, bool>> ToExpression();



        /// <summary>
        /// Allows us to test the rule against a single object in memory without a database.
        /// This is the secret to making our search logic unit-testable.
        /// </summary>
        /// <param name="entity">The specific object we want to check against the rule.</param>
        /// <returns>True if the object matches the rule; otherwise, false.</returns>
        public bool IsSatisiedBy(T entity)
        {
            var predicate = ToExpression().Compile();
            return predicate(entity);
        }
    }
}

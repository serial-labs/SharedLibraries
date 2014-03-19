using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SerialLabs.Data
{
    /// <summary>
    /// A contract for a repository pattern in a generic way
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    /// <typeparam name="TEntity">The type of the entity identifier</typeparam>
    /// <see cref="http://martinfowler.com/eaaCatalog/repository.html"/>
    public interface IGenericRepository<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        /// <summary>
        /// Gets a list of entities from the underlying data context.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        /// Gets a single entity from the underlying data context, matching the given identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetItem(TId id);

        /// <summary>
        /// Inserts a new entity into the underlying data context.
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);

        /// <summary>
        /// Updates an entity in the underlying data context.
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes an entity from the underlying data context.
        /// </summary>
        /// <param name="id"></param>
        void Delete(TId id);

        /// <summary>
        /// Deletes an entity from the underlying data context.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
    }
}

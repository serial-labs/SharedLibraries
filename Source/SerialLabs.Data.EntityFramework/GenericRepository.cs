using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.Data.EntityFramework
{
    /// <summary>
    /// Implements the repository pattern in a generic way
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <see cref="http://martinfowler.com/eaaCatalog/repository.html"/>
    public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        /// <summary>
        /// Context
        /// </summary>
        protected DbContext _context;
        /// <summary>
        /// DbSet
        /// </summary>
        protected DbSet<TEntity> _dbSet;

        /// <summary>
        /// Creates a new instance of the <see cref="GenericRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(DbContext context)
        {
            Guard.ArgumentNotNull<DataException>(context, "context");
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Gets a list of entities from the underlying data context.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var property in includeProperties.Split(
                new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>
        /// Gets a single entity from the underlying data context, matching the given identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetItem(TId id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Inserts a new entity into the underlying data context.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Updates an entity in the underlying data context.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes an entity from the underlying data context.
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(TId id)
        {
            TEntity entity = _dbSet.Find(id);

        }

        /// <summary>
        /// Deletes an entity from the underlying data context.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }
    }
}

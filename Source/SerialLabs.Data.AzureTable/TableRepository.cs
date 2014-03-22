using Microsoft.WindowsAzure.Storage.Table;
using SerialLabs.Data.AzureTable.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SerialLabs.Data.AzureTable
{
    /// <summary>
    /// A generic repository for Azure Table
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class TableRepository<TEntity>
        where TEntity : ITableEntity, new()
    {
        protected TableStorageConfiguration _configuration;
        protected TableStorageWriter _writer;
        protected TableStorageReader _reader;

        /// <summary>
        /// Create a new instance of the <see cref="TableRepository"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        public TableRepository(TableStorageConfiguration configuration)
        {
            TableStorageConfiguration.ValidateConfiguration(configuration);
            _writer = new TableStorageWriter(configuration);
            _reader = new TableStorageReader(configuration);
        }

        /// <summary>
        /// Get a collection of <see cref="{TEntity}"/> from the underlying table storage.
        /// </summary>
        /// <param name="filter">A filter predicat</param>
        /// <param name="orderBy">An ordering definition</param>
        /// <returns>A collection of <see cref="{TEntity}"/></returns>
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a single <see cref="{TEntity}"/> from the underlying table storage.
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <param name="rowKey"></param>
        /// <returns></returns>
        public virtual TEntity GetItem(string partitionKey, string rowKey)
        {
            Guard.ArgumentNotNullOrWhiteSpace(rowKey, "rowKey");

            //ICollection<TEntity> entries = Task.FromResult<ICollection<TEntity>>(_reader.ExecuteAsync(new EntryForPartitionAndKey<TEntity>(partitionKey, rowKey)));
            throw new NotImplementedException();
        }

        /// <summary>
        /// Insert a new entity into the underlying table storage.
        /// </summary>
        /// <param name="entity"></param>

        public virtual void Insert(TEntity entity)
        {
            Guard.ArgumentNotNull(entity, "entity");

            _writer.Insert(entity);
            _writer.Execute();
        }

        /// <summary>
        /// Asynchronously inserts a new entity into the underlying table storage.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task InsertAsync(TEntity entity)
        {
            Guard.ArgumentNotNull(entity, "entity");

            _writer.Insert(entity);
            await _writer.ExecuteAsync();
        }

        /// <summary>
        /// Update an existing entity from the underlying table storage.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity)
        {
            Guard.ArgumentNotNull(entity, "entity");

            _writer.Merge(entity);
            _writer.Execute();
        }

        /// <summary>
        /// Asynchronously updates an existing entity from the underlying table storage.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            Guard.ArgumentNotNull(entity, "entity");

            _writer.Merge(entity);
            await _writer.ExecuteAsync();
        }

        /// <summary>
        /// Delete an existing entity from the underlying table storage.
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <param name="rowKey"></param>
        public virtual void Delete(string partitionKey, string rowKey)
        {
            Guard.ArgumentNotNullOrWhiteSpace(partitionKey, "partitionKey");
            Guard.ArgumentNotNullOrWhiteSpace(rowKey, "rowKey");

            TableEntity entity = new TableEntity(partitionKey, rowKey);
            entity.ETag = "*";
            _writer.Delete(entity);
            _writer.Execute();
        }

        /// <summary>
        /// Asynchronously deletes an entity from the underlying table storage.
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <param name="rowKey"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(string partitionKey, string rowKey)
        {
            Guard.ArgumentNotNullOrWhiteSpace(partitionKey, "partitionKey");
            Guard.ArgumentNotNullOrWhiteSpace(rowKey, "rowKey");

            TableEntity entity = new TableEntity(partitionKey, rowKey);
            entity.ETag = "*";
            _writer.Delete(entity);
            await _writer.ExecuteAsync();
        }

        /// <summary>
        /// Delete an existing entity from the underlying table storage.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(TEntity entity)
        {
            Guard.ArgumentNotNull(entity, "entity");

            entity.ETag = "*";
            _writer.Delete(entity);
            _writer.Execute();
        }

        /// <summary>
        /// Asynchronously deletes an entity from the underlying table storage.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(TEntity entity)
        {
            Guard.ArgumentNotNull(entity, "entity");

            entity.ETag = "*";
            _writer.Delete(entity);
            await _writer.ExecuteAsync();
        }
    }
}

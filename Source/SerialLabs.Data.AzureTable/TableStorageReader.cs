using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace SerialLabs.Data.AzureTable
{
    public class TableStorageReader : TableStorageProvider
    {
        public TableStorageReader(string tableName, string storageConnectionString)
            : base(TableStorageConfiguration.CreateDefault(tableName, storageConnectionString))
        { }
        public TableStorageReader(TableStorageConfiguration configuration)
            : base(configuration)
        { }

        public async Task<ICollection<TEntity>> ExecuteAsync<TEntity>(ITableStorageQuery<TEntity> query)
            where TEntity : ITableEntity, new()
        {
            Guard.ArgumentNotNull(query, "query");

            return await Task.Run(() =>
            {
                if (_configuration.CacheItemPolicy == null)
                    return query.Execute(_table);

                TableStorageQueryCache<TEntity> cachedQuery = new TableStorageQueryCache<TEntity>(query, _configuration.CacheItemPolicy);
                return cachedQuery.Execute(_table);
            });
        }

        public TableStorageReader WithCache(CacheItemPolicy cacheItemPolicy = null)
        {
            if (cacheItemPolicy == null)
                cacheItemPolicy = TableStorageConfiguration.DefaultQueryCacheItemPolicy();
            _configuration.CacheItemPolicy = cacheItemPolicy;
            return this;
        }

        public static TableStorageReader Table(TableStorageConfiguration configuration)
        {
            return new TableStorageReader(configuration);
        }
        public static TableStorageReader Table(string tableName, string storageConnectionString)
        {
            return new TableStorageReader(TableStorageConfiguration.CreateDefault(tableName, storageConnectionString));
        }
    }
}

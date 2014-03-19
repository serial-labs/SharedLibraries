using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.Data.AzureTable.Queries
{
    /// <summary>
    /// Select the entities matching a given partition key
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntriesForPartition<TEntity> : TableStorageQuery<TEntity>
        where TEntity : ITableEntity, new()
    {

        private readonly string _partition;
        private readonly string _cacheKey;

        public override string UniqueIdentifier
        {
            get { return _cacheKey; }
        }


        public EntriesForPartition(string partition)
            : base()
        {
            Guard.ArgumentNotNullOrWhiteSpace(partition, "partition");

            _partition = partition;
            _cacheKey = CreateCacheKey();
        }

        protected override TableQuery<TEntity> CreateQuery()
        {
            //string filterCondition = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partition.ToUpperInvariant());
            string filterCondition = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partition);
            TableQuery<TEntity> query = new TableQuery<TEntity>();
            return query.Where(filterCondition);
        }

        protected string CreateCacheKey()
        {
            return String.Format(CultureInfo.InvariantCulture,
                    "EntriesForPartitionQuery-{0}", _partition);
        }
    }
}

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
    /// Select the entity matching a given partition key and row key
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntryForPartitionAndKey<TEntity> : TableStorageQuery<TEntity>
        where TEntity : ITableEntity, new()
    {

        private readonly string _partition;
        private readonly string _row;
        private readonly string _cacheKey;

        public override string UniqueIdentifier
        {
            get { return _cacheKey; }
        }

        public EntryForPartitionAndKey(string partition, string row)
            : base()
        {
            Guard.ArgumentNotNullOrWhiteSpace(partition, "partition");
            Guard.ArgumentNotNullOrWhiteSpace(row, "row");

            _partition = partition;
            _row = row;
            _cacheKey = CreateCacheKey();
        }

        protected override TableQuery<TEntity> CreateQuery()
        {
            string filterCondition = TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partition),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, _row)
            );

            TableQuery<TEntity> query = new TableQuery<TEntity>();
            return query.Where(filterCondition);
        }

        protected string CreateCacheKey()
        {
            return String.Format(CultureInfo.InvariantCulture,
                    "EntryForPartitionAndKey-{0}--{1}", _partition, _row);
        }
    }
}

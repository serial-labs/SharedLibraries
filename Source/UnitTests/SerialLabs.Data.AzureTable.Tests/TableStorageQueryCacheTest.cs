using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SerialLabs.Data.AzureTable.Tests
{
    [TestClass]
    public class TableStorageQueryCacheTest
    {
        [TestMethod]
        public void TableStorageQueryCache_Execute()
        {
            MemoryCache cache = new MemoryCache(DefaultCacheName);
            TableStorageQueryCache query = TableStorageQueryCache(ITableStorageQuery < TEntity > query);
        }
    }
}

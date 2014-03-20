using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Caching;

namespace SerialLabs.Data.AzureTable.Tests
{
    [TestClass]
    public class TableStorageQueryCacheTest
    {
        [TestMethod]
        public void TableStorageQueryCache_Execute()
        {
            ObjectCache cache = new MemoryCache("");
            //TableStorageQueryCache query = TableStorageQueryCache(query);
        }
    }
}

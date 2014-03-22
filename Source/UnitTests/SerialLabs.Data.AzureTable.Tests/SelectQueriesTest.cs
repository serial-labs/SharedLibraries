using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Table;
using SerialLabs.Data.AzureTable.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SerialLabs.Data.AzureTable.Tests
{
    [TestClass]
    public class SelectQueriesTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateSelectEntriesForPartition_NoPartition()
        {
            new EntriesForPartition<DynamicEntity>("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateSelectTopEntriesForPartition_NoPartition()
        {
            new TopEntriesForPartition<DynamicEntity>("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateSelectEntryForPartitionAndKey_NoPartition()
        {
            new EntryForPartitionAndKey<DynamicEntity>("", "aa");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateSelectEntryForPartitionAndKey_NoRow()
        {
            new EntryForPartitionAndKey<DynamicEntity>("aa", "");
        }

        [TestMethod]

        public void CreateSelectEntriesForPartition_Success()
        {
            EntriesForPartition<DynamicEntity> query = new EntriesForPartition<DynamicEntity>(Helper.RandomPartitionKey);
            Assert.IsNotNull(query);
        }

        [TestMethod]

        public void CreateSelectTopEntriesForPartition_Success()
        {
            TopEntriesForPartition<DynamicEntity> query = new TopEntriesForPartition<DynamicEntity>(Helper.RandomPartitionKey);
            Assert.IsNotNull(query);
        }

        [TestMethod]

        public void CreateSelectEntryForPartitionAndKey_Success()
        {
            EntryForPartitionAndKey<DynamicEntity> query = new EntryForPartitionAndKey<DynamicEntity>(Helper.RandomPartitionKey, Helper.RandomRowKey);
            Assert.IsNotNull(query);
        }

        [TestMethod]
        public void Selects_CreateCache_Success()
        {
            string partitionKey = Helper.RandomPartitionKey;
            string rowKey = Helper.RandomRowKey;

            FakeEntryForPartitionAndKey query1 = new FakeEntryForPartitionAndKey(partitionKey, rowKey);
            Assert.IsNotNull(query1);
            Assert.IsNotNull(query1.CreateCacheKey());

            FakeTopEntriesForPartition query2 = new FakeTopEntriesForPartition(partitionKey);
            Assert.IsNotNull(query2);
            Assert.IsNotNull(query2.CreateCacheKey());

            FakeEntriesForPartition query3 = new FakeEntriesForPartition(partitionKey);
            Assert.IsNotNull(query3);
            Assert.IsNotNull(query3.CreateCacheKey());

        }

        [TestMethod]
        public void Selects_CreateQuery_Success()
        {
            string partitionKey = Helper.RandomPartitionKey;
            string rowKey = Helper.RandomRowKey;

            FakeEntryForPartitionAndKey query1 = new FakeEntryForPartitionAndKey(partitionKey, rowKey);
            Assert.IsNotNull(query1);
            Assert.IsNotNull(query1.CreateQuery());

            FakeTopEntriesForPartition query2 = new FakeTopEntriesForPartition(partitionKey);
            Assert.IsNotNull(query2);
            Assert.IsNotNull(query2.CreateQuery());

            FakeEntriesForPartition query3 = new FakeEntriesForPartition(partitionKey);
            Assert.IsNotNull(query3);
            Assert.IsNotNull(query3.CreateQuery());

        }

        [TestMethod]
        public async Task SelectTopEntriesForPartition_InsertAndRead()
        {
            string table = Helper.DailyTableName;
            string partitionKey = Helper.RandomPartitionKey;
            string rowKey = Helper.RandomRowKey;
            DynamicEntity entity = Helper.CreateFakeDynamicPerson(partitionKey, rowKey);

            EntryForPartitionAndKey<DynamicEntity> query =
                new EntryForPartitionAndKey<DynamicEntity>(partitionKey, rowKey);

            TopEntriesForPartition<DynamicEntity> topEntries = new TopEntriesForPartition<DynamicEntity>(partitionKey);

            TableStorageWriter writer = new TableStorageWriter(table, Helper.StorageConnectionString);
            TableStorageReader reader = new TableStorageReader(table, Helper.StorageConnectionString);

            writer.Insert<DynamicEntity>(entity);
            writer.Execute();

            List<DynamicEntity> results = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(topEntries);
            Assert.AreEqual(1, results.Count);
            Comparers.AssertCompare(entity, results[0]);
        }

        [TestMethod]
        public async Task SelectEntriesForPartition_InsertAndRead()
        {
            string table = Helper.DailyTableName;
            string partitionKey = Helper.RandomPartitionKey;
            string rowKey = Helper.RandomRowKey;
            DynamicEntity entity = Helper.CreateFakeDynamicPerson(partitionKey, rowKey);

            EntriesForPartition<DynamicEntity> query = new EntriesForPartition<DynamicEntity>(partitionKey);

            TableStorageWriter writer = new TableStorageWriter(table, Helper.StorageConnectionString);
            TableStorageReader reader = new TableStorageReader(table, Helper.StorageConnectionString);

            writer.Insert<DynamicEntity>(entity);
            writer.Execute();

            List<DynamicEntity> results = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(query);
            Assert.AreEqual(1, results.Count);
            Comparers.AssertCompare(entity, results[0]);
        }

        [TestMethod]
        public async Task SelectEntryForPartitionAndKey_InsertAndRead()
        {
            string table = Helper.DailyTableName;
            string partitionKey = Helper.RandomPartitionKey;
            string rowKey = Helper.RandomRowKey;
            DynamicEntity entity = Helper.CreateFakeDynamicPerson(partitionKey, rowKey);

            EntryForPartitionAndKey<DynamicEntity> query = new EntryForPartitionAndKey<DynamicEntity>(partitionKey, rowKey);

            TableStorageWriter writer = new TableStorageWriter(table, Helper.StorageConnectionString);
            TableStorageReader reader = new TableStorageReader(table, Helper.StorageConnectionString);

            writer.Insert<DynamicEntity>(entity);
            writer.Execute();

            List<DynamicEntity> results = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(query);
            Assert.AreEqual(1, results.Count);
            Comparers.AssertCompare(entity, results[0]);
        }

        [TestMethod]
        public async Task SelectEntryForPartitionAndKey_InsertAndRead_WithCache()
        {
            string table = Helper.DailyTableName;
            string partitionKey = Helper.RandomPartitionKey;
            string rowKey = Helper.RandomRowKey;
            DynamicEntity entity = Helper.CreateFakeDynamicPerson(partitionKey, rowKey);

            EntryForPartitionAndKey<DynamicEntity> query = new EntryForPartitionAndKey<DynamicEntity>(partitionKey, rowKey);


            TableStorageWriter writer = new TableStorageWriter(table, Helper.StorageConnectionString);
            TableStorageReader reader = new TableStorageReader(table, Helper.StorageConnectionString).WithCache();
            reader.WithCache();

            writer.Insert<DynamicEntity>(entity);
            writer.Execute();

            List<DynamicEntity> results = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(query);
            Assert.AreEqual(1, results.Count);
            Comparers.AssertCompare(entity, results[0]);

            // read from cache
            results = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(query);
            Assert.AreEqual(1, results.Count);
            Comparers.AssertCompare(entity, results[0]);
        }

        [TestMethod]
        public async Task SelectEntryForPartitionAndKey_InsertAndRead_WithCacheAfterChange()
        {
            string table = Helper.DailyTableName;
            string partitionKey = Helper.RandomPartitionKey;
            string rowKey = Helper.RandomRowKey;
            DynamicEntity entity = Helper.CreateFakeDynamicPerson(partitionKey, rowKey);

            EntryForPartitionAndKey<DynamicEntity> query = new EntryForPartitionAndKey<DynamicEntity>(partitionKey, rowKey);


            TableStorageWriter writer = new TableStorageWriter(table, Helper.StorageConnectionString);
            TableStorageReader reader = TableStorageReader.Table(table, Helper.StorageConnectionString);
            reader.WithCache();

            writer.Insert<DynamicEntity>(entity);
            writer.Execute();

            List<DynamicEntity> results = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(query);
            Assert.AreEqual(1, results.Count);
            Comparers.AssertCompare(entity, results[0]);

            entity.Set("Number", 654);
            writer.InsertOrReplace<DynamicEntity>(entity);
            writer.Execute();

            // read from cache
            List<DynamicEntity> results2 = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(query);
            Assert.AreEqual(1, results2.Count);
            Comparers.AssertCompare(entity, results2[0]);
        }

        [TestMethod]
        public async Task SelectEntryForPartitionAndKey_InsertAndRead_NotExistant()
        {
            string table = Helper.DailyTableName;
            string partitionKey = Helper.RandomPartitionKey;
            string rowKey = Helper.RandomRowKey;
            DynamicEntity entity = Helper.CreateFakeDynamicPerson(partitionKey, rowKey);



            TableStorageWriter writer = new TableStorageWriter(table, Helper.StorageConnectionString);
            TableStorageReader reader = TableStorageReader.Table(table, Helper.StorageConnectionString);

            writer.Insert<DynamicEntity>(entity);
            writer.Execute();

            EntryForPartitionAndKey<DynamicEntity> query = new EntryForPartitionAndKey<DynamicEntity>(partitionKey, Guid.NewGuid().ToString()); // row key should be unknown

            List<DynamicEntity> results = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(query);
            Assert.AreEqual(0, results.Count);
        }

        /*
         * Fake classes
         * */

        class FakeEntryForPartitionAndKey : EntryForPartitionAndKey<DynamicEntity>
        {
            public FakeEntryForPartitionAndKey(string partitionKey, string rowKey)
                : base(partitionKey, rowKey) { }
            public new string CreateCacheKey() { return base.CreateCacheKey(); }
            public new TableQuery<DynamicEntity> CreateQuery() { return base.CreateQuery(); }
        }


        class FakeEntriesForPartition : EntriesForPartition<DynamicEntity>
        {
            public FakeEntriesForPartition(string partitionKey)
                : base(partitionKey) { }
            public new string CreateCacheKey() { return base.CreateCacheKey(); }
            public new TableQuery<DynamicEntity> CreateQuery() { return base.CreateQuery(); }
        }


        class FakeTopEntriesForPartition : TopEntriesForPartition<DynamicEntity>
        {
            public FakeTopEntriesForPartition(string partitionKey)
                : base(partitionKey) { }
            public new string CreateCacheKey() { return base.CreateCacheKey(); }
            public new TableQuery<DynamicEntity> CreateQuery() { return base.CreateQuery(); }
        }
    }
}

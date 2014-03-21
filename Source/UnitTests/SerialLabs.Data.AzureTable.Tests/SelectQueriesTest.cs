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
            EntriesForPartition<DynamicEntity> a = new EntriesForPartition<DynamicEntity>("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateSelectTopEntriesForPartition_NoPartition()
        {
            TopEntriesForPartition<DynamicEntity> a = new TopEntriesForPartition<DynamicEntity>("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateSelectEntryForPartitionAndKey_NoPartition()
        {
            EntryForPartitionAndKey<DynamicEntity> a = new EntryForPartitionAndKey<DynamicEntity>("", "aa");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateSelectEntryForPartitionAndKey_NoRow()
        {
            EntryForPartitionAndKey<DynamicEntity> a = new EntryForPartitionAndKey<DynamicEntity>("aa", "");
        }

        [TestMethod]

        public void CreateSelectEntriesForPartition_Success()
        {
            EntriesForPartition<DynamicEntity> a = new EntriesForPartition<DynamicEntity>(Helper.partitionKey);
            Assert.IsNotNull(a);
        }

        [TestMethod]

        public void CreateSelectTopEntriesForPartition_Success()
        {
            TopEntriesForPartition<DynamicEntity> a = new TopEntriesForPartition<DynamicEntity>(Helper.partitionKey);
            Assert.IsNotNull(a);
        }

        [TestMethod]

        public void CreateSelectEntryForPartitionAndKey_Success()
        {
            EntryForPartitionAndKey<DynamicEntity> a = new EntryForPartitionAndKey<DynamicEntity>(Helper.partitionKey, Helper.Person.defaultId);
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Selects_CreateCache_Success()
        {
            FakeEntryForPartitionAndKey a1 = new FakeEntryForPartitionAndKey(Helper.partitionKey, Helper.Person.defaultId);
            Assert.IsNotNull(a1);
            Assert.IsNotNull(a1.CreateCacheKey());

            FakeTopEntriesForPartition a2 = new FakeTopEntriesForPartition(Helper.partitionKey);
            Assert.IsNotNull(a2);
            Assert.IsNotNull(a2.CreateCacheKey());

            FakeEntriesForPartition a3 = new FakeEntriesForPartition(Helper.partitionKey);
            Assert.IsNotNull(a3);
            Assert.IsNotNull(a3.CreateCacheKey());

        }

        [TestMethod]
        public void Selects_CreateQuery_Success()
        {
            FakeEntryForPartitionAndKey a1 = new FakeEntryForPartitionAndKey(Helper.partitionKey, Helper.Person.defaultId);
            Assert.IsNotNull(a1);
            Assert.IsNotNull(a1.CreateQuery());

            FakeTopEntriesForPartition a2 = new FakeTopEntriesForPartition(Helper.partitionKey);
            Assert.IsNotNull(a2);
            Assert.IsNotNull(a2.CreateQuery());

            FakeEntriesForPartition a3 = new FakeEntriesForPartition(Helper.partitionKey);
            Assert.IsNotNull(a3);
            Assert.IsNotNull(a3.CreateQuery());

        }

        [TestMethod]
        public async Task SelectTopEntriesForPartition_InsertAndRead()
        {
            string table = Helper.NewTableName();

            TopEntriesForPartition<DynamicEntity> topEntries = new TopEntriesForPartition<DynamicEntity>(Helper.partitionKey);

            TableStorageWriter writer = new TableStorageWriter(table, Helper.ConnectionStringForTest);
            TableStorageReader reader = TableStorageReader.Table(table, Helper.ConnectionStringForTest);

            writer.Insert<DynamicEntity>(Helper.GeneratePersonDynamicEntity());
            writer.Execute();

            List<DynamicEntity> results = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(topEntries);
            Assert.AreEqual(1, results.Count);
            Assert.IsTrue(Helper.AssertCompare(Helper.GeneratePersonDynamicEntity(), results[0]));                        
        }

        [TestMethod]
        public async Task SelectEntriesForPartition_InsertAndRead()
        {
            string table = Helper.NewTableName();

            EntriesForPartition<DynamicEntity> Entries = new EntriesForPartition<DynamicEntity>(Helper.partitionKey);

            TableStorageWriter writer = new TableStorageWriter(table, Helper.ConnectionStringForTest);
            TableStorageReader reader = TableStorageReader.Table(table, Helper.ConnectionStringForTest);

            writer.Insert<DynamicEntity>(Helper.GeneratePersonDynamicEntity());
            writer.Execute();

            List<DynamicEntity> results = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(Entries);
            Assert.AreEqual(1, results.Count);
            Assert.IsTrue(Helper.AssertCompare(Helper.GeneratePersonDynamicEntity(), results[0]));                        
        }

        [TestMethod]
        public async Task SelectEntryForPartitionAndKey_InsertAndRead()
        {
            string table = Helper.NewTableName();

            EntryForPartitionAndKey<DynamicEntity> Entries = new EntryForPartitionAndKey<DynamicEntity>(Helper.partitionKey, Helper.Person.defaultId);

            TableStorageWriter writer = new TableStorageWriter(table, Helper.ConnectionStringForTest);
            TableStorageReader reader = TableStorageReader.Table(table, Helper.ConnectionStringForTest);

            writer.Insert<DynamicEntity>(Helper.GeneratePersonDynamicEntity());
            writer.Execute();

            List<DynamicEntity> results = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(Entries);
            Assert.AreEqual(1, results.Count);
            Assert.IsTrue(Helper.AssertCompare(Helper.GeneratePersonDynamicEntity(), results[0]));                        
        }

        [TestMethod]
        public async Task SelectEntryForPartitionAndKey_InsertAndRead_WithCache()
        {
            string table = Helper.NewTableName();

            EntryForPartitionAndKey<DynamicEntity> Entries = new EntryForPartitionAndKey<DynamicEntity>(Helper.partitionKey, Helper.Person.defaultId);


            TableStorageWriter writer = new TableStorageWriter(table, Helper.ConnectionStringForTest);
            TableStorageReader reader = TableStorageReader.Table(table, Helper.ConnectionStringForTest);
            reader.WithCache();

            writer.Insert<DynamicEntity>(Helper.GeneratePersonDynamicEntity());
            writer.Execute();

            List<DynamicEntity> results = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(Entries);
            Assert.AreEqual(1, results.Count);
            Assert.IsTrue(Helper.AssertCompare(Helper.GeneratePersonDynamicEntity(), results[0]));            

            // read from cache
            results = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(Entries);
            Assert.AreEqual(1, results.Count);
            Assert.IsTrue(Helper.AssertCompare(Helper.GeneratePersonDynamicEntity(), results[0]));            
        }

        [TestMethod]
        public async Task SelectEntryForPartitionAndKey_InsertAndRead_WithCacheAfterChange()
        {
            string table = Helper.NewTableName();
            DynamicEntity expected = Helper.GeneratePersonDynamicEntity();

            EntryForPartitionAndKey<DynamicEntity> Entries = new EntryForPartitionAndKey<DynamicEntity>(Helper.partitionKey, Helper.Person.defaultId);


            TableStorageWriter writer = new TableStorageWriter(table, Helper.ConnectionStringForTest);
            TableStorageReader reader = TableStorageReader.Table(table, Helper.ConnectionStringForTest);
            reader.WithCache();

            writer.Insert<DynamicEntity>(expected);
            writer.Execute();

            List<DynamicEntity> results = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(Entries);
            Assert.AreEqual(1, results.Count);
            Assert.IsTrue(Helper.AssertCompare(expected, results[0]));            
            

            expected.Set("Number", 654);
            writer.InsertOrReplace<DynamicEntity>(expected);
            writer.Execute();

            // read from cache
            List<DynamicEntity> results2 = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(Entries);
            Assert.AreEqual(1, results2.Count);
            Assert.IsTrue(Helper.AssertCompare(expected, results2[0]));            
        }

        [TestMethod]
        public async Task SelectEntryForPartitionAndKey_InsertAndRead_NotExistant()
        {
            string table = Helper.NewTableName();

            EntryForPartitionAndKey<DynamicEntity> Entries = new EntryForPartitionAndKey<DynamicEntity>(Helper.partitionKey, "25");

            TableStorageWriter writer = new TableStorageWriter(table, Helper.ConnectionStringForTest);
            TableStorageReader reader = TableStorageReader.Table(table, Helper.ConnectionStringForTest);

            writer.Insert<DynamicEntity>(Helper.GeneratePersonDynamicEntity());
            writer.Execute();

            List<DynamicEntity> results = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(Entries);
            Assert.AreEqual(0, results.Count);
        }

        /*
         * Fake classes
         * */

        class FakeEntryForPartitionAndKey : EntryForPartitionAndKey<DynamicEntity>
        {
            public FakeEntryForPartitionAndKey(string a, string b)
                : base(a, b) { }
            public new string CreateCacheKey() { return base.CreateCacheKey(); }
            public new TableQuery<DynamicEntity> CreateQuery() { return base.CreateQuery(); }
        }


        class FakeEntriesForPartition : EntriesForPartition<DynamicEntity>
        {
            public FakeEntriesForPartition(string a)
                : base(a) { }
            public new string CreateCacheKey() { return base.CreateCacheKey(); }
            public new TableQuery<DynamicEntity> CreateQuery() { return base.CreateQuery(); }
        }


        class FakeTopEntriesForPartition : TopEntriesForPartition<DynamicEntity>
        {
            public FakeTopEntriesForPartition(string a)
                : base(a) { }
            public new string CreateCacheKey() { return base.CreateCacheKey(); }
            public new TableQuery<DynamicEntity> CreateQuery() { return base.CreateQuery(); }
        }
    }
}

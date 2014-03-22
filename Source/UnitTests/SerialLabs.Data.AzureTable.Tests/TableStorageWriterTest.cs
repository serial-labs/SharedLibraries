using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;
using SerialLabs.Data.AzureTable.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SerialLabs.Data.AzureTable.Tests
{
    [TestClass]
    public class TableStorageWriterTest
    {


        [TestMethod]
        [ExpectedException(typeof(StorageException))]
        public void TableStorageWriter_DuplicateInsertBatch_WithException()
        {
            string tableName = Helper.DailyTableName;
            string partitionKey = Helper.RandomPartitionKey;
            string rowKey = Helper.RandomRowKey;
            DynamicEntity entity = Helper.CreateFakeDynamicPerson(partitionKey, rowKey);

            FakeStorageWriter fakeStorage = new FakeStorageWriter(tableName, Helper.StorageConnectionString);
            Assert.AreEqual(fakeStorage.PendingOperations, 0);
            fakeStorage.Insert<DynamicEntity>(entity);
            fakeStorage.Insert<DynamicEntity>(entity);
            Assert.AreEqual(fakeStorage.PendingOperations, 2);
            fakeStorage.Execute();
        }

        [TestMethod]
        public async Task TableStorageWriter_InsertAndExecute()
        {
            string tableName = Helper.DailyTableName;
            string partitionKey = Helper.RandomPartitionKey;
            string rowKey = Helper.RandomRowKey;
            DynamicEntity entity = Helper.CreateFakeDynamicPerson(partitionKey, rowKey);

            FakeStorageWriter fakeStorage = new FakeStorageWriter(tableName, Helper.StorageConnectionString);
            Assert.AreEqual(fakeStorage.PendingOperations, 0);
            fakeStorage.Insert<DynamicEntity>(entity);
            Assert.AreEqual(fakeStorage.PendingOperations, 1);

            fakeStorage.Execute();

            TableStorageReader reader = new TableStorageReader(tableName, Helper.StorageConnectionString);
            List<DynamicEntity> actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(entity.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Comparers.AssertCompare(entity, actual[0]);
        }

        [TestMethod]
        public async Task TableStorageWriter_InsertAndDelete()
        {
            string tableName = Helper.DailyTableName;
            string partitionKey = Helper.RandomPartitionKey;
            string rowKey = Helper.RandomRowKey;
            DynamicEntity entity = Helper.CreateFakeDynamicPerson(partitionKey, rowKey);

            FakeStorageWriter fakeStorage = new FakeStorageWriter(tableName, Helper.StorageConnectionString);
            fakeStorage.Insert<DynamicEntity>(entity);

            fakeStorage.Execute();

            TableStorageReader reader = new TableStorageReader(tableName, Helper.StorageConnectionString);
            List<DynamicEntity> actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(entity.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Comparers.AssertCompare(entity, actual[0]);

            fakeStorage.Delete<DynamicEntity>(entity);
            fakeStorage.Execute();

            actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(entity.PartitionKey));

            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        async public Task TableStorageWriter_InsertAndExecuteAsync()
        {
            string tableName = Helper.DailyTableName;
            string partitionKey = Helper.RandomPartitionKey;
            string rowKey = Helper.RandomRowKey;
            DynamicEntity entity = Helper.CreateFakeDynamicPerson(partitionKey, rowKey);

            FakeStorageWriter fakeStorage = new FakeStorageWriter(tableName, Helper.StorageConnectionString);
            Assert.AreEqual(fakeStorage.PendingOperations, 0);
            fakeStorage.Insert<DynamicEntity>(entity);
            Assert.AreEqual(fakeStorage.PendingOperations, 1);

            await fakeStorage.ExecuteAsync();

            TableStorageReader reader = new TableStorageReader(tableName, Helper.StorageConnectionString);
            List<DynamicEntity> actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(entity.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Comparers.AssertCompare(entity, actual[0]);
        }

        [TestMethod]
        public async Task TableStorageWriter_InsertAndMerge()
        {
            string tableName = Helper.DailyTableName;
            string partitionKey = Helper.RandomPartitionKey;
            string rowKey = Helper.RandomRowKey;
            DynamicEntity entity = Helper.CreateFakeDynamicPerson(partitionKey, rowKey);

            FakeStorageWriter fakeStorage = new FakeStorageWriter(tableName, Helper.StorageConnectionString);
            fakeStorage.Insert<DynamicEntity>(entity);
            fakeStorage.Execute();

            TableStorageReader reader = new TableStorageReader(tableName, Helper.StorageConnectionString);
            List<DynamicEntity> actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(entity.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Comparers.AssertCompare(entity, actual[0]);

            entity.Set("Extra", "Extra");
            fakeStorage.Merge<DynamicEntity>(entity);
            fakeStorage.Execute();

            actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(entity.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Comparers.AssertCompare(entity, actual[0]);
        }

        [TestMethod]
        public async Task TableStorageWriter_InsertOrMerge()
        {
            string tableName = Helper.DailyTableName;
            string partitionKey = Helper.RandomPartitionKey;
            string rowKey = Helper.RandomRowKey;
            DynamicEntity entity = Helper.CreateFakeDynamicPerson(partitionKey, rowKey);

            FakeStorageWriter fakeStorage = new FakeStorageWriter(tableName, Helper.StorageConnectionString);
            fakeStorage.Insert<DynamicEntity>(entity);
            fakeStorage.Execute();

            TableStorageReader reader = new TableStorageReader(tableName, Helper.StorageConnectionString);
            List<DynamicEntity> actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(entity.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Comparers.AssertCompare(entity, actual[0]);

            entity.Set("Extra", "Extra");
            fakeStorage.InsertOrMerge<DynamicEntity>(entity);
            fakeStorage.Execute();

            actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(entity.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Comparers.AssertCompare(entity, actual[0]);
        }

        [TestMethod]
        public async Task TableStorageWriter_InsertAndReplace()
        {
            string tableName = Helper.DailyTableName;
            string partitionKey = Helper.RandomPartitionKey;
            string rowKey = Helper.RandomRowKey;
            DynamicEntity entity = Helper.CreateFakeDynamicPerson(partitionKey, rowKey);

            FakeStorageWriter fakeStorage = new FakeStorageWriter(tableName, Helper.StorageConnectionString);
            fakeStorage.Insert<DynamicEntity>(entity);
            fakeStorage.Execute();

            TableStorageReader reader = new TableStorageReader(tableName, Helper.StorageConnectionString);
            List<DynamicEntity> actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(entity.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Comparers.AssertCompare(entity, actual[0]);

            entity.Set("Number", 987);
            fakeStorage.Replace<DynamicEntity>(entity);
            fakeStorage.Execute();

            actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(entity.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Comparers.AssertCompare(entity, actual[0]);
        }

        [TestMethod]
        public async Task TableStorageWriter_InsertOrReplace()
        {
            string tableName = Helper.DailyTableName;
            string partitionKey = Helper.RandomPartitionKey;
            string rowKey = Helper.RandomRowKey;
            DynamicEntity entity = Helper.CreateFakeDynamicPerson(partitionKey, rowKey);

            FakeStorageWriter fakeStorage = new FakeStorageWriter(tableName, Helper.StorageConnectionString);

            fakeStorage.Insert<DynamicEntity>(entity);
            fakeStorage.Execute();

            TableStorageReader reader = new TableStorageReader(tableName, Helper.StorageConnectionString);
            List<DynamicEntity> actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(entity.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Comparers.AssertCompare(entity, actual[0]);

            entity.Set("Number", 987);
            fakeStorage.InsertOrReplace<DynamicEntity>(entity);
            fakeStorage.Execute();

            actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(entity.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Comparers.AssertCompare(entity, actual[0]);
        }

        class FakeStorageWriter : TableStorageWriter
        {
            public FakeStorageWriter(string tableName, string connectionStringSettingName)
                : base(tableName, connectionStringSettingName)
            {

            }
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;
using SerialLabs.Data.AzureTable.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            string tableName = Helper.NewTableName();
            DynamicEntity example = Helper.GeneratePersonDynamicEntity();

            FakeStorageWriter fakeStorage = new FakeStorageWriter(tableName, Helper.ConnectionStringForTest);
            Assert.AreEqual(fakeStorage.PendingOperations, 0);
            fakeStorage.Insert<DynamicEntity>(example);
            fakeStorage.Insert<DynamicEntity>(example);
            Assert.AreEqual(fakeStorage.PendingOperations, 2);
            fakeStorage.Execute();


        }

        [TestMethod]
        public async Task TableStorageWriter_InsertAndExecute()
        {
            string tableName = Helper.NewTableName();
            FakeStorageWriter fakeStorage = new FakeStorageWriter(tableName, Helper.ConnectionStringForTest);
            Assert.AreEqual(fakeStorage.PendingOperations, 0);
            DynamicEntity dynEnt = Helper.GeneratePersonDynamicEntity();
            fakeStorage.Insert<DynamicEntity>(dynEnt);
            Assert.AreEqual(fakeStorage.PendingOperations, 1);

            fakeStorage.Execute();

            TableStorageReader reader = new TableStorageReader(tableName, Helper.ConnectionStringForTest);
            List<DynamicEntity> actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(dynEnt.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(dynEnt, actual[0]);

        }

        [TestMethod]
        public async Task TableStorageWriter_InsertAndDelete()
        {
            string tableName = Helper.NewTableName();
            FakeStorageWriter fakeStorage = new FakeStorageWriter(tableName, Helper.ConnectionStringForTest);
            DynamicEntity dynEnt = Helper.GeneratePersonDynamicEntity();
            fakeStorage.Insert<DynamicEntity>(dynEnt);

            fakeStorage.Execute();

            TableStorageReader reader = new TableStorageReader(tableName, Helper.ConnectionStringForTest);
            List<DynamicEntity> actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(dynEnt.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(dynEnt, actual[0]);

            fakeStorage.Delete<DynamicEntity>(dynEnt);
            fakeStorage.Execute();

            actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(dynEnt.PartitionKey));

            Assert.AreEqual(0, actual.Count);

        }

        [TestMethod]
        async public Task TableStorageWriter_InsertAndExecuteAsync()
        {
            string tableName = Helper.NewTableName();
            FakeStorageWriter fakeStorage = new FakeStorageWriter(tableName, Helper.ConnectionStringForTest);
            Assert.AreEqual(fakeStorage.PendingOperations, 0);
            DynamicEntity dynEnt = Helper.GeneratePersonDynamicEntity();
            fakeStorage.Insert<DynamicEntity>(dynEnt);
            Assert.AreEqual(fakeStorage.PendingOperations, 1);

            await fakeStorage.ExecuteAsync();

            TableStorageReader reader = new TableStorageReader(tableName, Helper.ConnectionStringForTest);
            List<DynamicEntity> actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(dynEnt.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(dynEnt, actual[0]);

        }

        [TestMethod]
        public async Task TableStorageWriter_InsertAndMerge()
        {
            string tableName = Helper.NewTableName();
            FakeStorageWriter fakeStorage = new FakeStorageWriter(tableName, Helper.ConnectionStringForTest);
            DynamicEntity dynEnt = Helper.GeneratePersonDynamicEntity();
            fakeStorage.Insert<DynamicEntity>(dynEnt);
            fakeStorage.Execute();

            TableStorageReader reader = new TableStorageReader(tableName, Helper.ConnectionStringForTest);
            List<DynamicEntity> actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(dynEnt.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(dynEnt, actual[0]);

            dynEnt.Add("Extra", "Extra");
            fakeStorage.Merge<DynamicEntity>(dynEnt);
            fakeStorage.Execute();

            actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(dynEnt.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(dynEnt, actual[0]);

        }

        [TestMethod]
        public async Task TableStorageWriter_InsertOrMerge()
        {
            string tableName = Helper.NewTableName();
            FakeStorageWriter fakeStorage = new FakeStorageWriter(tableName, Helper.ConnectionStringForTest);
            DynamicEntity dynEnt = Helper.GeneratePersonDynamicEntity();
            fakeStorage.Insert<DynamicEntity>(dynEnt);
            fakeStorage.Execute();

            TableStorageReader reader = new TableStorageReader(tableName, Helper.ConnectionStringForTest);
            List<DynamicEntity> actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(dynEnt.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(dynEnt, actual[0]);

            dynEnt.Add("Extra", "Extra");
            fakeStorage.InsertOrMerge<DynamicEntity>(dynEnt);
            fakeStorage.Execute();

            actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(dynEnt.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(dynEnt, actual[0]);

        }

        [TestMethod]
        public async Task TableStorageWriter_InsertAndReplace()
        {
            string tableName = Helper.NewTableName();
            FakeStorageWriter fakeStorage = new FakeStorageWriter(tableName, Helper.ConnectionStringForTest);
            DynamicEntity dynEnt = Helper.GeneratePersonDynamicEntity();
            fakeStorage.Insert<DynamicEntity>(dynEnt);
            fakeStorage.Execute();

            TableStorageReader reader = new TableStorageReader(tableName, Helper.ConnectionStringForTest);
            List<DynamicEntity> actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(dynEnt.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(dynEnt, actual[0]);

            dynEnt.Set("Number", 987);
            fakeStorage.Replace<DynamicEntity>(dynEnt);
            fakeStorage.Execute();

            actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(dynEnt.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(dynEnt, actual[0]);

        }

        [TestMethod]
        public async Task TableStorageWriter_InsertOrReplace()
        {
            string tableName = Helper.NewTableName();
            FakeStorageWriter fakeStorage = new FakeStorageWriter(tableName, Helper.ConnectionStringForTest);
            DynamicEntity dynEnt = Helper.GeneratePersonDynamicEntity();
            fakeStorage.Insert<DynamicEntity>(dynEnt);
            fakeStorage.Execute();

            TableStorageReader reader = new TableStorageReader(tableName, Helper.ConnectionStringForTest);
            List<DynamicEntity> actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(dynEnt.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(dynEnt, actual[0]);

            dynEnt.Set("Number", 987);
            fakeStorage.InsertOrReplace<DynamicEntity>(dynEnt);
            fakeStorage.Execute();

            actual = (List<DynamicEntity>)await reader.ExecuteAsync<DynamicEntity>(new EntriesForPartition<DynamicEntity>(dynEnt.PartitionKey));

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(dynEnt, actual[0]);

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

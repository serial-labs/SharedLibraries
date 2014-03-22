using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.Data.AzureTable.Tests
{
    [TestClass]
    public class TableStorageProviderTest
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateTableStorageProvider_NoTableName_WithException()
        {
            FakeStorageProvider writer = new FakeStorageProvider("", "connectionString");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateTableStorageProvider_NoConnString_WithException()
        {
            FakeStorageProvider writer = new FakeStorageProvider("TableName", "");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void CreateTableStorageProvider_BadConnectionString()
        {
            FakeStorageProvider fakeStorage = new FakeStorageProvider("aa", "aa");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void CreateTableStorageProvider_BadTableName()
        {
            string tableName = Guid.NewGuid().ToString();
            FakeStorageProvider fakeStorage = new FakeStorageProvider(tableName, Helper.StorageConnectionString);
        }

        [TestMethod]
        public void CreateTableStorageProvider()
        {
            string tableName = Helper.DailyTableName;
            FakeStorageProvider fakeStorage = new FakeStorageProvider(tableName, Helper.StorageConnectionString);
        }

        [TestMethod]
        public void TableStorageProvider_GetTableReference()
        {
            string tableName = Helper.DailyTableName;
            FakeStorageProvider fakeStorage = new FakeStorageProvider(tableName, Helper.StorageConnectionString);
            fakeStorage.GetTableReference(CloudStorageAccount.Parse(Helper.StorageConnectionString), tableName);
        }

        class FakeStorageProvider : TableStorageProvider
        {
            public FakeStorageProvider(string tableName, string storageConnectionString)
                : base(TableStorageConfiguration.CreateDefault(tableName, storageConnectionString))
            {

            }

            public new CloudTable GetTableReference(CloudStorageAccount storageAccount, string tableName)
            {
                return base.GetTableReference(storageAccount, tableName);
            }

        }
    }
}

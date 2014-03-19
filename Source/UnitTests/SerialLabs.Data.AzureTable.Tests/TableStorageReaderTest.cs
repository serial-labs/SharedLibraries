using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.Data.AzureTable.Tests
{
    [TestClass]
    public class TableStorageReaderTest
    {
        [TestMethod]
        public void TableStorageReaderTest_GetReader()
        {
            TableStorageReader reader = TableStorageReader.Table(Helper.NewTableName(), Helper.ConnectionStringForTest);
            Assert.IsNotNull(reader);
        }

        [TestMethod]
        public async Task TableStorageReaderTest_Execute()
        {
            TableStorageReader reader = TableStorageReader.Table(Helper.NewTableName(), Helper.ConnectionStringForTest);
            var result = await reader.ExecuteAsync<TableEntity>(new EmptyQuery());
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TableStorageReaderTest_WithCache()
        {
            TableStorageReader reader = TableStorageReader.Table(Helper.NewTableName(), Helper.ConnectionStringForTest);
            reader = reader.WithCache();
            Assert.IsNotNull(reader);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace SerialLabs.Data.AzureTable.Tests
{
    [TestClass]
    public class TableStorageReaderTest
    {
        [TestMethod]
        public void TableStorageReaderTest_GetReader()
        {
            TableStorageReader reader = new TableStorageReader(Helper.DailyTableName, Helper.StorageConnectionString);
            Assert.IsNotNull(reader);
        }

        [TestMethod]
        public async Task TableStorageReaderTest_Execute()
        {
            TableStorageReader reader = new TableStorageReader(Helper.DailyTableName, Helper.StorageConnectionString);
            var result = await reader.ExecuteAsync<TableEntity>(new EmptyQuery());
            Assert.IsNotNull(result);
        }
    }
}

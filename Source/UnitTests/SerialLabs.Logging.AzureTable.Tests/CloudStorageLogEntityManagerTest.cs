using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SerialLabs.Logging.AzureTable.Tests
{
    [TestClass]
    public class CloudStorageLogEntityManagerTest
    {
        [TestMethod]
        [ExpectedException(typeof(PlatformException))]
        public void CreatePartitionKeyGuardTest()
        {
            ApplicationLogEntityManager.CreatePartitionKey("", DateTimeOffset.MinValue);
        }

        [TestMethod]
        public void CreatePartitionKeyTest()
        {
            string expected = "myApplication-201402";
            string actual = ApplicationLogEntityManager.CreatePartitionKey("myApplication", new DateTimeOffset(2014, 2, 1, 0, 0, 0, new TimeSpan()));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateFromLogEntryTest()
        {
            ApplicationLogEntity entity = ApplicationLogEntityManager.CreateFromLogEntry(new LogEntry
            {
                ApplicationName = "myApplication",
                TimeStamp = new DateTime(2014, 2, 1, 0, 0, 0),
                Message = "This is a log message"
            }, null);

            Assert.AreEqual("myApplication-201402", entity.PartitionKey);
            Assert.AreNotEqual(Guid.Empty.ToString(), entity.RowKey);
            Assert.AreEqual("This is a log message", entity.Message);
        }
    }
}

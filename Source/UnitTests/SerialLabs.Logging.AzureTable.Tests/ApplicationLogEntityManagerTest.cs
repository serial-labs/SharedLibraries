using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using static SerialLabs.Logging.AzureTable.ApplicationLogEntityManager;

namespace SerialLabs.Logging.AzureTable.Tests
{
    [TestClass]
    public class ApplicationLogEntityManagerTest
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
            string expected = "myApplication_201402";
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

            Assert.AreEqual("myApplication_201402", entity.PartitionKey);
            Assert.AreNotEqual(Guid.Empty.ToString(), entity.RowKey);
            Assert.AreEqual("This is a log message", entity.Message);
        }

        [TestMethod]
        public void CreateRowKey_Ascending_WithSuccess()
        {
            DateTime date = new DateTime(DateTime.MinValue.Ticks, DateTimeKind.Utc);
            Guid guid = Guid.Empty;

            string expected = String.Format(CultureInfo.InvariantCulture, "{0:D19}_{1}", date.Ticks, guid);
            string actual = ApplicationLogEntityManager.CreateRowKey(guid, date, SortOrder.Ascending);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateRowKey_Descending_WithSuccess()
        {
            DateTime date = new DateTime(DateTime.MaxValue.Ticks, DateTimeKind.Utc);
            Guid guid = Guid.Empty;

            string expected = String.Format(CultureInfo.InvariantCulture, "{0:D19}_{1}", DateTime.MaxValue.Ticks - date.Ticks, guid);
            string actual = ApplicationLogEntityManager.CreateRowKey(guid, date, SortOrder.Descending);

            Assert.AreEqual(expected, actual);
        }
    }
}

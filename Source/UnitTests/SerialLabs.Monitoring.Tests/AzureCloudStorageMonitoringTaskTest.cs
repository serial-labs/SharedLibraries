using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.Monitoring.Tests
{
    [TestClass]
    public class AzureCloudStorageMonitoringTaskTest
    {
        private const string TestTaskLabel = "AzureCloudStorageMonitoringTask";
        private const string TestStorageConnectionString = "UseDevelopmentStorage=true";
        private const string TestContainerName = "MonitoringTest";

        [TestMethod]
        public void CreateAzureCloudStorageMonitoringTask_WithSuccess()
        {
            AzureCloudStorageMonitoringTask task = new AzureCloudStorageMonitoringTask(
                TestTaskLabel,
                TestStorageConnectionString,
                AzureCloudContainerType.TableStorage,
                TestContainerName,
                true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateAzureCloudStorageMonitoringTask_NoLabel_WithException()
        {
            AzureCloudStorageMonitoringTask task = new AzureCloudStorageMonitoringTask(
                null,
                TestStorageConnectionString,
                AzureCloudContainerType.TableStorage,
                TestContainerName,
                true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateAzureCloudStorageMonitoringTask_NoStorageConnectionString_WithException()
        {
            AzureCloudStorageMonitoringTask task = new AzureCloudStorageMonitoringTask(
                TestTaskLabel,
                null,
                AzureCloudContainerType.TableStorage,
                TestContainerName,
                true);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateAzureCloudStorageMonitoringTask_NoCloudContainerType_WithException()
        {
            AzureCloudStorageMonitoringTask task = new AzureCloudStorageMonitoringTask(
                TestTaskLabel,
                TestStorageConnectionString,
                AzureCloudContainerType.NotSet,
                TestContainerName,
                true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateAzureCloudStorageMonitoringTask_NoContainerName_WithException()
        {
            AzureCloudStorageMonitoringTask task = new AzureCloudStorageMonitoringTask(
                TestTaskLabel,
                TestStorageConnectionString,
                AzureCloudContainerType.TableStorage,
                null,
                true);
        }

        [TestMethod]
        public void AzureCloudStorageMonitoringTask_Execute_WithSuccess()
        {
            AzureCloudStorageMonitoringTask task = new AzureCloudStorageMonitoringTask(TestTaskLabel, TestStorageConnectionString);
            MonitoringResult result = task.Execute();
            Assert.IsNotNull(result);
            Assert.AreEqual(TestTaskLabel, result.Title);
        }

        [TestMethod]
        public async Task AzureCloudStorageMonitoringTask_ExecuteAsync_WithSuccess()
        {
            AzureCloudStorageMonitoringTask task = new AzureCloudStorageMonitoringTask(TestTaskLabel, TestStorageConnectionString);
            MonitoringResult result = await task.ExecuteAsync();
            Assert.IsNotNull(result);
            Assert.AreEqual(TestTaskLabel, result.Title);
        }

        //private CloudTable GetTestCloudTable(string storageConnectionString, string tableName)
        //{
        //    CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);
        //    CloudTableClient client = account.CreateCloudTableClient();
        //    return client.GetTableReference(tableName);
        //}
    }
}

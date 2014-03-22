using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Runtime.Caching;

namespace SerialLabs.Data.AzureTable.Tests
{
    /// <summary>
    /// Summary description for TableStorageConfigurationTest
    /// </summary>
    [TestClass]
    public class TableStorageConfigurationTest
    {
        [TestMethod]
        public void ReadDefaultRequestOptions_WithSuccess()
        {
            TableRequestOptions options = TableStorageConfiguration.DefaultRequestOptions();
            Assert.IsNotNull(options);
            Assert.AreEqual(TablePayloadFormat.Json, options.PayloadFormat);
            Assert.IsInstanceOfType(options.RetryPolicy, typeof(ExponentialRetry));
        }

        [TestMethod]
        public void ReadDefaultQueryCacheItemPolicy()
        {
            DateTime now = DateTime.UtcNow;
            CacheItemPolicy policy = TableStorageConfiguration.DefaultQueryCacheItemPolicy();
            Assert.IsNotNull(policy);
            Assert.IsTrue(policy.AbsoluteExpiration > now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateConfiguration_WithException_NullConfiguration()
        {
            TableStorageConfiguration.ValidateConfiguration(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ValidateConfiguration_WithException_ParameterLess()
        {
            TableStorageConfiguration.ValidateConfiguration(new TableStorageConfiguration());
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ValidateConfiguration_WithException_NoStorageConnectionString()
        {
            TableStorageConfiguration.ValidateConfiguration(new TableStorageConfiguration()
            {
                TableName = "TableName"
            });
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ValidateConfiguration_WithException_NoRequestOptions()
        {
            TableStorageConfiguration.ValidateConfiguration(new TableStorageConfiguration()
            {
                TableName = "TableName",
                StorageConnectionString = "UseDevelopmentStorage=true"
            });
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ValidateConfiguration_WithException_InvalidTableNameFormat()
        {
            TableStorageConfiguration.ValidateConfiguration(new TableStorageConfiguration()
            {
                TableName = "Tableé",
                StorageConnectionString = "UseDevelopmentStorage=true",
                RequestOptions = new TableRequestOptions()
            });
        }

        [TestMethod]
        public void ValidateConfiguration_WithSuccess()
        {
            TableStorageConfiguration.ValidateConfiguration(new TableStorageConfiguration()
            {
                TableName = "TableName",
                StorageConnectionString = "UseDevelopmentStorage=true",
                RequestOptions = new TableRequestOptions(),
                CacheItemPolicy = new CacheItemPolicy()
            });
        }
        [TestMethod]
        public void ValidateConfiguration_WithDefault_WithSuccess()
        {
            TableStorageConfiguration configuration = TableStorageConfiguration.CreateDefault("TableName", "UseDevelopmentStorage=true");
            TableStorageConfiguration.ValidateConfiguration(configuration);
        }
    }
}

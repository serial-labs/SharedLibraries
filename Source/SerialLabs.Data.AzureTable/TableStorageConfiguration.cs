using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Runtime.Caching;

namespace SerialLabs.Data.AzureTable
{
    public class TableStorageConfiguration
    {
        public string TableName { get; set; }
        public string StorageConnectionString { get; set; }
        public TableRequestOptions RequestOptions { get; set; }
        public CacheItemPolicy CacheItemPolicy { get; set; }
        public bool UseNaggleAlgorithm { get; set; }

        public static void ValidateConfiguration(TableStorageConfiguration configuration)
        {
            Guard.ArgumentNotNull(configuration, "configuration");
            Guard.ArgumentNotNullOrWhiteSpace<FormatException>(configuration.TableName, "configuration.TableName");
            Guard.ArgumentNotNullOrWhiteSpace<FormatException>(configuration.StorageConnectionString, "configuration.StorageConnectionString");
            Guard.ArgumentNotNull<FormatException>(configuration.RequestOptions, "configuration.RequestOptions");

            // Validate table name format
            if (!RegexHelper.IsTableContainerNameValid(configuration.TableName))
                throw new FormatException("TableName format is not valid");
        }
        public static TableStorageConfiguration CreateDefault(string tableName, string storageConnectionString)
        {
            Guard.ArgumentNotNullOrWhiteSpace(tableName, "tableName");
            Guard.ArgumentNotNullOrWhiteSpace(storageConnectionString, "storageConnectionString");

            return new TableStorageConfiguration
            {
                TableName = tableName,
                StorageConnectionString = storageConnectionString,
                RequestOptions = DefaultRequestOptions()
            };
        }

        public static TableRequestOptions DefaultRequestOptions()
        {
            return new TableRequestOptions
            {
                PayloadFormat = TablePayloadFormat.Json,
                RetryPolicy = new ExponentialRetry()
            };
        }

        public static CacheItemPolicy DefaultQueryCacheItemPolicy()
        {
            return new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.UtcNow.Add(TimeSpan.FromMinutes(1))
            };
        }
    }
}

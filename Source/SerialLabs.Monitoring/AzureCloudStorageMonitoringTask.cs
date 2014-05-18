using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Threading.Tasks;

namespace SerialLabs.Monitoring
{
    /// <summary>
    /// Monitors an azure cloud storage
    /// </summary>
    public class AzureCloudStorageMonitoringTask : MonitoringTask
    {
        private const string DefaultContainerName = "AzureCloudStorageMonitoringTask";
        protected string StorageConnectionString { get; private set; }
        protected string ContainerName { get; private set; }
        protected bool CreateIfNotExist { get; private set; }
        protected AzureCloudContainerType ContainerType { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="AzureCloudMonitoringTask"/>
        /// </summary>
        /// <param name="label"></param>
        /// <param name="connectionConnectionString"></param>
        public AzureCloudStorageMonitoringTask(string label, string connectionConnectionString)
            : this(label, connectionConnectionString, AzureCloudContainerType.TableStorage, DefaultContainerName, true)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="AzureCloudStorageMonitoringTask"/>
        /// </summary>
        /// <param name="label"></param>
        /// <param name="storageConnectionString"></param>
        /// <param name="containerType">The type of the container which will be monitored</param>
        /// <param name="containerName">The name of container to test a connection to</param>
        /// <param name="createIfNotExist">Force the creation of the table if it doesn't exists, to ensure that no error is thrown</param>
        public AzureCloudStorageMonitoringTask(string label, string storageConnectionString, AzureCloudContainerType containerType, string containerName, bool createIfNotExist)
            : base(label)
        {
            Guard.ArgumentNotNullOrWhiteSpace(storageConnectionString, "storageConnectionString");
            StorageConnectionString = storageConnectionString;

            Guard.ArgumentNotNullOrWhiteSpace(containerName, "containerName");
            ContainerName = containerName;

            if (containerType == AzureCloudContainerType.NotSet)
                throw new ArgumentException("Container type if not set");
            ContainerType = containerType;
        }

        /// <summary>
        /// Synchronously checks if the cloud storage is responding
        /// </summary>
        protected override void ExecuteCore()
        {
            switch (ContainerType)
            {

                case AzureCloudContainerType.TableStorage:
                    QueryCloudTable();
                    break;
                case AzureCloudContainerType.BlobStorage:
                    QueryCloudBlob();
                    break;
                case AzureCloudContainerType.QueueStorage:
                    QueryCloudQueue();
                    break;
                case AzureCloudContainerType.NotSet:
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Asynchronously checks if the cloud storage is responding.
        /// </summary>
        /// <returns></returns>
        protected override Task ExecuteCoreAsync()
        {
            switch (ContainerType)
            {
                case AzureCloudContainerType.TableStorage:
                    return Task.Run(() => { QueryCloudTable(); });
                case AzureCloudContainerType.BlobStorage:
                    return Task.Run(() => { QueryCloudBlob(); });
                case AzureCloudContainerType.QueueStorage:
                    return Task.Run(() => { QueryCloudQueue(); });
                case AzureCloudContainerType.NotSet:
                default:
                    throw new NotImplementedException();
            }
        }

        private void QueryCloudTable()
        {
            CloudTable table = GetCloudTable();
            if (CreateIfNotExist)
                table.CreateIfNotExists();
            string guid = Guid.NewGuid().ToString();
            string partitionCondition = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, guid);
            string rowCondition = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, guid);

            TableQuery<DynamicTableEntity> query = new TableQuery<DynamicTableEntity>();
            query.Where(TableQuery.CombineFilters(partitionCondition, TableOperators.And, rowCondition));
            table.ExecuteQuery<DynamicTableEntity>(query);
        }

        private void QueryCloudBlob()
        {
            CloudBlobContainer container = GetBlobContainer();
        }

        private void QueryCloudQueue()
        {
            throw new NotImplementedException();
        }

        private CloudTable GetCloudTable()
        {
            CloudTableClient client = GetStorageAccount().CreateCloudTableClient();
            client.DefaultRequestOptions.RetryPolicy = new ExponentialRetry();
            client.DefaultRequestOptions.ServerTimeout = new TimeSpan(0, 0, 0, 5);
            client.DefaultRequestOptions.PayloadFormat = TablePayloadFormat.JsonNoMetadata;
            client.DefaultRequestOptions.MaximumExecutionTime = new TimeSpan(0, 0, 0, 5);
            client.DefaultRequestOptions.LocationMode = LocationMode.PrimaryOnly;
            return client.GetTableReference(ContainerName);
        }

        private CloudBlobContainer GetBlobContainer()
        {
            CloudBlobClient client = GetStorageAccount().CreateCloudBlobClient();
            client.DefaultRequestOptions.MaximumExecutionTime = new TimeSpan(0, 0, 0, 5);
            client.DefaultRequestOptions.RetryPolicy = new ExponentialRetry();
            client.DefaultRequestOptions.ServerTimeout = new TimeSpan(0, 0, 0, 5);
            return client.GetContainerReference(ContainerName);
        }

        private CloudQueue GetQueueContainer()
        {
            CloudQueueClient client = GetStorageAccount().CreateCloudQueueClient();
            client.DefaultRequestOptions.MaximumExecutionTime = new TimeSpan(0, 0, 0, 5);
            client.DefaultRequestOptions.RetryPolicy = new ExponentialRetry();
            client.DefaultRequestOptions.ServerTimeout = new TimeSpan(0, 0, 0, 5);
            return client.GetQueueReference(ContainerName);
        }

        private CloudStorageAccount GetStorageAccount()
        {
            return CloudStorageAccount.Parse(StorageConnectionString);
        }
    }
}

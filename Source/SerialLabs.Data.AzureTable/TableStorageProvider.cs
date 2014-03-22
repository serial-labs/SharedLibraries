using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Net;

namespace SerialLabs.Data.AzureTable
{
    public abstract class TableStorageProvider
    {
        protected readonly CloudTable _table;
        protected readonly TableStorageConfiguration _configuration;

        protected TableStorageProvider(TableStorageConfiguration configuration)
        {
            TableStorageConfiguration.ValidateConfiguration(configuration);
            _configuration = configuration;

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_configuration.StorageConnectionString);
            // http://msmvps.com/blogs/nunogodinho/archive/2013/11/20/windows-azure-storage-performance-best-practices.aspx
            ServicePointManager.FindServicePoint(storageAccount.TableEndpoint).UseNagleAlgorithm = _configuration.UseNaggleAlgorithm;
            _table = GetTableReference(storageAccount, _configuration.TableName);
        }

        protected CloudTable GetTableReference(CloudStorageAccount storageAccount, string tableName)
        {
            CloudTableClient client = storageAccount.CreateCloudTableClient();
            return client.GetTableReference(tableName);
        }
    }
}

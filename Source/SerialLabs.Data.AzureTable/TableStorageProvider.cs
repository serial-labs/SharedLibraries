using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace SerialLabs.Data.AzureTable
{
    public abstract class TableStorageProvider
    {
        protected readonly CloudTable _table;

        protected TableStorageProvider(string tableName, string connectionString)
        {
            Guard.ArgumentNotNullOrWhiteSpace(tableName, "tableName");

            // Table name must correspond to a format
            if (!(new Regex("^[A-Za-z][A-Za-z0-9]{2,62}$")).IsMatch(tableName))
            {
                throw new FormatException();
            }

            Guard.ArgumentNotNullOrWhiteSpace(connectionString, "connectionString");

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            // http://msmvps.com/blogs/nunogodinho/archive/2013/11/20/windows-azure-storage-performance-best-practices.aspx
            ServicePointManager.FindServicePoint(storageAccount.TableEndpoint).UseNagleAlgorithm = false;
            _table = GetTableReference(storageAccount, tableName);
        }

        protected CloudTable GetTableReference(CloudStorageAccount storageAccount, string tableName)
        {
            CloudTableClient client = storageAccount.CreateCloudTableClient();
            return client.GetTableReference(tableName);
        }
    }
}

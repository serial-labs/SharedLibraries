
using System.Configuration;
namespace SerialLabs.Logging.AzureTable.Tests
{
    class TestHelper
    {
        public const string StorageConnectionStringName = "StorageConnectionString";


        public static string GetConnectionString()
        {
            return ConfigurationManager.AppSettings[TestHelper.StorageConnectionStringName];
        }
    }
}

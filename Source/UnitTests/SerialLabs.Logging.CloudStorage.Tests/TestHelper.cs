
using System.Configuration;
namespace SerialLabs.Logging.CloudStorage.Tests
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

using Microsoft.ApplicationServer.Caching;
using System.Threading.Tasks;

namespace SerialLabs.Monitoring
{
    public class AzureCacheMonitoringTask : MonitoringTask
    {
        private DataCacheFactoryConfiguration _factoryConfig;
        public AzureCacheMonitoringTask(string label, DataCacheFactoryConfiguration factoryConfig)
            : base(label)
        {
            Guard.ArgumentNotNullOrWhiteSpace(label, "label");
            Guard.ArgumentNotNull(factoryConfig, "factoryConfig");
            _factoryConfig = factoryConfig;
        }
        public AzureCacheMonitoringTask(string label)
            : base(label)
        {
            Guard.ArgumentNotNullOrWhiteSpace(label, "label");
            _factoryConfig = new DataCacheFactoryConfiguration();
        }
        protected override void ExecuteCore()
        {
            DataCacheFactory cacheFactory = new DataCacheFactory(_factoryConfig);
            DataCache defaultCache = cacheFactory.GetDefaultCache();
            defaultCache.Put("testkey", "testobject");
            string strObject = (string)defaultCache.Get("testkey");
        }

        protected override Task ExecuteCoreAsync()
        {
            DataCacheFactory cacheFactory = new DataCacheFactory(_factoryConfig);
            DataCache defaultCache = cacheFactory.GetDefaultCache();
            defaultCache.Put("testkey", "testobject");
            return Task.FromResult<string>((string)defaultCache.Get("testkey"));
        }
    }
}

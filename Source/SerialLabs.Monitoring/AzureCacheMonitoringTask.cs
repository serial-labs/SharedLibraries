using Microsoft.ApplicationServer.Caching;
using System;
using System.Threading.Tasks;

namespace SerialLabs.Monitoring
{
    public class AzureCacheMonitoringTask : MonitoringTask
    {
        protected string CacheRegion { get; private set; }
        protected string CacheKey { get; private set; }

        public AzureCacheMonitoringTask(string label)
            : this(label, "MonitoringCacheRegion", "MonitoringCacheKey")
        { }

        public AzureCacheMonitoringTask(string label, string cacheRegion, string cacheKey)
            : base(label)
        {
            Guard.ArgumentNotNullOrWhiteSpace(cacheRegion, "cacheRegion");
            CacheRegion = cacheRegion;
            Guard.ArgumentNotNullOrWhiteSpace(cacheKey, "cacheKey");
            CacheKey = cacheKey;
        }

        protected override void ExecuteCore()
        {
            DataCache cache = new DataCache();
            cache.CreateRegion(CacheRegion);
            cache.Put(CacheKey, Guid.NewGuid().ToString(), CacheRegion);
            string result = (string)cache.Get(CacheKey, CacheRegion);
        }

        protected override Task ExecuteCoreAsync()
        {
            DataCache cache = new DataCache();
            cache.CreateRegion(CacheRegion);
            cache.Put(CacheKey, Guid.NewGuid().ToString(), CacheRegion);
            string result = (string)cache.Get(CacheKey, CacheRegion);
            return Task.FromResult<string>((string)cache.Get(CacheKey));
        }
    }
}

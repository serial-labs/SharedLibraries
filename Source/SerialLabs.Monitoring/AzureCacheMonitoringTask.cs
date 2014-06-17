using Microsoft.ApplicationServer.Caching;
using System;
using System.Threading.Tasks;

namespace SerialLabs.Monitoring
{
    public class AzureCacheMonitoringTask : MonitoringTask
    {
        protected DataCache Cache { get; private set; }
        protected string CacheRegion { get; private set; }
        protected string CacheKey { get; private set; }

        public AzureCacheMonitoringTask(string label)
            : this(label, "MonitoringCacheRegion", "MonitoringCacheKey")
        { }

        public AzureCacheMonitoringTask(string label, string cacheRegion, string cacheKey)
            : this(label, new DataCache(), cacheRegion, cacheKey)
        { }

        public AzureCacheMonitoringTask(string label, DataCache cache, string cacheRegion, string cacheKey)
        {
            Guard.ArgumentNotNull(cache, "cache");
            Cache = cache;

            Guard.ArgumentNotNullOrWhiteSpace(cacheRegion, "cacheRegion");
            CacheRegion = cacheRegion;

            Guard.ArgumentNotNullOrWhiteSpace(cacheKey, "cacheKey");
            CacheKey = cacheKey;
        }

        protected override void ExecuteCore()
        {
            Cache.CreateRegion(CacheRegion);
            Cache.Put(CacheKey, Guid.NewGuid().ToString(), CacheRegion);
            string result = (string)Cache.Get(CacheKey, CacheRegion);
        }

        protected override Task ExecuteCoreAsync()
        {
            Cache.CreateRegion(CacheRegion);
            Cache.Put(CacheKey, Guid.NewGuid().ToString(), CacheRegion);
            string result = (string)Cache.Get(CacheKey, CacheRegion);
            return Task.FromResult<string>((string)Cache.Get(CacheKey));
        }
    }
}

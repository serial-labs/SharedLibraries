//using Microsoft.ApplicationServer.Caching;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace SerialLabs.Monitoring
{
    public class AzureCacheMonitoringTask : MonitoringTask
    {
        //protected DataCache Cache { get; private set; }
        protected string CacheRegion { get; private set; }
        protected string CacheKey { get; private set; }

        public AzureCacheMonitoringTask(string label)
            : this(label, "MonitoringCacheRegion", "MonitoringCacheKey")
        { }

        public AzureCacheMonitoringTask(string label, string cacheRegion, string cacheKey)
            : this(label, HttpRuntime.Cache, cacheRegion, cacheKey)
        { }

        public AzureCacheMonitoringTask(string label, System.Web.Caching.Cache Cache, string cacheRegion, string cacheKey)
            : base(label)
        {
            Guard.ArgumentNotNull(Cache, "cache");

            Guard.ArgumentNotNullOrWhiteSpace(cacheRegion, "cacheRegion");
            CacheRegion = cacheRegion;

            Guard.ArgumentNotNullOrWhiteSpace(cacheKey, "cacheKey");
            CacheKey = cacheKey;
        }

        protected override void ExecuteCore()
        {
            //Cache.CreateRegion(CacheRegion);
            string result = PutGet();
        }

        protected override Task ExecuteCoreAsync()
        {
            //Cache.CreateRegion(CacheRegion);
            //string result = PutGet();
            return Task.FromResult<string>(PutGet());
        }

        private string PutGet() {
            string newKey = CacheRegion + "|" + CacheKey;
            if (HttpRuntime.Cache[newKey] != null)
            {
                HttpRuntime.Cache.Add(newKey, Guid.NewGuid().ToString(), null,
                    Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }
            else
            {
                HttpRuntime.Cache.Insert(newKey, Guid.NewGuid().ToString(), null,
                    Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }
            return HttpRuntime.Cache.Get(newKey).ToString ();
        }
    }
}

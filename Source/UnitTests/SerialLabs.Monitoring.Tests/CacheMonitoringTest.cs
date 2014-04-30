using Microsoft.ApplicationServer.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security;
using System.Threading.Tasks;

namespace SerialLabs.Monitoring.Tests
{
    [TestClass]
    public class CacheMonitoringTest
    {
        private string Endpoint = "myblazon.cache.windows.net";
        private string SecurityString = "YWNzOmh0dHBzOi8vbXlibGF6b24yODA3LWNhY2hlLmFjY2Vzc2NvbnRyb2wud2luZG93cy5uZXQvL1dSQVB2MC45LyZvd25lciY5QnJSbUlFMzVOSVA4WTh2VkdvU0tOVEw3Y0s1ZjFKTHc1WjZCTEtwdWtVPSZodHRwOi8vbXlibGF6b24uY2FjaGUud2luZG93cy5uZXQv";

        [TestMethod]
        public void CreateCacheMonitoring_WithConfigArgs_Success()
        {
            AzureCacheMonitoringTask cacheMonitoring = new AzureCacheMonitoringTask("testCache", InitDataCacheConfig());
        }
        [TestMethod]
        public void CreateCacheMonitoring_WithNoArgs_WithSuccess()
        {
            AzureCacheMonitoringTask cacheMonitoring = new AzureCacheMonitoringTask("testCache");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateCacheMonitoring_WithNoArgs_WithException()
        {
            AzureCacheMonitoringTask cacheMonitoring = new AzureCacheMonitoringTask(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateCacheMonitoring_LabelWithException()
        {
            AzureCacheMonitoringTask cacheMonitoring = new AzureCacheMonitoringTask(null, InitDataCacheConfig());
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateCacheMonitoring_EndpointWithException()
        {
            AzureCacheMonitoringTask cacheMonitoring = new AzureCacheMonitoringTask("test", null);
        }

        [TestMethod]
        public void CacheMonitoringTask_Execute_WithSuccess()
        {
            AzureCacheMonitoringTask cacheMonitoring = new AzureCacheMonitoringTask("test", InitDataCacheConfig());
            var result = cacheMonitoring.Execute();
            Assert.IsNotNull(result);
            Assert.IsNull(result.Error);
        }
        [TestMethod]
        public async Task CacheMonitoringTask_ExecuteAsync_WithSuccess()
        {
            AzureCacheMonitoringTask cacheMonitoring = new AzureCacheMonitoringTask("test", InitDataCacheConfig());
            var result = await cacheMonitoring.ExecuteAsync();
            Assert.IsNotNull(result);
            Assert.IsNull(result.Error);
        }

        #region helper
        public DataCacheFactoryConfiguration InitDataCacheConfig()
        {
            DataCacheFactoryConfiguration factoryConfig = new DataCacheFactoryConfiguration();
            string strACSKey = SecurityString;
            var secureACSKey = new SecureString();
            foreach (char a in strACSKey)
            {
                secureACSKey.AppendChar(a);
            }
            secureACSKey.MakeReadOnly();
            DataCacheSecurity factorySecurity = new DataCacheSecurity(secureACSKey);
            factoryConfig = new DataCacheFactoryConfiguration();
            factoryConfig.AutoDiscoverProperty = new DataCacheAutoDiscoverProperty(true, Endpoint);
            factoryConfig.SecurityProperties = factorySecurity;
            return factoryConfig;
        }
        #endregion
    }
}

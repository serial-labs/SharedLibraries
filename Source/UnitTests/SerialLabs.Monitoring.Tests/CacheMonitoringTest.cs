using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SerialLabs.Monitoring.Tests
{
    [TestClass]
    public class CacheMonitoringTest
    {
        //[TestMethod]
        //public void CreateCacheMonitoring_WithConfigArgs_Success()
        //{            
        //    AzureCacheMonitoringTask cacheMonitoring = new AzureCacheMonitoringTask("testCache", "testCacheRegion", "testCacheKey");
        //}

        //[TestMethod]
        //public void CreateCacheMonitoring_WithNoArgs_WithSuccess()
        //{
        //    AzureCacheMonitoringTask cacheMonitoring = new AzureCacheMonitoringTask("testCache");
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void CreateCacheMonitoring_WithNoArgs_WithException()
        //{
        //    AzureCacheMonitoringTask cacheMonitoring = new AzureCacheMonitoringTask(null, null, null);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void CreateCacheMonitoring_LabelWithException()
        //{
        //    AzureCacheMonitoringTask cacheMonitoring = new AzureCacheMonitoringTask("test", null, null);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void CreateCacheMonitoring_EndpointWithException()
        //{
        //    AzureCacheMonitoringTask cacheMonitoring = new AzureCacheMonitoringTask("test", "test", null);
        //}

        //[TestMethod]
        //public void CacheMonitoringTask_Execute_WithSuccess()
        //{
        //    AzureCacheMonitoringTask cacheMonitoring = new AzureCacheMonitoringTask("test", "test", Guid.NewGuid().ToString());
        //    var result = cacheMonitoring.Execute();
        //    Assert.IsNotNull(result);
        //    Assert.IsNull(result.Error);
        //}

        //[TestMethod]
        //public async Task CacheMonitoringTask_ExecuteAsync_WithSuccess()
        //{
        //    AzureCacheMonitoringTask cacheMonitoring = new AzureCacheMonitoringTask("test", "test", Guid.NewGuid().ToString());
        //    var result = await cacheMonitoring.ExecuteAsync();
        //    Assert.IsNotNull(result);
        //    Assert.IsNull(result.Error);
        //}

        #region helper
        //public DataCacheFactoryConfiguration InitDataCacheConfig()
        //{
        //    DataCacheFactoryConfiguration factoryConfig = new DataCacheFactoryConfiguration();
        //    string strACSKey = SecurityString;
        //    var secureACSKey = new SecureString();
        //    foreach (char a in strACSKey)
        //    {
        //        secureACSKey.AppendChar(a);
        //    }
        //    secureACSKey.MakeReadOnly();
        //    DataCacheSecurity factorySecurity = new DataCacheSecurity(secureACSKey);
        //    factoryConfig = new DataCacheFactoryConfiguration();
        //    factoryConfig.AutoDiscoverProperty = new DataCacheAutoDiscoverProperty(true, Endpoint);
        //    factoryConfig.SecurityProperties = factorySecurity;
        //    return factoryConfig;
        //}
        #endregion
    }
}

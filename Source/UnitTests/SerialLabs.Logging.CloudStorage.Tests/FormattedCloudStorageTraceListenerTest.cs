using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.Logging.CloudStorage.Tests
{
    [TestClass]
    public class FormattedCloudStorageTraceListenerTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            FormattedCloudStorageTraceListener listener = new FormattedCloudStorageTraceListener(TestHelper.GetConnectionString());
        }

        [TestMethod]
        [ExpectedException(typeof(PlatformException))]
        public void ConstructorWithGuardTest()
        {
            FormattedCloudStorageTraceListener listener = new FormattedCloudStorageTraceListener(null);
        }

        [TestMethod]
        [ExpectedException(typeof(PlatformException))]
        public void ConstructorWithGuard2Test()
        {
            FormattedCloudStorageTraceListener listener = new FormattedCloudStorageTraceListener("");
        }
        [TestMethod]
        [ExpectedException(typeof(PlatformException))]
        public void ConstructorWithGuard3Test()
        {
            FormattedCloudStorageTraceListener listener = new FormattedCloudStorageTraceListener(" ");
        }

        [TestMethod]
        public void WriteTest()
        {
            FormattedCloudStorageTraceListener listener = new FormattedCloudStorageTraceListener(TestHelper.GetConnectionString());
            listener.Write("Unit Testing Write Test");        
        }

        [TestMethod]
        public async Task WriteAsyncTest()
        {
            FormattedCloudStorageTraceListener listener = new FormattedCloudStorageTraceListener(TestHelper.GetConnectionString());
            await listener.WriteAsync("Unit Testing Write Test");
        }

        [TestMethod]
        public void WriteLineTest()
        {
            FormattedCloudStorageTraceListener listener = new FormattedCloudStorageTraceListener(TestHelper.GetConnectionString());
            listener.WriteLine("Unit Testing Write Test");
        }

        [TestMethod]
        public async Task WriteLineAsyncTest()
        {
            FormattedCloudStorageTraceListener listener = new FormattedCloudStorageTraceListener(TestHelper.GetConnectionString());
            await listener.WriteLineAsync("Unit Testing Write Test");
        }

    }
}

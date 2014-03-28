using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace SerialLabs.Logging.AzureTable.Tests
{
    [TestClass]
    public class FormattedCloudStorageTraceListenerTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            FormattedAzureTableTraceListener listener = new FormattedAzureTableTraceListener(TestHelper.GetConnectionString());
        }

        [TestMethod]
        [ExpectedException(typeof(PlatformException))]
        public void ConstructorWithGuardTest()
        {
            FormattedAzureTableTraceListener listener = new FormattedAzureTableTraceListener(null);
        }

        [TestMethod]
        [ExpectedException(typeof(PlatformException))]
        public void ConstructorWithGuard2Test()
        {
            FormattedAzureTableTraceListener listener = new FormattedAzureTableTraceListener("");
        }
        [TestMethod]
        [ExpectedException(typeof(PlatformException))]
        public void ConstructorWithGuard3Test()
        {
            FormattedAzureTableTraceListener listener = new FormattedAzureTableTraceListener(" ");
        }

        [TestMethod]
        public void WriteTest()
        {
            FormattedAzureTableTraceListener listener = new FormattedAzureTableTraceListener(TestHelper.GetConnectionString());
            listener.Write("Unit Testing Write Test");        
        }

        [TestMethod]
        public async Task WriteAsyncTest()
        {
            FormattedAzureTableTraceListener listener = new FormattedAzureTableTraceListener(TestHelper.GetConnectionString());
            await listener.WriteAsync("Unit Testing Write Test");
        }

        [TestMethod]
        public void WriteLineTest()
        {
            FormattedAzureTableTraceListener listener = new FormattedAzureTableTraceListener(TestHelper.GetConnectionString());
            listener.WriteLine("Unit Testing Write Test");
        }

        [TestMethod]
        public async Task WriteLineAsyncTest()
        {
            FormattedAzureTableTraceListener listener = new FormattedAzureTableTraceListener(TestHelper.GetConnectionString());
            await listener.WriteLineAsync("Unit Testing Write Test");
        }

    }
}

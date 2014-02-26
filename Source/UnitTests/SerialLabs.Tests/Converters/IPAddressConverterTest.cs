using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerialLabs.Converters;

namespace SerialLabs.Tests.Converters
{
    [TestClass]
    public class IPAddressConverterTest
    {
        [TestMethod]
        public void ToInt64Test()
        {
            Assert.AreEqual(1474760558, IPAddressConverter.ToInt64("87.231.15.110"));
            Assert.AreEqual(2130706433, IPAddressConverter.ToInt64("127.0.0.1"));
        }

        //[TestMethod]
        //public void ToAddressTest()
        //{
        //    Assert.AreEqual("87.231.15.110", IPAddressConverter.ToAddress(1474760558));
        //    Assert.AreEqual("64.233.187.99", IPAddressConverter.ToAddress(1089059683));
        //    Assert.AreEqual("127.0.0.1", IPAddressConverter.ToAddress(2130706433));            
        //}
    }
}

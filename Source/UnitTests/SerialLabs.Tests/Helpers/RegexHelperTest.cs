using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SerialLabs.Tests
{
    [TestClass]
    public class RegexHelperTest
    {
        [TestMethod]
        public void IsSHA256Test()
        {
            Assert.IsTrue(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.Empty.ToString())));
            Assert.IsTrue(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString())));
            Assert.IsFalse(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString()).Substring(0, 31)));
            Assert.IsFalse(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString()).Substring(0, 63) + "@"));
        }
        [TestMethod]
        public void IsSecretKeyTest()
        {
            Assert.IsTrue(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.Empty.ToString())));
            Assert.IsTrue(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString())));
            Assert.IsFalse(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString()).Substring(0, 12)));
            Assert.IsFalse(RegexHelper.IsSHA256Hash(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString()).Substring(0, 15) + "@"));
        }
        [TestMethod]
        public void IsEmailTest()
        {
            Assert.IsTrue(RegexHelper.IsEmail("b172f15bac94d30fde90@serial-labs.com"));
        }
        [TestMethod]
        public void IsAccessCodeTest()
        {
            Assert.IsFalse(RegexHelper.IsAccessCode("12345678901")); //  11 chars
            Assert.IsFalse(RegexHelper.IsAccessCode("123456789@")); //   Unauthorized char
            Assert.IsFalse(RegexHelper.IsAccessCode("123456789é")); //   Unauthorized char

            Assert.IsTrue(RegexHelper.IsAccessCode("1234567890"));
            Assert.IsTrue(RegexHelper.IsAccessCode("123.456.78"));
            Assert.IsTrue(RegexHelper.IsAccessCode("123-456-78"));
        }
        [TestMethod]
        public void IsUrlTest()
        {
            Assert.IsTrue(RegexHelper.IsUrl("http://www.domain.com/page.html"));
            Assert.IsTrue(RegexHelper.IsUrl("https://www.domain.com/page.html"));
            Assert.IsTrue(RegexHelper.IsUrl("ftp://www.domain.com/page.html"));
            Assert.IsTrue(RegexHelper.IsUrl("http://domain.com/?query=a"));
            Assert.IsTrue(RegexHelper.IsUrl("http://www.domain.com/page#result"));
            Assert.IsTrue(RegexHelper.IsUrl("http://www.domain.com/page.html/"));
            Assert.IsTrue(RegexHelper.IsUrl("http://subdomain.domain.com/page.html"));
            Assert.IsTrue(RegexHelper.IsUrl("http://www.domain.com/1234,345"));
            Assert.IsTrue(RegexHelper.IsUrl("http://127.0.0.1:8090/"));

            Assert.IsFalse(RegexHelper.IsUrl("xtp://domain.com"));
            Assert.IsFalse(RegexHelper.IsUrl("http://localhost/")); // Should use 127.0.0.1
        }
    }
}

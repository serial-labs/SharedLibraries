using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerialLabs.UnitTestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.Tests.Helpers
{
    [TestClass]
    public class DateHelperTest
    {
        [TestMethod]
        public void ToUnixTimeTest()
        {
            DateTime expected = DateTime.Now;
            long timeStamp = DateHelper.ToUnixTime(expected);
            DateTime actual = DateHelper.FromUnixTime(timeStamp);
            CommonComparers.AreSimilar(expected, actual);
        }
    }
}

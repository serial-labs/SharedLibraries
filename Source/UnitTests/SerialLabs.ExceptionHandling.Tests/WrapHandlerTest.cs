using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.ExceptionHandling.Tests
{
    [TestClass]
    public class WrapHandlerTest
    {
        /// <summary>
        ///A test for ReplaceHandler Constructor
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WrapHandlerConstructor_Failure_Test()
        {
            string exceptionMessage = string.Empty;
            Type replaceExceptionType = null;
            WrapHandler target = new WrapHandler(exceptionMessage, replaceExceptionType);
        }
        /// <summary>
        ///A test for ReplaceHandler Constructor
        ///</summary>
        [TestMethod]
        public void WrapHandlerConstructor_Sucess_Test()
        {
            string exceptionMessage = string.Empty;
            Type replaceExceptionType = typeof(PlatformException);
            WrapHandler target = new WrapHandler(exceptionMessage, replaceExceptionType);
        }
        [TestMethod]
        public void WrapHandler_HandleException_Test()
        {
            string errorMessage = "An error ocurred in the platform layer";
            Type replaceExceptionType = typeof(PlatformException);
            Exception originalException = new ArgumentNullException("argumentName", "A super big error occured");
            WrapHandler handler = new WrapHandler(errorMessage, replaceExceptionType);
            Exception result = handler.HandleException(originalException);
            Assert.IsNotNull(result);
            Assert.AreEqual<string>(errorMessage, result.Message);
            Assert.AreEqual<Type>(replaceExceptionType, result.GetType());
            Assert.AreEqual<Exception>(originalException, result.InnerException);
        }
    }
}

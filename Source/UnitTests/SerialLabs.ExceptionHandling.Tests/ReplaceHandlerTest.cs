using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.ExceptionHandling.Tests
{
    [TestClass]
    public class ReplaceHandlerTest
    {
        /// <summary>
        ///A test for ReplaceHandler Constructor
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReplaceHandlerConstructor_Failure_Test()
        {
            string exceptionMessage = string.Empty;
            Type replaceExceptionType = null;
            ReplaceHandler target = new ReplaceHandler(exceptionMessage, replaceExceptionType);
        }
        /// <summary>
        ///A test for ReplaceHandler Constructor
        ///</summary>
        [TestMethod]
        public void ReplaceHandlerConstructor_Sucess_Test()
        {
            string exceptionMessage = string.Empty;
            Type replaceExceptionType = typeof(PlatformException);
            ReplaceHandler target = new ReplaceHandler(exceptionMessage, replaceExceptionType);
        }
        [TestMethod]
        public void ReplaceHandler_HandleException_Test()
        {
            string errorMessage = "An error ocurred in the platform layer";
            Type replaceExceptionType = typeof(PlatformException);
            ReplaceHandler handler = new ReplaceHandler(errorMessage, replaceExceptionType);
            Exception result = handler.HandleException(new ArgumentNullException("argumentName", "A super big error occured"));
            Assert.IsNotNull(result);
            Assert.AreEqual<string>(errorMessage, result.Message);
            Assert.AreEqual<Type>(replaceExceptionType, result.GetType());
            Assert.IsNull(result.InnerException);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SerialLabs.ExceptionHandling.Tests
{
    [TestClass]
    public class ExceptionManagerTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HandleException_NotifyRethrow_Test()
        {
            IEnumerable<IExceptionHandler> handlers = new List<IExceptionHandler>(
                new IExceptionHandler[] {
                    new ReplaceHandler("An error occured in the platform", typeof(PlatformException))
                }
            );
            ExceptionManager manager = new ExceptionManager(PostHandlingAction.NotifyRethrow, handlers);
            try
            {
                ThrowArgumentNullException();
            }
            catch (Exception e)
            {
                if (manager.HandleException(e)) throw;
            }
        }
        [TestMethod]
        [ExpectedException(typeof(PlatformException))]
        public void HandleException_ThrowNewException_Test()
        {
            IEnumerable<IExceptionHandler> handlers = new List<IExceptionHandler>(
                new IExceptionHandler[] {
                    new ReplaceHandler("An error occured in the platform", typeof(PlatformException))
                }
            );
            ExceptionManager manager = new ExceptionManager(PostHandlingAction.ThrowNewException, handlers);
            try
            {
                ThrowArgumentNullException();
            }
            catch (Exception e)
            {
                if (manager.HandleException(e)) throw;
            }
        }
        [TestMethod]
        public void HandleException_None_Test()
        {
            IEnumerable<IExceptionHandler> handlers = new List<IExceptionHandler>(
                new IExceptionHandler[] {
                    new ReplaceHandler("An error occured in the platform", typeof(PlatformException))
                }
            );
            ExceptionManager manager = new ExceptionManager(PostHandlingAction.None, handlers);
            try
            {
                ThrowArgumentNullException();
            }
            catch (Exception e)
            {
                if (manager.HandleException(e)) throw;
            }
        }
        [TestMethod]
        public void HandleException_Out_None_Test()
        {
            IEnumerable<IExceptionHandler> handlers = new List<IExceptionHandler>(
                new IExceptionHandler[] {
                                new ReplaceHandler("An error occured in the platform", typeof(PlatformException))
                            }
            );
            ExceptionManager manager = new ExceptionManager(PostHandlingAction.None, handlers);
            Exception exceptionToThrow = null;
            bool rethrowRecommanded = true;
            try
            {
                ThrowArgumentNullException();
            }
            catch (Exception e)
            {
                rethrowRecommanded = manager.HandleException(e, out exceptionToThrow);
            }
            Assert.IsFalse(rethrowRecommanded);
            Assert.IsNull(exceptionToThrow);
        }
        [TestMethod]
        public void HandleException_Out_NotifyRethrow_Test()
        {
            IEnumerable<IExceptionHandler> handlers = new List<IExceptionHandler>(
                new IExceptionHandler[] {
                                new ReplaceHandler("An error occured in the platform", typeof(PlatformException))
                            }
            );
            ExceptionManager manager = new ExceptionManager(PostHandlingAction.NotifyRethrow, handlers);
            Exception exceptionToThrow = null;
            bool rethrowRecommanded = true;
            try
            {
                ThrowArgumentNullException();
            }
            catch (Exception e)
            {
                rethrowRecommanded = manager.HandleException(e, out exceptionToThrow);
            }
            Assert.IsTrue(rethrowRecommanded);
            Assert.IsNull(exceptionToThrow);
        }
        [TestMethod]
        public void HandleException_Out_ThrowNewException_Test()
        {
            IEnumerable<IExceptionHandler> handlers = new List<IExceptionHandler>(
                new IExceptionHandler[] {
                                new ReplaceHandler("An error occured in the platform", typeof(PlatformException))
                            }
            );
            ExceptionManager manager = new ExceptionManager(PostHandlingAction.ThrowNewException, handlers);
            Exception exceptionToThrow = null;
            bool rethrowRecommanded = true;
            try
            {
                ThrowArgumentNullException();
            }
            catch (Exception e)
            {
                rethrowRecommanded = manager.HandleException(e, out exceptionToThrow);
            }
            Assert.IsTrue(rethrowRecommanded);
            Assert.IsInstanceOfType(exceptionToThrow, typeof(PlatformException));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Process_NotifyRethrow_Test()
        {
            IEnumerable<IExceptionHandler> handlers = new List<IExceptionHandler>(
                new IExceptionHandler[] {
                    new ReplaceHandler("An error occured in the platform", typeof(PlatformException))
                }
            );
            ExceptionManager manager = new ExceptionManager(PostHandlingAction.NotifyRethrow, handlers);
            Exception exceptionToHandle = new ArgumentNullException("argument", "Argument Null Exception");
            manager.Process(() => { ThrowArgumentNullException(); });
        }
        [TestMethod]
        [ExpectedException(typeof(PlatformException))]
        public void Process_ThrowNewException_Test()
        {
            IEnumerable<IExceptionHandler> handlers = new List<IExceptionHandler>(
                new IExceptionHandler[] {
                    new ReplaceHandler("An error occured in the platform", typeof(PlatformException))
                }
            );
            ExceptionManager manager = new ExceptionManager(PostHandlingAction.ThrowNewException, handlers);
            Exception exceptionToHandle = new ArgumentNullException("argument", "Argument Null Exception");
            manager.Process(() => { ThrowArgumentNullException(); });
        }
        [TestMethod]
        public void Process_None_Test()
        {
            IEnumerable<IExceptionHandler> handlers = new List<IExceptionHandler>(
                new IExceptionHandler[] {
                    new ReplaceHandler("An error occured in the platform", typeof(PlatformException))
                }
            );
            ExceptionManager manager = new ExceptionManager(PostHandlingAction.None, handlers);
            Exception exceptionToHandle = new ArgumentNullException("argument", "Argument Null Exception");
            manager.Process(() => { ThrowArgumentNullException(); });
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Process_WithReturn_NotifyRethrow_Test()
        {
            IEnumerable<IExceptionHandler> handlers = new List<IExceptionHandler>(
                new IExceptionHandler[] {
                    new ReplaceHandler("An error occured in the platform", typeof(PlatformException))
                }
            );
            ExceptionManager manager = new ExceptionManager(PostHandlingAction.NotifyRethrow, handlers);
            Exception exceptionToHandle = new ArgumentNullException("argument", "Argument Null Exception");
            manager.Process<bool>(ThrowArgumentNullExceptionWithReturn, false);
        }
        [TestMethod]
        [ExpectedException(typeof(PlatformException))]
        public void Process_WithReturn_ThrowNewException_Test()
        {
            IEnumerable<IExceptionHandler> handlers = new List<IExceptionHandler>(
                new IExceptionHandler[] {
                    new ReplaceHandler("An error occured in the platform", typeof(PlatformException))
                }
            );
            ExceptionManager manager = new ExceptionManager(PostHandlingAction.ThrowNewException, handlers);
            Exception exceptionToHandle = new ArgumentNullException("argument", "Argument Null Exception");
            manager.Process<bool>(ThrowArgumentNullExceptionWithReturn, false);
        }
        [TestMethod]
        public void Process_WithReturn_None_Test()
        {
            IEnumerable<IExceptionHandler> handlers = new List<IExceptionHandler>(
                new IExceptionHandler[] {
                    new ReplaceHandler("An error occured in the platform", typeof(PlatformException))
                }
            );
            ExceptionManager manager = new ExceptionManager(PostHandlingAction.None, handlers);
            Exception exceptionToHandle = new ArgumentNullException("argument", "Argument Null Exception");
            manager.Process<bool>(ThrowArgumentNullExceptionWithReturn, false);
        }
        private void BlankMethod()
        {
        }
        private void ThrowArgumentNullException()
        {
            throw new ArgumentNullException("argument", "Argument Null Exception");
        }
        private bool ThrowArgumentNullExceptionWithReturn()
        {
            throw new ArgumentNullException("argument", "Argument Null Exception");
        }
    }
}

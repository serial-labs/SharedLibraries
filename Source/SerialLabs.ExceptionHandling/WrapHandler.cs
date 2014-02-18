﻿using SerialLabs.ExceptionHandling.Properties;
using System;
using System.Globalization;

namespace SerialLabs.ExceptionHandling
{
    /// <summary>
    /// Wraps the current exception in the handling chain with a new exception of a specified type.
    /// </summary>
    public class WrapHandler : ExceptionHandler
    {
        private readonly IStringResolver exceptionMessageResolver;
        private readonly Type wrapExceptionType;

        /// <summary>
        /// Initialize a new instance of the <see cref="WrapHandler"/> class with an exception message and the type of <see cref="Exception"/> to use.
        /// </summary>
        /// <param name="exceptionMessage">The exception message.</param>
        /// <param name="wrapExceptionType">The type of <see cref="Exception"/> to use to wrap.</param>
        public WrapHandler(string exceptionMessage, Type wrapExceptionType)
            : this(new ConstantStringResolver(exceptionMessage), wrapExceptionType)
        { }

        /// <summary>
        /// Initialize a new instance of the <see cref="WrapHandler"/> class with an exception message resolver
        /// and the type of <see cref="Exception"/> to use.
        /// </summary>
        /// <param name="exceptionMessageResolver">The exception message resolver.</param>
        /// <param name="wrapExceptionType">The type of <see cref="Exception"/> to use to wrap.</param>
        public WrapHandler(IStringResolver exceptionMessageResolver, Type wrapExceptionType)
            : base("WrapHandler")
        {
            if (wrapExceptionType == null) throw new ArgumentNullException("wrapExceptionType");
            if (!typeof(Exception).IsAssignableFrom(wrapExceptionType))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.ExceptionTypeNotException, wrapExceptionType.Name), "wrapExceptionType");
            }

            this.exceptionMessageResolver = exceptionMessageResolver;
            this.wrapExceptionType = wrapExceptionType;
        }

        /// <summary>
        /// <para>Gets the <see cref="Type"/> of exception to wrap the original exception with.</para>
        /// </summary>
        /// <value>
        /// <para>The <see cref="Type"/> of exception to wrap the original exception with.</para>
        /// </value>
        public Type WrapExceptionType
        {
            get { return wrapExceptionType; }
        }

        /// <summary>
        /// <para>Gets the message of the wrapped exception.</para>
        /// </summary>
        /// <value>
        /// <para>The message of the wrapped exception.</para>
        /// </value>
        public string WrapExceptionMessage
        {
            get { return exceptionMessageResolver.GetString(); }
        }

        /// <summary>
        /// <para>Wraps the <see cref="Exception"/> with the configuration exception type.</para>
        /// </summary>
        /// <param name="exception"><para>The exception to handle.</para></param>        
        /// <returns><para>Modified exception to pass to the next exceptionHandlerData in the chain.</para></returns>        
        public override Exception HandleException(Exception exception)
        {
            return WrapException(exception, WrapExceptionMessage);
        }

        private Exception WrapException(Exception originalException, string wrapExceptionMessage)
        {
            object[] extraParameters = new object[2];
            extraParameters[0] = wrapExceptionMessage;
            extraParameters[1] = originalException;
            return (Exception)Activator.CreateInstance(wrapExceptionType, extraParameters);
        }
    }
}

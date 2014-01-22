using System;
using System.Diagnostics;

namespace SerialLabs.Logging.Configuration
{
    /// <summary>
    /// Represents the configuration settings that describe a <see cref="EmailTraceListener"/>.
    /// </summary>
    public class EmailTraceListenerData
    {
        private const string toAddressProperty = "toAddress";
        private const string fromAddressProperty = "fromAddress";
        private const string subjectLineStarterProperty = "subjectLineStarter";
        private const string subjectLineEnderProperty = "subjectLineEnder";
        private const string smtpServerProperty = "smtpServer";
        private const string smtpPortProperty = "smtpPort";
        private const string formatterNameProperty = "formatter";
        private const string authenticationModeProperty = "authenticationMode";
        private const string useSSLProperty = "useSSL";
        private const string userNameProperty = "userName";
        private const string passwordProperty = "password";

        /// <summary>
        /// Initializes a <see cref="EmailTraceListenerData"/>.
        /// </summary>
        public EmailTraceListenerData()
        {
            ListenerDataType = typeof(EmailTraceListenerData);
        }

        /// <summary>
        /// Initializes a <see cref="EmailTraceListenerData"/> with a toaddress, 
        /// fromaddress, subjectLineStarter, subjectLineEnder, smtpServer, and a formatter name.
        /// Default value for the SMTP port is 25
        /// </summary>
        /// <param name="toAddress">A semicolon delimited string the represents to whom the email should be sent.</param>
        /// <param name="fromAddress">Represents from whom the email is sent.</param>
        /// <param name="subjectLineStarter">Starting text for the subject line.</param>
        /// <param name="subjectLineEnder">Ending text for the subject line.</param>
        /// <param name="smtpServer">The name of the SMTP server.</param>
        /// <param name="formatterName">The name of the Formatter <see cref="ILogFormatter"/> which determines how the
        ///email message should be formatted</param>
        public EmailTraceListenerData(string toAddress, string fromAddress, string subjectLineStarter, string subjectLineEnder, string smtpServer, string formatterName)
            : this(toAddress, fromAddress, subjectLineStarter, subjectLineEnder, smtpServer, 25, formatterName)
        {

        }

        /// <summary>
        /// Initializes a <see cref="EmailTraceListenerData"/> with a toaddress, 
        /// fromaddress, subjectLineStarter, subjectLineEnder, smtpServer, and a formatter name.
        /// </summary>
        /// <param name="toAddress">A semicolon delimited string the represents to whom the email should be sent.</param>
        /// <param name="fromAddress">Represents from whom the email is sent.</param>
        /// <param name="subjectLineStarter">Starting text for the subject line.</param>
        /// <param name="subjectLineEnder">Ending text for the subject line.</param>
        /// <param name="smtpServer">The name of the SMTP server.</param>
        /// <param name="smtpPort">The port on the SMTP server to use for sending the email.</param>
        /// <param name="formatterName">The name of the Formatter <see cref="ILogFormatter"/> which determines how the
        ///email message should be formatted</param>
        public EmailTraceListenerData(string toAddress, string fromAddress, string subjectLineStarter, string subjectLineEnder, string smtpServer, int smtpPort, string formatterName)
            : this("unnamed", toAddress, fromAddress, subjectLineStarter, subjectLineEnder, smtpServer, smtpPort, formatterName)
        {
        }

        /// <summary>
        /// Initializes a <see cref="EmailTraceListenerData"/> with a toaddress, 
        /// fromaddress, subjectLineStarter, subjectLineEnder, smtpServer, and a formatter name.
        /// </summary>
        /// <param name="name">The name of this listener</param>        
        /// <param name="toAddress">A semicolon delimited string the represents to whom the email should be sent.</param>
        /// <param name="fromAddress">Represents from whom the email is sent.</param>
        /// <param name="subjectLineStarter">Starting text for the subject line.</param>
        /// <param name="subjectLineEnder">Ending text for the subject line.</param>
        /// <param name="smtpServer">The name of the SMTP server.</param>
        /// <param name="smtpPort">The port on the SMTP server to use for sending the email.</param>
        /// <param name="formatterName">The name of the Formatter <see cref="ILogFormatter"/> which determines how the
        ///email message should be formatted</param>
        public EmailTraceListenerData(string name, string toAddress, string fromAddress, string subjectLineStarter, string subjectLineEnder, string smtpServer, int smtpPort, string formatterName)
            : this(name, toAddress, fromAddress, subjectLineStarter, subjectLineEnder, smtpServer, smtpPort, formatterName, TraceOptions.None)
        {
        }

        /// <summary>
        /// Initializes a <see cref="EmailTraceListenerData"/> with a toaddress, 
        /// fromaddress, subjectLineStarter, subjectLineEnder, smtpServer, a formatter name and trace options.
        /// </summary>
        /// <param name="name">The name of this listener</param>        
        /// <param name="toAddress">A semicolon delimited string the represents to whom the email should be sent.</param>
        /// <param name="fromAddress">Represents from whom the email is sent.</param>
        /// <param name="subjectLineStarter">Starting text for the subject line.</param>
        /// <param name="subjectLineEnder">Ending text for the subject line.</param>
        /// <param name="smtpServer">The name of the SMTP server.</param>
        /// <param name="smtpPort">The port on the SMTP server to use for sending the email.</param>
        /// <param name="formatterName">The name of the Formatter <see cref="ILogFormatter"/> which determines how the
        ///email message should be formatted</param>
        ///<param name="traceOutputOptions">The trace options.</param>
        public EmailTraceListenerData(string name, string toAddress, string fromAddress, string subjectLineStarter, string subjectLineEnder, string smtpServer, int smtpPort, string formatterName, TraceOptions traceOutputOptions)
        {
            this.ToAddress = toAddress;
            this.FromAddress = fromAddress;
            this.SubjectLineStarter = subjectLineStarter;
            this.SubjectLineEnder = subjectLineEnder;
            this.SmtpServer = smtpServer;
            this.SmtpPort = smtpPort;
            this.Formatter = formatterName;
        }

        /// <summary>
        /// Initializes a <see cref="EmailTraceListenerData"/> with a toaddress, 
        /// fromaddress, subjectLineStarter, subjectLineEnder, smtpServer, a formatter name and trace options.
        /// </summary>
        /// <param name="name">The name of this listener</param>        
        /// <param name="toAddress">A semicolon delimited string the represents to whom the email should be sent.</param>
        /// <param name="fromAddress">Represents from whom the email is sent.</param>
        /// <param name="subjectLineStarter">Starting text for the subject line.</param>
        /// <param name="subjectLineEnder">Ending text for the subject line.</param>
        /// <param name="smtpServer">The name of the SMTP server.</param>
        /// <param name="smtpPort">The port on the SMTP server to use for sending the email.</param>
        /// <param name="formatterName">The name of the Formatter <see cref="ILogFormatter"/> which determines how the
        ///email message should be formatted</param>
        /// <param name="traceOutputOptions">The trace options.</param>
        /// <param name="filter">The filter to apply.</param>
        public EmailTraceListenerData(string name, string toAddress, string fromAddress, string subjectLineStarter, string subjectLineEnder, string smtpServer, int smtpPort, string formatterName, TraceOptions traceOutputOptions, SourceLevels filter)
        {
            this.ToAddress = toAddress;
            this.FromAddress = fromAddress;
            this.SubjectLineStarter = subjectLineStarter;
            this.SubjectLineEnder = subjectLineEnder;
            this.SmtpServer = smtpServer;
            this.SmtpPort = smtpPort;
            this.Formatter = formatterName;
        }

        /// <summary>
        /// Initializes a <see cref="EmailTraceListenerData"/> with a toaddress, 
        /// fromaddress, subjectLineStarter, subjectLineEnder, smtpServer, a formatter name, trace options
        /// and authentication information.
        /// </summary>
        /// <param name="name">The name of this listener</param>        
        /// <param name="toAddress">A semicolon delimited string the represents to whom the email should be sent.</param>
        /// <param name="fromAddress">Represents from whom the email is sent.</param>
        /// <param name="subjectLineStarter">Starting text for the subject line.</param>
        /// <param name="subjectLineEnder">Ending text for the subject line.</param>
        /// <param name="smtpServer">The name of the SMTP server.</param>
        /// <param name="smtpPort">The port on the SMTP server to use for sending the email.</param>
        /// <param name="formatterName">The name of the Formatter <see cref="ILogFormatter"/> which determines how the
        ///email message should be formatted</param>
        /// <param name="traceOutputOptions">The trace options.</param>
        /// <param name="filter">The filter to apply.</param>
        /// <param name="authenticationMode">Authenticate mode to use.</param>
        /// <param name="userName">User name to pass to the server if using <see cref="EmailAuthenticationMode.UserNameAndPassword"/>.</param>
        /// <param name="password">Password to pass to the server if using <see cref="EmailAuthenticationMode.UserNameAndPassword"/>.</param>
        /// <param name="useSSL">Connect to the server using SSL?</param>
        public EmailTraceListenerData(string name,
            string toAddress, string fromAddress,
            string subjectLineStarter, string subjectLineEnder,
            string smtpServer, int smtpPort,
            string formatterName, TraceOptions traceOutputOptions, SourceLevels filter,
            EmailAuthenticationMode authenticationMode, string userName, string password, bool useSSL)
        {
            this.ToAddress = toAddress;
            this.FromAddress = fromAddress;
            this.SubjectLineStarter = subjectLineStarter;
            this.SubjectLineEnder = subjectLineEnder;
            this.SmtpServer = smtpServer;
            this.SmtpPort = smtpPort;
            this.Formatter = formatterName;
            this.AuthenticationMode = authenticationMode;
            this.UserName = userName;
            this.Password = password;
            this.UseSSL = useSSL;
        }
        /// <summary>
        /// Listener DataType
        /// </summary>
        public Type ListenerDataType
        {
            get;
            set;
        }
        /// <summary>
        /// Gets and sets the ToAddress.  One or more email semicolon separated addresses.
        /// </summary>
        public string ToAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets and sets the FromAddress. Email address that messages will be sent from.
        /// </summary>
        public string FromAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets and sets the Subject prefix.
        /// </summary>
        public string SubjectLineStarter
        {
            get;
            set;
        }

        /// <summary>
        /// Gets and sets the Subject suffix.
        /// </summary>
        public string SubjectLineEnder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets and sets the SMTP server to use to send messages.
        /// </summary>
        public string SmtpServer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets and sets the SMTP port.
        /// </summary>
        public int SmtpPort
        {
            get;
            set;
        }

        /// <summary>
        /// Gets and sets the formatter name.
        /// </summary>
        public string Formatter
        {
            get;
            set;
        }

        /// <summary>
        /// How do you authenticate against the email server?
        /// </summary>
        public EmailAuthenticationMode AuthenticationMode
        {
            get;
            set;
        }

        /// <summary>
        /// Use SSL to connect to the email server?
        /// </summary>
        public bool UseSSL
        {
            get;
            set;
        }

        /// <summary>
        /// User name when authenticating with user name and password.
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Password when authenticating with user name and password.
        /// </summary>
        public string Password
        {
            get;
            set;
        }
    }
}

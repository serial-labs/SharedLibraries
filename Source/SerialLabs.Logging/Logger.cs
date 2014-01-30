using System.Diagnostics;

namespace SerialLabs.Logging
{
    /// <summary>
    /// Logging facade
    /// </summary>
    public class Logger
    {
        private ILogWriter logWriter;
        private bool canLog = false;
        private string name;
        /// <summary>
        /// Logger name
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="logWriter"></param>
        public Logger(ILogWriter logWriter)
            : this(logWriter, "Default")
        { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="logWriter"></param>
        /// <param name="name"></param>
        public Logger(ILogWriter logWriter, string name)
        {
            Guard.ArgumentNotNullOrEmpty(name, "name");
            this.name = name;
            this.logWriter = logWriter;
            if (this.logWriter != null)
                this.canLog = true;
        }
        /// <summary>
        /// Logs a information message
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        public void TraceInformation(string title, string message, int eventId)
        {
            TraceMessage(title, message, eventId, TraceEventType.Information);
        }
        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        public void TraceWarning(string title, string message, int eventId)
        {
            TraceMessage(title, message, eventId, TraceEventType.Warning);
        }
        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        public void TraceError(string title, string message, int eventId)
        {
            TraceMessage(title, message, eventId, TraceEventType.Error);
        }
        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        /// <param name="eventType"></param>
        public void TraceMessage(string title, string message, int eventId, TraceEventType eventType)
        {
            if (canLog)
                this.logWriter.Write(new LogEntry(this.Name, message, "General", 1, eventId, eventType, title, null));
        }
    }
}

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using SerialLabs.Logging.Formatters;
using SerialLabs.Logging.TraceListeners;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SerialLabs.Logging
{
    /// <summary>
    /// A <see cref="System.Diagnostics.TraceListener"/> that writes to a Cloud Storage,
    /// formatting the output with an <see cref="ILogFormatter"/>.
    /// </summary>
    public class FormattedCloudStorageTraceListener : FormattedTraceListenerBase
    {
        /// <summary>
        /// The name of the Azure Table where the log will be saved
        /// </summary>
        public const string AzureTableName = "ApplicationLogs";

        /// <summary>
        /// The cloud storage connection string where to save the log to.
        /// </summary>
        protected string _cloudStorageConnectionString;

        /// <summary>
        /// Creates a new instance of the <see cref="FormattedCloudStorageTraceListener"/>
        /// </summary>
        /// <param name="cloudStorageConnectionString"></param>
        public FormattedCloudStorageTraceListener(string cloudStorageConnectionString)
            : this(cloudStorageConnectionString, null)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="FormattedCloudStorageTraceListener"/>
        /// </summary>
        /// <param name="cloudStorageConnectionString"></param>
        /// <param name="formatter"></param>
        public FormattedCloudStorageTraceListener(string cloudStorageConnectionString, ILogFormatter formatter)
            : base(formatter)
        {
            Guard.ArgumentNotNullOrWhiteSpace<PlatformException>(cloudStorageConnectionString, "cloudStorageConnectionString");
            _cloudStorageConnectionString = cloudStorageConnectionString;
        }

        /// <summary>
        /// Write a message
        /// </summary>
        /// <param name="message"></param>
        public override void Write(string message)
        {
            LogEntry logEntry = CreateDefaultLogEntry(message);
            ExecuteWriteLog(logEntry);
        }

        /// <summary>
        /// Asynchronously write a message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task WriteAsync(string message)
        {
            LogEntry logEntry = CreateDefaultLogEntry(message);
            await ExecuteWriteLogAsync(logEntry);
        }

        /// <summary>
        /// WriteLine
        /// </summary>
        /// <param name="message"></param>
        public override void WriteLine(string message)
        {
            Write(message);
        }

        /// <summary>
        /// Asynchronous WriteLine
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task WriteLineAsync(string message)
        {
            await WriteAsync(message);
        }

        /// <summary>
        /// Trace data
        /// </summary>
        /// <param name="eventCache"></param>
        /// <param name="source"></param>
        /// <param name="eventType"></param>
        /// <param name="id"></param>
        /// <param name="data">Data to log. Can be a <see cref="String"/> or <see cref="LogEntry"/></param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if (this.Filter == null || this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                if (data is LogEntry)
                {
                    LogEntry logEntry = data as LogEntry;
                    ExecuteWriteLog(logEntry);
                }
                else if (data is String)
                {
                    Write(data as String);
                }
                else
                {
                    base.TraceData(eventCache, source, eventType, id, data);
                }
            }
        }

        /// <summary>
        /// CloudStorage write implementation
        /// </summary>
        /// <param name="logEntry"></param>
        protected void ExecuteWriteLog(LogEntry logEntry)
        {
            CloudStorageLogEntity entity = CloudStorageLogEntityManager.CreateFromLogEntry(logEntry, this.Formatter);
            if (entity == null)
                return;

            CloudTable table = GetTableReference();
            table.CreateIfNotExists();
            TableOperation insertOperation = TableOperation.Insert(entity);
            table.Execute(insertOperation);
        }

        /// <summary>
        /// CloudStorage asynchronous write implementation
        /// </summary>
        /// <param name="logEntry"></param>
        /// <returns></returns>
        protected async Task ExecuteWriteLogAsync(LogEntry logEntry)
        {
            CloudStorageLogEntity entity = CloudStorageLogEntityManager.CreateFromLogEntry(logEntry, this.Formatter);
            if (entity == null)
                return;

            CloudTable table = GetTableReference();
            await table.CreateIfNotExistsAsync();
            TableOperation insertOperation = TableOperation.Insert(entity);
            await table.ExecuteAsync(insertOperation);
        }

        /// <summary>
        /// Creates a default log entry
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual LogEntry CreateDefaultLogEntry(string message)
        {
            return new LogEntry
            {
                Severity = System.Diagnostics.TraceEventType.Information,
                TimeStamp = DateTime.UtcNow,
                Message = message,
                ApplicationName = "Undefined"
            };
        }

        private CloudTable GetTableReference()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_cloudStorageConnectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            return tableClient.GetTableReference(AzureTableName);
        }
    }
}

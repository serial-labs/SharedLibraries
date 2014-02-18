using SerialLabs.Logging.Formatters;
using System;
using System.Globalization;
using System.Linq;

namespace SerialLabs.Logging
{
    /// <summary>
    /// This class is responsible for managing <see cref="CloudStorageLogEntity"/> in the system.
    /// </summary>
    public static class CloudStorageLogEntityManager
    {
        /// <summary>
        /// Returns a formatted partition key
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static string CreatePartitionKey(string applicationName, DateTimeOffset timeStamp)
        {
            Guard.ArgumentNotNullOrWhiteSpace<PlatformException>(applicationName, "applicationName");

            return String.Format(CultureInfo.InvariantCulture, "{0}-{1}",
                applicationName.Trim(),
                timeStamp.ToString("yyyyMM"));
        }

        /// <summary>
        /// Creates a full populated new <see cref="CloudStorageLogEntity"/> from a given <see cref="LogEntry"/>
        /// </summary>
        /// <param name="logEntry"></param>
        /// <param name="formatter"></param>
        /// <returns></returns>
        public static CloudStorageLogEntity CreateFromLogEntry(LogEntry logEntry, ILogFormatter formatter)
        {
            Guard.ArgumentNotNull<PlatformException>(logEntry, "logEntry");
            CloudStorageLogEntity entity = new CloudStorageLogEntity
            {
                ApplicationName = String.IsNullOrWhiteSpace(logEntry.ApplicationName) ?
                    "Undefined" : logEntry.ApplicationName,
                EventId = logEntry.EventId,
                Category = logEntry.Categories.FirstOrDefault(),
                Priority = logEntry.Priority,
                Severity = logEntry.Severity.ToString(),
                Title = logEntry.Title,
                MachineName = logEntry.MachineName,
                AppDomainName = logEntry.AppDomainName,
                ProcessId = logEntry.ProcessId,
                ProcessName = logEntry.ProcessName,
                ThreadName = logEntry.ManagedThreadName,
                Win32ThreadId = logEntry.Win32ThreadId,
                Message = logEntry.Message
            };

            entity.Timestamp = new DateTimeOffset(logEntry.TimeStamp, TimeSpan.Zero);
            entity.PartitionKey = CreatePartitionKey(entity.ApplicationName, entity.Timestamp);
            entity.RowKey = Guid.NewGuid().ToString();
            entity.FormattedMessage = formatter != null ? formatter.Format(logEntry) : logEntry.Message;

            return entity;
        }
    }
}

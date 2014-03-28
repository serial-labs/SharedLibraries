using SerialLabs.Logging.Formatters;
using System;
using System.Globalization;
using System.Linq;

namespace SerialLabs.Logging.AzureTable
{
    /// <summary>
    /// This class is responsible for managing <see cref="ApplicationLogEntity"/> in the system.
    /// </summary>
    public static class ApplicationLogEntityManager
    {
        /// <summary>
        /// Returns a formatted row key
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public static string CreateRowKey(SortOrder sortOrder = SortOrder.Descending)
        {
            return CreateRowKey(Guid.NewGuid(), DateTime.UtcNow, sortOrder);
        }

        /// <summary>
        /// Returns a formatted row key
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string CreateRowKey(Guid guid, SortOrder sortOrder = SortOrder.Descending)
        {
            return CreateRowKey(guid, DateTime.UtcNow, sortOrder);
        }

        /// <summary>
        /// Returns a formatted row key
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="date"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public static string CreateRowKey(Guid guid, DateTime date, SortOrder sortOrder)
        {
            long ticks = date.Ticks;
            if (sortOrder == SortOrder.Descending)
                ticks = DateTime.MaxValue.Ticks - date.Ticks;

            return String.Format(CultureInfo.InvariantCulture, "{0:D19}_{1}", ticks, guid.ToString());
        }

        /// <summary>
        /// Returns a formatted partition key
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static string CreatePartitionKey(string applicationName, DateTimeOffset timeStamp)
        {
            Guard.ArgumentNotNullOrWhiteSpace<PlatformException>(applicationName, "applicationName");

            return String.Format(CultureInfo.InvariantCulture, "{0}_{1}",
                applicationName.Trim(),
                timeStamp.ToString("yyyyMM"));
        }

        /// <summary>
        /// Creates a full populated new <see cref="ApplicationLogEntity"/> from a given <see cref="LogEntry"/>
        /// </summary>
        /// <param name="logEntry"></param>
        /// <param name="formatter"></param>
        /// <returns></returns>
        public static ApplicationLogEntity CreateFromLogEntry(LogEntry logEntry, ILogFormatter formatter)
        {
            Guard.ArgumentNotNull<PlatformException>(logEntry, "logEntry");
            ApplicationLogEntity entity = new ApplicationLogEntity
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

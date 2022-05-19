using SerialLabs.Logging.Formatters;
using System;
using System.Diagnostics;
using System.Globalization;
using seriallabs;

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
            //long ticks = date.Ticks;
            string uid = "";
            if (sortOrder == SortOrder.Descending)
            {
                //ticks = DateTime.MaxValue.Ticks - date.Ticks;
                //ticks = new DateTime(2080,01,01).Ticks - date.Ticks;
                uid = CoolTools.UniqueIdGenerator.generateUIDdesc();
            }else{ uid = CoolTools.UniqueIdGenerator.generateUIDdesc();}

            return uid;
        }

        /// <summary>
        /// Returns a formatted partition key
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static string CreatePartitionKey(string applicationName, DateTimeOffset timeStamp, string keyword)
        {
            Guard.ArgumentNotNullOrWhiteSpace<PlatformException>(applicationName, "applicationName");
            var m = timeStamp.Month;
            var q = (m+2) / 3 ;
            string date = $"{timeStamp:yyyy}Q{q}";
            string appName = applicationName.Replace("MyBlazon", "").Trim();
            return String.Format(CultureInfo.InvariantCulture, "{0}-{1}-{2}",
                date,
                keyword,
                appName
                );
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
                AppDomainName = logEntry.AppDomainName,
                ApplicationName = logEntry.ApplicationName,
                Category = logEntry.Categories.Join(","),
                EventId = logEntry.EventId,
                MachineName = Environment.MachineName,
                Message = logEntry.Message,
                Priority = logEntry.Priority,
                ProcessId = logEntry.ProcessId,
                ProcessName = logEntry.ProcessName,
                Severity = logEntry.Severity.ToString(),
                ThreadName = logEntry.ManagedThreadName,
                Title = logEntry.Title,
                Win32ThreadId = logEntry.Win32ThreadId
            };

            entity.Timestamp = new DateTimeOffset(logEntry.TimeStamp, TimeSpan.Zero);
            entity.PartitionKey = CreatePartitionKey(entity.ApplicationName, entity.Timestamp, entity.Category);
            entity.RowKey = ApplicationLogEntityManager.CreateRowKey(SortOrder.Descending);
            entity.FormattedMessage = formatter != null ? formatter.Format(logEntry) : logEntry.Message;

            return entity;
        }

        public static ApplicationLogEntity Create(string title, string rawMessage, string formattedMessage, int eventId, string applicationName = "Default", TraceEventType severity = TraceEventType.Error, string category = "General", int priority = 1, string processId = null, string processName = null, string threadName = null, string wind32ThreadId = null)
        {
            return new ApplicationLogEntity
            {
                AppDomainName = "Unknown AppDomain",
                ApplicationName = applicationName,
                Category = category,
                EventId = eventId,
                FormattedMessage = formattedMessage,
                MachineName = Environment.MachineName,
                Message = rawMessage,
                PartitionKey = CreatePartitionKey(applicationName, DateTimeOffset.UtcNow,category+"/"+severity),
                Priority = priority,
                ProcessId = processId,
                ProcessName = processName,
                RowKey = ApplicationLogEntityManager.CreateRowKey(SortOrder.Descending),
                Severity = severity.ToString(),
                ThreadName = threadName,
                Timestamp = DateTimeOffset.UtcNow,
                Title = title,
                Win32ThreadId = wind32ThreadId
            };
        }
    }
}

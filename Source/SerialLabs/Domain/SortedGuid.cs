using System;

namespace SerialLabs.Data
{
    /// <summary>
    /// A sorted Guid
    /// This is a composite guid which is associated with a time stamp.
    /// This time stamp helps to order the guid in a table storage.
    /// </summary>
    public class SortedGuid
    {
        protected const char Separator = '_';

        /// <summary>
        /// TimeStamp
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Guid
        /// </summary>
        public Guid Guid { get; set; }
    }
}

using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Runtime.Serialization;

namespace SerialLabs.Logging
{
    [Serializable]
    [DataContractAttribute(Name = "Log", Namespace = PlatformConstants.XmlNamespace)]
    public class CloudStorageLogEntity : TableEntity
    {
        #region Properties
        /// <summary>
        /// ApplicationName
        /// </summary>
        [DataMemberAttribute]
        public string ApplicationName { get; set; }
        /// <summary>
        /// EventId
        /// </summary>
        [DataMemberAttribute]
        public int EventId { get; set; }
        /// <summary>
        /// Priority
        /// </summary>
        [DataMemberAttribute]
        public int Priority { get; set; }
        /// <summary>
        /// Severity
        /// </summary>
        [DataMemberAttribute]
        public string Severity { get; set; }
        /// <summary>
        /// Category
        /// </summary>
        [DataMemberAttribute]
        public string Category { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        [DataMemberAttribute]
        public string Title { get; set; }
        /// <summary>
        /// MachineName
        /// </summary>
        [DataMemberAttribute]
        public string MachineName { get; set; }
        /// <summary>
        /// AppDomainName
        /// </summary>
        [DataMemberAttribute]
        public string AppDomainName { get; set; }
        /// <summary>
        /// ProcessId
        /// </summary>
        [DataMemberAttribute]
        public string ProcessId { get; set; }
        /// <summary>
        /// ProcessName
        /// </summary>
        [DataMemberAttribute]
        public string ProcessName { get; set; }
        /// <summary>
        /// ThreadName
        /// </summary>
        [DataMemberAttribute]
        public string ThreadName { get; set; }
        /// <summary>
        /// Win32ThreadId
        /// </summary>
        [DataMemberAttribute]
        public string Win32ThreadId { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        [DataMemberAttribute]
        public string Message { get; set; }
        /// <summary>
        /// FormattedMessage
        /// </summary>
        [DataMemberAttribute]
        public string FormattedMessage { get; set; }
        #endregion
    }
}

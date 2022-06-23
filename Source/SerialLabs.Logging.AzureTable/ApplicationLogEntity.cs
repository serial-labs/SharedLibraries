using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Runtime.Serialization;

namespace SerialLabs.Logging.AzureTable
{
    [Serializable]
    [DataContractAttribute(Name = "Log", Namespace = PlatformConstants.XmlNamespace)]
    public class ApplicationLogEntity : TableEntity
    {
        #region Properties
        [DataMemberAttribute]
        public string ApplicationName { get; set; }

        [DataMemberAttribute]
        public int EventId { get; set; }
        
        [DataMemberAttribute]
        public int Priority { get; set; }
        
        [DataMemberAttribute]
        public string Severity { get; set; }
        
        [DataMemberAttribute]
        public string Category { get; set; }
        
        [DataMemberAttribute]
        public string Title { get; set; }
        
        [DataMemberAttribute]
        public string MachineName { get; set; }
        
        //[DataMemberAttribute]
        //public string AppDomainName { get; set; }
        
        [DataMemberAttribute]
        public string ProcessId { get; set; }
        public string SessionId { get; set; }
        
        [DataMemberAttribute]
        public string ProcessName { get; set; }
        
        [DataMemberAttribute]
        public string ThreadName { get; set; }
        
        [DataMemberAttribute]
        public string Win32ThreadId { get; set; }
        
        [DataMemberAttribute]
        public string Message { get; set; }
        
        [DataMemberAttribute]
        public string FormattedMessage { get; set; }
        #endregion
    }
}

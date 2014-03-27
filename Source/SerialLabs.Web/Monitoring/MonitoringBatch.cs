using System.Collections.Generic;

namespace SerialLabs.Web.Monitoring
{
    /// <summary>
    /// Encapsulate the status check results
    /// </summary>
    public class MonitoringJobBatch
    {
        /// <summary>
        /// The name is this set of status checks
        /// </summary>
        public string Label { get; set; }
        public IEnumerable<MonitoringJob> JobList { get; set; }

        public static MonitoringJobBatch Create(string label, IEnumerable<MonitoringJob> jobs)
        {
            return new MonitoringJobBatch
            {
                Label = label,
                JobList = new List<MonitoringJob>(jobs)
            };
        }
    }
}

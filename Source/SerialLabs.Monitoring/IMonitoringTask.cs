using System.Threading.Tasks;

namespace SerialLabs.Monitoring
{
    /// <summary>
    /// Monitoring Task interface
    /// </summary>
    public interface IMonitoringTask
    {
        MonitoringResult Execute();
        Task<MonitoringResult> ExecuteAsync();
        string Label { get; }
        string Type { get; }
    }
}

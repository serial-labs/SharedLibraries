using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.Web.Monitoring
{
    public interface IMonitoringProvider
    {
        MonitoringJobBatch Execute();
    }
}

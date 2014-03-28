using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.Monitoring
{
    public static class MonitoringManager
    {
        public static IEnumerable<MonitoringResult> ExecuteTasks(IEnumerable<IMonitoringTask> tasks)
        {
            Guard.ArgumentNotNull(tasks, "tasks");

            foreach (var item in tasks)
            {
                yield return item.Execute();
            }
        }

        public static async Task<IEnumerable<MonitoringResult>> ExecuteTasksAsync(IEnumerable<IMonitoringTask> tasks)
        {
            Guard.ArgumentNotNull(tasks, "tasks");

            List<MonitoringResult> result = new List<MonitoringResult>();

            foreach (var item in tasks)
            {
                result.Add(await item.ExecuteAsync());
            }

            return result;
        }

        /// <summary>
        /// Gets the local ip address
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIpAddress()
        {
            return GetLocalIpAddress(GetHostName());
        }

        /// <summary>
        /// Gets the local ip address
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIpAddress(string hostName)
        {
            IPHostEntry host = Dns.GetHostEntry(hostName);
            string localIp = "";
            foreach (var item in host.AddressList)
            {
                if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIp = item.ToString();
                }
            }
            return localIp;
        }

        /// <summary>
        /// Gets the local host name
        /// </summary>
        /// <returns></returns>
        public static string GetHostName()
        {
            return Dns.GetHostName();
        }


        public static void FormatExceptionInfos(Exception ex, StringBuilder appendTo)
        {
            if (ex == null)
                return;
            string result = FormatExceptionInfos(ex);
            if (appendTo == null)
                return;
            appendTo.AppendLine(result);

            if (ex.InnerException != null)
                FormatExceptionInfos(ex.InnerException, appendTo);

        }

        public static string FormatExceptionInfos(Exception exception)
        {
            if (exception == null)
                return null;

            return String.Format("ExceptionType: {0}\nMessage: {1}\nStack:{2}\n\n", exception.GetType(), exception.Message, exception.StackTrace);
        }
    }
}

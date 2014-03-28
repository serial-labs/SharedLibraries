
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.Monitoring
{
    /// <summary>
    /// A base class providing comming features for a monitoring task.
    /// </summary>
    public abstract class MonitoringTask : IMonitoringTask
    {
        /// <summary>
        /// Label
        /// </summary>
        public string Label { get; protected set; }

        protected MonitoringTask()
            : this("Unamed task")
        { }

        protected MonitoringTask(string label)
        {
            Guard.ArgumentNotNullOrWhiteSpace(label, "label");
            Label = label;
        }

        /// <summary>
        /// Execute the task and return the result.
        /// </summary>
        /// <returns></returns>
        public MonitoringResult Execute()
        {
            MonitoringResult result = new MonitoringResult();
            result.Title = Label;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            try
            {
                ExecuteCore();
            }
            catch (Exception ex)
            {
                result.Error = ex;
                StringBuilder builder = new StringBuilder();
                MonitoringManager.FormatExceptionInfos(ex, builder);
                result.AdditionalInfos = builder.ToString();
                builder.Clear();
                builder = null;
            }
            timer.Stop();
            result.Duration = timer.Elapsed;
            return result;
        }

        /// <summary>
        /// Execute the task asynchronously and return the result.
        /// </summary>
        /// <returns></returns>
        public async Task<MonitoringResult> ExecuteAsync()
        {
            MonitoringResult result = new MonitoringResult();
            result.Title = Label;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            try
            {
                await ExecuteCoreAsync();
            }
            catch (Exception ex)
            {
                result.Error = ex;
                StringBuilder builder = new StringBuilder();
                MonitoringManager.FormatExceptionInfos(ex, builder);
                result.AdditionalInfos = builder.ToString();
                builder.Clear();
                builder = null;
            }
            timer.Stop();
            result.Duration = timer.Elapsed;
            return result;
        }

        /// <summary>
        /// Synchronous implementation of the task's execution
        /// </summary>
        /// <returns></returns>
        protected abstract void ExecuteCore();

        /// <summary>
        /// Asynchronous implementation of the task's execution
        /// </summary>
        /// <returns></returns>
        protected abstract Task ExecuteCoreAsync();
    }
}

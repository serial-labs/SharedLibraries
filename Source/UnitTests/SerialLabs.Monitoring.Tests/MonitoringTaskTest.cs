using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace SerialLabs.Monitoring.Tests
{
    [TestClass]
    public class MonitoringTaskTest
    {
        private const string TestMonitoringTaskName = "FakeMonitoringTask";

        [TestMethod]
        public void CreateMonitoringTask_WithSuccess()
        {
            FakeMonitoringTask task = new FakeMonitoringTask(TestMonitoringTaskName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateMonitoringTask_NoLabel_WithException()
        {
            FakeMonitoringTask task = new FakeMonitoringTask(null);
        }

        [TestMethod]
        public void ExecuteMonitoringTask_WithSuccess()
        {
            FakeMonitoringTask task = new FakeMonitoringTask(TestMonitoringTaskName);
            MonitoringResult result = task.Execute();

            Assert.IsTrue(task.HasBeenExecuted);
            Assert.AreEqual(result.Title, TestMonitoringTaskName);
        }

        [TestMethod]
        public async Task ExecuteMonitoringTaskAsync_WithSuccess()
        {
            FakeMonitoringTask task = new FakeMonitoringTask(TestMonitoringTaskName);
            MonitoringResult result = await task.ExecuteAsync();

            Assert.IsTrue(task.HasBeenExecutedAsync);
            Assert.AreEqual(result.Title, TestMonitoringTaskName);
        }
    }

    class FakeMonitoringTask : MonitoringTask
    {
        public bool HasBeenExecuted { get; private set; }
        public bool HasBeenExecutedAsync { get; private set; }

        public FakeMonitoringTask(string label)
            : base(label)
        { }

        protected override void ExecuteCore()
        {
            HasBeenExecuted = true;
        }

        protected override Task ExecuteCoreAsync()
        {
            HasBeenExecutedAsync = true;
            return Task.FromResult(HasBeenExecutedAsync);
        }
    }
}

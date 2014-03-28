using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SerialLabs.Monitoring
{
    public class SqlStorageMonitoringTask : MonitoringTask
    {
        protected string ConnectionString { get; private set; }

        public SqlStorageMonitoringTask(string label, string connectionString)
            : base(label)
        {
            Guard.ArgumentNotNullOrWhiteSpace(label, "label");
            Guard.ArgumentNotNullOrWhiteSpace(connectionString, "connectionString");
            this.ConnectionString = connectionString;
        }

        protected override void ExecuteCore()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT GETUTCDATE()", connection);
                object result = command.ExecuteScalar();
            }
        }
        protected override async Task ExecuteCoreAsync()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT GETUTCDATE()", connection);
                await command.ExecuteScalarAsync();
            }
        }
    }
}

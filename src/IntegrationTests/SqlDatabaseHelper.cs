using System;
using System.Data.SqlClient;
using System.Text;
using TinyReturns.SharedKernel;

namespace TinyReturns.IntegrationTests
{
    public sealed class SqlDatabaseHelper
    {
        private readonly ISystemLog _systemLog;

        public SqlDatabaseHelper()
        {
            var serviceLocator = new ServiceLocatorForIntegrationTests();
            _systemLog = serviceLocator.GetService<ISystemLog>();
        }

        public void ConnectionExecuteWithLog(
            string connectionString,
            Action<SqlConnection> connectionAction,
            string logSql)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                _systemLog.Info("Executing: " + logSql);

                sqlConnection.Open();
                connectionAction(sqlConnection);
                sqlConnection.Close();
            }
        }

        public void ConnectionExecuteWithLog(
            string connectionString,
            Action<SqlConnection> connectionAction,
            string logSql,
            object values)
        {
            var propertyInfos = values.GetType().GetProperties();

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Executing:");
            stringBuilder.AppendLine(logSql);
            stringBuilder.AppendLine("Values:");

            foreach (var info in propertyInfos)
            {
                stringBuilder.Append("@" + info.Name + " = ");
                var value = info.GetValue(values, null);

                if (value == null)
                    stringBuilder.AppendLine("NULL,");
                else
                    stringBuilder.AppendLine(value.ToString());
            }

            _systemLog.Info(stringBuilder.ToString());

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                connectionAction(sqlConnection);
                sqlConnection.Close();
            }
        }
    }
}
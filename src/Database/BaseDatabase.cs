﻿using System;
using System.Data.SqlClient;
using System.Text;
using TinyReturns.SharedKernel;

namespace TinyReturns.Database
{
    public abstract class BaseDatabase
    {
        protected ISystemLog SystemLog;

        protected BaseDatabase(
            ISystemLog systemLog)
        {
            SystemLog = systemLog;
        }

        protected abstract string DefaultConnectionString { get; }

        protected void ConnectionExecuteWithLog(
            Action<SqlConnection> connectionAction,
            string logSql)
        {
            using (var sqlConnection = new SqlConnection(DefaultConnectionString))
            {
                SystemLog.Info("Executing: " + logSql);

                sqlConnection.Open();
                connectionAction(sqlConnection);
                sqlConnection.Close();
            }
        }

        protected void ConnectionExecuteWithLog(
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

            SystemLog.Info(stringBuilder.ToString());

            using (var sqlConnection = new SqlConnection(DefaultConnectionString))
            {
                sqlConnection.Open();
                connectionAction(sqlConnection);
                sqlConnection.Close();
            }
        }

    }

}
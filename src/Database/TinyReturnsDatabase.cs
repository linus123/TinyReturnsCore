﻿using System.Linq;
using Dapper;
using TinyReturns.Core;
using TinyReturns.Core.DataRepositories;

namespace TinyReturns.Database
{
    public class TinyReturnsDatabase :
        BaseDatabase,
        IMonthlyReturnsDataGateway
    {
        private readonly ITinyReturnsDatabaseSettings _tinyReturnsDatabaseSettings;

        public TinyReturnsDatabase(
            ITinyReturnsDatabaseSettings tinyReturnsDatabaseSettings,
            ISystemLog systemLog) : base(systemLog)
        {
            _tinyReturnsDatabaseSettings = tinyReturnsDatabaseSettings;
        }

        protected override string DefaultConnectionString
        {
            get { return _tinyReturnsDatabaseSettings.ReturnsDatabaseConnectionString; }
        }

        public void InsertMonthlyReturns(MonthlyReturnDto[] monthlyReturns)
        {
            const string sql = @"
INSERT INTO [MonthlyReturn]
           ([ReturnSeriesId]
           ,[Year]
           ,[Month]
           ,[ReturnValue])
     VALUES
           (@ReturnSeriesId
           ,@Year
           ,@Month
           ,@ReturnValue)
";

            ConnectionExecuteWithLog(
                connection =>
                {
                    connection.Execute(sql, monthlyReturns);
                },
                sql,
                monthlyReturns);
        }

        public MonthlyReturnDto[] GetMonthlyReturns(int returnSeriesId)
        {
            const string sql = @"
SELECT
        [ReturnSeriesId]
        ,[Year]
        ,[Month]
        ,[ReturnValue]
    FROM
        [MonthlyReturn]
    WHERE
        ReturnSeriesId = @ReturnSeriesId";

            MonthlyReturnDto[] result = null;

            var paramObject = new { ReturnSeriesId = returnSeriesId };

            ConnectionExecuteWithLog(
                connection =>
                {
                    result = connection.Query<MonthlyReturnDto>(sql, paramObject).ToArray();
                },
                sql,
                paramObject);

            return result;
        }

        public MonthlyReturnDto[] GetMonthlyReturns(
            int[] returnSeriesIds)
        {
            const string sqlTemplate = @"
SELECT
        [ReturnSeriesId]
        ,[Year]
        ,[Month]
        ,[ReturnValue]
    FROM
        [MonthlyReturn]
    WHERE
        ReturnSeriesId IN ({0})";

            var commaSep = returnSeriesIds
                .Select(s => s.ToString())
                .Aggregate((f, s) => f + ", " + s);

            var sql = string.Format(sqlTemplate, commaSep);

            MonthlyReturnDto[] result = null;

            ConnectionExecuteWithLog(
                connection =>
                {
                    result = connection.Query<MonthlyReturnDto>(sql).ToArray();
                },
                sql);

            return result;
        }

        public void DeleteMonthlyReturns(int returnSeriesId)
        {
            const string deleteSqlTemplate = "DELETE FROM MonthlyReturn WHERE ReturnSeriesId = @ReturnSeriesId";

            var paramObject = new { ReturnSeriesId = returnSeriesId };

            ConnectionExecuteWithLog(
                connection =>
                {
                    connection.Execute(deleteSqlTemplate, paramObject);
                },
                deleteSqlTemplate,
                paramObject);
        }

        public void DeleteAllMonthlyReturns()
        {
            const string deleteMonthlyReturnsSql = "DELETE FROM [MonthlyReturn]";

            ConnectionExecuteWithLog(
                connection => connection.Execute(deleteMonthlyReturnsSql),
                deleteMonthlyReturnsSql);
        }
    }
}
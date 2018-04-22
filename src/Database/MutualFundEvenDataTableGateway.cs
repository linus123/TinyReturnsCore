using System.Linq;
using Dapper;
using TinyReturns.Core;
using TinyReturns.Core.MutualFundManagement;

namespace TinyReturns.Database
{
    public class MutualFundEvenDataTableGateway : BaseDatabase, IMutualFundEvenDataTableGateway
    {
        private readonly ITinyReturnsDatabaseSettings _tinyReturnsDatabaseSettings;

        public MutualFundEvenDataTableGateway(
            ITinyReturnsDatabaseSettings tinyReturnsDatabaseSettings,
            ISystemLog systemLog)
            : base(systemLog)
        {
            _tinyReturnsDatabaseSettings = tinyReturnsDatabaseSettings;
        }

        protected override string DefaultConnectionString
        {
            get { return _tinyReturnsDatabaseSettings.ReturnsDatabaseConnectionString; }
        }

        public MutualFundEvenDto[] GetAllForTickerSymbol(
            string tickerSymbol)
        {
            const string sql = @"
SELECT
        [EventId]
        ,[EventType]
        ,[NewValue]
        ,[EffectiveDate]
        ,[DateCreated]
    FROM
        [MutualFund].[Event] AS Event
    WHERE
        ReturnSeriesId = @ReturnSeriesId";

            MutualFundEvenDto[] result = null;

            var paramObject = new { TickerSymbol = tickerSymbol };

            ConnectionExecuteWithLog(
                connection =>
                {
                    result = connection.Query<MutualFundEvenDto>(sql, paramObject).ToArray();
                },
                sql,
                paramObject);

            return result;
        }

        public void DeleteAll()
        {
            const string sql = "DELETE FROM [MutualFund].[Event]";

            ConnectionExecuteWithLog(
                connection => connection.Execute(sql),
                sql);
        }
    }
}
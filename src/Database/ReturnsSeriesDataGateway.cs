using System.Linq;
using Dapper;
using TinyReturns.Core;
using TinyReturns.Core.DataRepositories;

namespace TinyReturns.Database
{
    public class ReturnsSeriesDataGateway : BaseDatabase, IReturnsSeriesDataGateway
    {
        private readonly ITinyReturnsDatabaseSettings _tinyReturnsDatabaseSettings;

        public ReturnsSeriesDataGateway(
            ITinyReturnsDatabaseSettings tinyReturnsDatabaseSettings,
            ISystemLog systemLog) : base(systemLog)
        {
            _tinyReturnsDatabaseSettings = tinyReturnsDatabaseSettings;
        }

        protected override string DefaultConnectionString
        {
            get { return _tinyReturnsDatabaseSettings.ReturnsDatabaseConnectionString; }
        }

        public int InsertReturnSeries(ReturnSeriesDto returnSeries)
        {
            const string sql = @"
INSERT INTO [ReturnSeries]
           ([InvestmentVehicleNumber]
           ,[FeeTypeCode])
     VALUES
           (@InvestmentVehicleNumber
           ,@FeeTypeCode)

SELECT CAST(SCOPE_IDENTITY() as int)
";

            int newId = 0;

            ConnectionExecuteWithLog(
                connection =>
                {
                    newId = connection.Query<int>(sql, returnSeries).Single();
                },
                sql,
                returnSeries);

            return newId;
        }

        public Maybe<ReturnSeriesDto> GetReturnSeries(int returnSeriesId)
        {
            const string sql = @"
SELECT
        [ReturnSeriesId]
        ,[InvestmentVehicleNumber]
        ,[FeeTypeCode]
    FROM
        [ReturnSeries]
    WHERE
        ReturnSeriesId = @ReturnSeriesId";

            ReturnSeriesDto result = null;

            var paramObject = new { ReturnSeriesId = returnSeriesId };

            ConnectionExecuteWithLog(
                connection =>
                {
                    result = connection.Query<ReturnSeriesDto>(sql, paramObject).FirstOrDefault();
                },
                sql);

            if (result == null)
                return Maybe<ReturnSeriesDto>.None;

            return Maybe<ReturnSeriesDto>.Some(result);
        }

        public ReturnSeriesDto[] GetReturnSeries(int[] entityNumbers)
        {
            const string sqlTemplate = @"
SELECT
        [ReturnSeriesId]
        ,[InvestmentVehicleNumber]
        ,[FeeTypeCode]
    FROM
        [ReturnSeries]
    WHERE
        InvestmentVehicleNumber IN ({0})";

            var commaSepNumbers = entityNumbers
                .Select(n => n.ToString())
                .Aggregate((f, s) => f + "," + s);

            var sql = string.Format(sqlTemplate, commaSepNumbers);

            ReturnSeriesDto[] result = null;

            ConnectionExecuteWithLog(
                connection =>
                {
                    result = connection.Query<ReturnSeriesDto>(sql).ToArray();
                },
                sqlTemplate);

            return result;
        }

        public void DeleteAllReturnSeries()
        {
            const string deleteReturnSeriesSql = "DELETE FROM [ReturnSeries]";

            ConnectionExecuteWithLog(
                connection => connection.Execute(deleteReturnSeriesSql),
                deleteReturnSeriesSql);
        }

        public void DeleteReturnSeries(int returnSeriesId)
        {
            const string deleteSqlTemplate = "DELETE FROM ReturnSeries WHERE ReturnSeriesId = @ReturnSeriesId";

            var paramObject = new { ReturnSeriesId = returnSeriesId };

            ConnectionExecuteWithLog(
                connection =>
                {
                    connection.Execute(deleteSqlTemplate, paramObject);
                },
                deleteSqlTemplate,
                paramObject);
        }

    }
}
using System.Linq;
using Dapper;
using TinyReturns.Core;
using TinyReturns.Core.DataRepositories;

namespace TinyReturns.Database
{
    public class InvestmentVehicleDataGateway : BaseDatabase, IInvestmentVehicleDataGateway
    {
        private readonly ITinyReturnsDatabaseSettings _tinyReturnsDatabaseSettings;

        public InvestmentVehicleDataGateway(
            ITinyReturnsDatabaseSettings tinyReturnsDatabaseSettings,
            ISystemLog systemLog) : base(systemLog)
        {
            _tinyReturnsDatabaseSettings = tinyReturnsDatabaseSettings;
        }

        protected override string DefaultConnectionString
        {
            get { return _tinyReturnsDatabaseSettings.ReturnsDatabaseConnectionString; }
        }

        public InvestmentVehicleDto[] GetAllEntities()
        {
            const string sql = @"
SELECT
        [InvestmentVehicleNumber]
        ,[Name]
        ,[InvestmentVehicleTypeCode]
    FROM
        [InvestmentVehicle]";

            InvestmentVehicleDto[] result = null;

            ConnectionExecuteWithLog(
                connection =>
                {
                    result = connection.Query<InvestmentVehicleDto>(sql).ToArray();
                },
                sql);

            return result;
        }

    }
}
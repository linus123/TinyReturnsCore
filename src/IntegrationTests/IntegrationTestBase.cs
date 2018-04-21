using Dimensional.TinyReturns.Database;
using TinyReturns.Core;
using TinyReturns.FileIo;

namespace Dimensional.TinyReturns.IntegrationTests
{
    public class IntegrationTestBase : DatabaseTestBase
    {
        private static bool _hasInit;

        public IntegrationTestBase()
        {
            if (!_hasInit)
            {
                var tinyReturnsDatabaseSettings = new DatabaseSettings();
                var systemLogNoOp = new SystemLogNoOp();

                var monthlyReturnsDataGateway = new TinyReturnsDatabase(tinyReturnsDatabaseSettings, systemLogNoOp);

                MasterFactory.ReturnsSeriesDataGateway = monthlyReturnsDataGateway;
                MasterFactory.MonthlyReturnsDataGateway = monthlyReturnsDataGateway;
                MasterFactory.InvestmentVehicleDataGateway = monthlyReturnsDataGateway;

                MasterFactory.CitiReturnsFileReader = new CitiReturnsFileReader(systemLogNoOp);

                _hasInit = true;
            }
        }
    }
}
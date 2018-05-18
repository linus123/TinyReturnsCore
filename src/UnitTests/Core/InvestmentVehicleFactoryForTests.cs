using System.Linq;
using TinyReturns.Core;
using TinyReturns.Core.DateExtend;

namespace TinyReturns.UnitTests.Core
{
    public class InvestmentVehicleFactoryForTests
    {
        public static InvestmentVehicleFactoryForTests SetupPortfolio(
            int number, string name)
        {
            var portfolio = InvestmentVehicle.CreatePortfolio(number, name);

            return new InvestmentVehicleFactoryForTests(portfolio);
        }

        public static InvestmentVehicleFactoryForTests SetupBenchmark(
            int number, string name)
        {
            var portfolio = InvestmentVehicle.CreateBenchmark(number, name);

            return new InvestmentVehicleFactoryForTests(portfolio);
        }

        private readonly InvestmentVehicle _investmentVehicle;

        public InvestmentVehicleFactoryForTests(
            InvestmentVehicle investmentVehicle)
        {
            _investmentVehicle = investmentVehicle;
        }

        public InvestmentVehicleFactoryForTests AddNetReturn(
            MonthYear monthYear,
            decimal returnValue)
        {
            return AddReturn(monthYear, returnValue, FeeType.NetOfFees);
        }

        public InvestmentVehicleFactoryForTests AddGrossReturn(
            MonthYear monthYear,
            decimal returnValue)
        {
            return AddReturn(monthYear, returnValue, FeeType.GrossOfFees);
        }

        private InvestmentVehicleFactoryForTests AddReturn(
            MonthYear monthYear,
            decimal returnValue,
            FeeType feeType)
        {
            var netSeries = _investmentVehicle.GetAllReturnSeries().FirstOrDefault(s => { return s.FeeType == feeType; });

            if (netSeries == null)
            {
                netSeries = new MonthlyReturnSeries() {FeeType = feeType};
                _investmentVehicle.AddReturnSeries(netSeries);
            }

            netSeries.AddReturn(monthYear, returnValue);

            return this;
        }

        public InvestmentVehicle Create()
        {
            return _investmentVehicle;
        }
    }
}
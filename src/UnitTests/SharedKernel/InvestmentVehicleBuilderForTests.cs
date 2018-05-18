namespace TinyReturns.UnitTests.SharedKernel
{
    public class InvestmentVehicleBuilderForTests
    {
        public static InvestmentVehicleBuilderForTests SetupPortfolio(
            int number, string name)
        {
            var portfolio = InvestmentVehicle.CreatePortfolio(number, name);

            return new InvestmentVehicleBuilderForTests(portfolio);
        }

        public static InvestmentVehicleBuilderForTests SetupBenchmark(
            int number, string name)
        {
            var portfolio = InvestmentVehicle.CreateBenchmark(number, name);

            return new InvestmentVehicleBuilderForTests(portfolio);
        }

        private readonly InvestmentVehicle _investmentVehicle;

        public InvestmentVehicleBuilderForTests(
            InvestmentVehicle investmentVehicle)
        {
            _investmentVehicle = investmentVehicle;
        }

        public InvestmentVehicleBuilderForTests AddNetReturn(
            MonthYear monthYear,
            decimal returnValue)
        {
            return AddReturn(monthYear, returnValue, FeeType.NetOfFees);
        }

        public InvestmentVehicleBuilderForTests AddGrossReturn(
            MonthYear monthYear,
            decimal returnValue)
        {
            return AddReturn(monthYear, returnValue, FeeType.GrossOfFees);
        }

        private InvestmentVehicleBuilderForTests AddReturn(
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
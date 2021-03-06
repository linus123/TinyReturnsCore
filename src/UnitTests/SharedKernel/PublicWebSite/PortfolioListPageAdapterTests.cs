﻿using TinyReturns.SharedKernel.DateExtend;
using TinyReturns.SharedKernel.PublicWebSite;
using Xunit;

namespace TinyReturns.UnitTests.SharedKernel.PublicWebSite
{
    public class PortfolioListPageAdapterTests
    {
        private readonly MonthYear _monthYear;

        public PortfolioListPageAdapterTests()
        {
            _monthYear = new MonthYear(2014, 5);
        }

        [Fact]
        public void AddInvestmentVehicleShouldMapPortfolioProperties()
        {
            var repositoryStub = CreateRepositoryStubWithSinglePortfolio(_monthYear);

            var calculator = new PortfolioListPageFacade(repositoryStub);

            var records = calculator.GetPortfolioPageRecords(_monthYear);

            Assert.Single(records);
            Assert.Equal(100, records[0].PortfolioNumber);
            Assert.Equal("Portfolio100", records[0].PortfolioName);
        }

        [Fact]
        public void AddInvestmentVehicleShouldMapReturnsForSinglePortfolio()
        {
            var repositoryStub = CreateRepositoryStubWithSinglePortfolio(_monthYear);

            var calculator = new PortfolioListPageFacade(repositoryStub);

            var records = calculator.GetPortfolioPageRecords(_monthYear);

            Assert.Single(records);

            AssetValueIsEqual(0.1m, records[0].OneMonth);
            AssetValueIsEqual(0.716m, records[0].ThreeMonth);
            AssetValueIsEqual(2.60360m, records[0].YearToDate);
        }

        [Fact]
        public void AddInvestmentVehicleShouldMapReturnsForSinglePortfolioWithNoReturns()
        {
            var repositoryStub = CreateRepositoryStubWithSinglePortfolioMinusReturns();

            var calculator = new PortfolioListPageFacade(repositoryStub);

            var records = calculator.GetPortfolioPageRecords(_monthYear);

            Assert.Single(records);

            AssetResultHasError(records[0].OneMonth);
            AssetResultHasError(records[0].ThreeMonth);
            AssetResultHasError(records[0].YearToDate);
        }

        private void AssetValueIsEqual(
            decimal expectedValue,
            SerializableReturnResult result)
        {
            Assert.False(result.HasError);
            Assert.Null(result.ErrorMessage);
            Assert.Equal(expectedValue, result.Value);
            Assert.NotNull(result.Calculation);
        }

        private void AssetResultHasError(
            SerializableReturnResult result)
        {
            Assert.True(result.HasError);
            Assert.NotNull(result.ErrorMessage);
            Assert.Null(result.Value);
            Assert.Null(result.Calculation);
        }

        private InvestmentVehicleReturnsRepositoryStub CreateRepositoryStubWithSinglePortfolio(
            MonthYear monthYear)
        {
            var repositoryStub = new InvestmentVehicleReturnsRepositoryStub();

            var portfolio = InvestmentVehicleBuilderForTests
                .SetupPortfolio(100, "Portfolio100")
                .AddNetReturn(monthYear, 0.1m)
                .AddNetReturn(monthYear.AddMonths(-1), 0.2m)
                .AddNetReturn(monthYear.AddMonths(-2), 0.3m)
                .AddNetReturn(monthYear.AddMonths(-3), 0.4m)
                .AddNetReturn(monthYear.AddMonths(-4), 0.5m)
                .Create();

            repositoryStub.AddInvestmentVehicle(portfolio);

            return repositoryStub;
        }

        private InvestmentVehicleReturnsRepositoryStub CreateRepositoryStubWithSinglePortfolioMinusReturns()
        {
            var repositoryStub = new InvestmentVehicleReturnsRepositoryStub();

            var portfolio = InvestmentVehicleBuilderForTests
                .SetupPortfolio(100, "Portfolio100")
                .Create();

            repositoryStub.AddInvestmentVehicle(portfolio);

            return repositoryStub;
        }
    }
}
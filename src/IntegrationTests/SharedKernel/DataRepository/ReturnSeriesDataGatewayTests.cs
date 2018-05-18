using System.Collections.Generic;
using System.Linq;
using TinyReturns.SharedKernel;
using TinyReturns.SharedKernel.DataGateways;
using Xunit;

namespace TinyReturns.IntegrationTests.SharedKernel.DataRepository
{
    public class ReturnSeriesDataGatewayTests
    {
        private readonly IReturnsSeriesDataGateway _returnsSeriesDataGateway;
        private readonly IMonthlyReturnsDataGateway _monthlyReturnsDataGateway;

        public ReturnSeriesDataGatewayTests()
        {
            var serviceLocator = new ServiceLocatorForIntegrationTests();

            _returnsSeriesDataGateway = serviceLocator.GetService<IReturnsSeriesDataGateway>();
            _monthlyReturnsDataGateway = serviceLocator.GetService<IMonthlyReturnsDataGateway>();
        }

        [Fact]
        public void ShouldReadAndWriteReturnSeries()
        {
            var newReturnsSeries = InsertTestReturnSeries();

            var savedReturnSeries = _returnsSeriesDataGateway.GetReturnSeries(newReturnsSeries.ReturnSeriesId);

            AssertReturnSeriesRecordIsValid(savedReturnSeries, newReturnsSeries);

            _returnsSeriesDataGateway.DeleteReturnSeries(newReturnsSeries.ReturnSeriesId);
        }

        [Fact]
        public void ShouldReadAndWriteMonthlyReturns()
        {
            var newReturnsSeries = InsertTestReturnSeries();

            var testMonthlyReturns = CreateTestMonthlyReturns(newReturnsSeries);

            _monthlyReturnsDataGateway.InsertMonthlyReturns(testMonthlyReturns);

            var savedMonthlyReturns = _monthlyReturnsDataGateway.GetMonthlyReturns(newReturnsSeries.ReturnSeriesId);

            AssertMonthlyReturnsAreValid(savedMonthlyReturns, newReturnsSeries.ReturnSeriesId);

            _monthlyReturnsDataGateway.DeleteMonthlyReturns(newReturnsSeries.ReturnSeriesId);
            _returnsSeriesDataGateway.DeleteReturnSeries(newReturnsSeries.ReturnSeriesId);
        }

        private void AssertMonthlyReturnsAreValid(
            MonthlyReturnDto[] savedMonthlyReturns,
            int returnSeriesId)
        {
            Assert.Equal(3, savedMonthlyReturns.Length);

            var target = savedMonthlyReturns.FirstOrDefault(r =>
                r.Month == 1 && r.Year == 2000 && r.ReturnSeriesId == returnSeriesId);

            Assert.NotNull(target);

            Assert.Equal(0.1m, target.ReturnValue);
        }

        private void AssertReturnSeriesRecordIsValid(
            Maybe<ReturnSeriesDto> savedReturnSeries,
            ReturnSeriesDto expectedReturnSeries)
        {
            Assert.True(savedReturnSeries.HasValue);

            Assert.Equal(savedReturnSeries.Value.ReturnSeriesId, expectedReturnSeries.ReturnSeriesId);
            Assert.Equal(savedReturnSeries.Value.InvestmentVehicleNumber, expectedReturnSeries.InvestmentVehicleNumber);
            Assert.Equal(savedReturnSeries.Value.FeeTypeCode, expectedReturnSeries.FeeTypeCode);
        }

        private static MonthlyReturnDto[] CreateTestMonthlyReturns(
            ReturnSeriesDto returnSeries)
        {
            var monthlyReturnList = new List<MonthlyReturnDto>();

            monthlyReturnList.Add(new MonthlyReturnDto()
            {
                Year = 2000,
                Month = 1,
                ReturnValue = 0.1m,
                ReturnSeriesId = returnSeries.ReturnSeriesId
            });
            monthlyReturnList.Add(new MonthlyReturnDto()
            {
                Year = 2000,
                Month = 2,
                ReturnValue = 0.2m,
                ReturnSeriesId = returnSeries.ReturnSeriesId
            });
            monthlyReturnList.Add(new MonthlyReturnDto()
            {
                Year = 2000,
                Month = 3,
                ReturnValue = 0.3m,
                ReturnSeriesId = returnSeries.ReturnSeriesId
            });

            var monthlyReturns = monthlyReturnList.ToArray();
            return monthlyReturns;
        }

        private ReturnSeriesDto InsertTestReturnSeries()
        {
            var returnSeries = new ReturnSeriesDto();

            returnSeries.InvestmentVehicleNumber = 100;
            returnSeries.FeeTypeCode = 'N';

            var newId = _returnsSeriesDataGateway.InsertReturnSeries(returnSeries);

            returnSeries.ReturnSeriesId = newId;

            return returnSeries;
        }
    }
}

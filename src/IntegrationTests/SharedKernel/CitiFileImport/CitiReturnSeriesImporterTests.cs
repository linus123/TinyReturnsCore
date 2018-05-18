using System;
using System.IO;
using System.Linq;
using TinyReturns.SharedKernel;
using TinyReturns.SharedKernel.CitiFileImport;
using TinyReturns.SharedKernel.DataGateways;
using Xunit;

namespace TinyReturns.IntegrationTests.SharedKernel.CitiFileImport
{
    public class CitiReturnSeriesImporterTests
    {
        public class TestHelper
        {
            private readonly ServiceLocatorForIntegrationTests _serviceLocator;

            private readonly IReturnsSeriesDataGateway _returnsSeriesDataGateway;
            private readonly IMonthlyReturnsDataGateway _monthlyReturnsDataGateway;

            public TestHelper()
            {
                _serviceLocator = new ServiceLocatorForIntegrationTests();

                _returnsSeriesDataGateway = _serviceLocator.GetService<IReturnsSeriesDataGateway>();
                _monthlyReturnsDataGateway = _serviceLocator.GetService<IMonthlyReturnsDataGateway>();
            }

            public ReturnSeriesDto[] GetReturnSeries(
                int[] returnSeriesIds)
            {
                return _returnsSeriesDataGateway.GetReturnSeries(returnSeriesIds);
            }

            public void DeleteTestData(
                Action testAct)
            {
                _monthlyReturnsDataGateway.DeleteAllMonthlyReturns();
                _returnsSeriesDataGateway.DeleteAllReturnSeries();

                testAct();

                _monthlyReturnsDataGateway.DeleteAllMonthlyReturns();
                _returnsSeriesDataGateway.DeleteAllReturnSeries();
            }

            public MonthlyReturnDto[] GetMonthlyReturnSeries(int entityNumber)
            {
                var returnSeries = _returnsSeriesDataGateway.GetReturnSeries(new[] { entityNumber }).First();
                var monthlyReturns = _monthlyReturnsDataGateway.GetMonthlyReturns(returnSeries.ReturnSeriesId);
                return monthlyReturns;
            }

            public CitiFileImportInteractor CreateInteractor()
            {
                return _serviceLocator.GetService<CitiFileImportInteractor>();
            }
        }

        private readonly IReturnsSeriesDataGateway _returnsSeriesDataGateway;
        private readonly IMonthlyReturnsDataGateway _monthlyReturnsDataGateway;

        public CitiReturnSeriesImporterTests()
        {
            var serviceLocator = new ServiceLocatorForIntegrationTests();

            _returnsSeriesDataGateway = serviceLocator.GetService<IReturnsSeriesDataGateway>();
            _monthlyReturnsDataGateway = serviceLocator.GetService<IMonthlyReturnsDataGateway>();
        }

        [Fact]
        public void ImportMonthlyReturnsFileShouldInsertCorrectNumberOfSeriesWhenGivenValidFile()
        {
            var testHelper = new TestHelper();

            testHelper.DeleteTestData(() =>
            {
                var importer = testHelper.CreateInteractor();

                importer.ImportFiles(new CitiFileImportRequestModel(new[] { GetNetReturnsTestFilePath() }));

                var series = testHelper.GetReturnSeries(
                    new[] { 100, 101, 102 });

                Assert.Equal(3, series.Length);
            });
        }

        [Fact]
        public void ImportMonthlyReturnsFileShouldInsertCorrectSeriesWhenGivenValidNetOfFeesFile()
        {
            var testHelper = new TestHelper();

            testHelper.DeleteTestData(() =>
            {
                var importer = testHelper.CreateInteractor();

                importer.ImportFiles(new CitiFileImportRequestModel(new[] { GetNetReturnsTestFilePath() }));

                var series = testHelper.GetReturnSeries(
                    new[] { 100 });

                Assert.Equal(100, series[0].InvestmentVehicleNumber);
                Assert.Equal(series[0].FeeTypeCode, FeeType.NetOfFees.Code);
            });
        }

        [Fact]
        public void ImportMonthlyReturnsFileShouldInsertCorrectNumberOfMonthlyReturns()
        {
            var testHelper = new TestHelper();

            testHelper.DeleteTestData(() =>
            {
                var importer = testHelper.CreateInteractor();

                importer.ImportFiles(new CitiFileImportRequestModel(new[] { GetNetReturnsTestFilePath() }));

                var monthlyReturns = testHelper.GetMonthlyReturnSeries(100);

                Assert.Equal(9, monthlyReturns.Length);
            });
        }

        [Fact]
        public void ImportMonthlyReturnsFileShouldCorrectlyMapMonthlyReturns()
        {
            var testHelper = new TestHelper();

            testHelper.DeleteTestData(() =>
            {
                var importer = testHelper.CreateInteractor();

                importer.ImportFiles(new CitiFileImportRequestModel(new[] { GetNetReturnsTestFilePath() }));

                var monthlyReturns = testHelper.GetMonthlyReturnSeries(100);

                var target = monthlyReturns.FirstOrDefault(r => r.Month == 10 && r.Year == 2013);

                Assert.NotNull(target);
                Assert.Equal(0.0440055m, target.ReturnValue);

            });
        }

        private string GetNetReturnsTestFilePath()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            var targetFile = currentDirectory
                 + @"\SharedKernel\TestNetReturnsForEntity100_101_102.csv";

            return targetFile;
        }
    }
}
﻿using System.IO;
using System.Linq;
using TinyReturns.Core;
using TinyReturns.Core.CitiFileImport;
using TinyReturns.Core.DataRepositories;
using Xunit;

namespace TinyReturns.IntegrationTests.Core.CitiFileImport
{
    public class CitiReturnSeriesImporterTests : IntegrationTestBase
    {
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
            DeleteTestData();
            
            ImportTestFile(GetNetReturnsTestFilePath());

            var series = _returnsSeriesDataGateway.GetReturnSeries(
                new[] {100, 101, 102});

            Assert.Equal(series.Length, 3);

            DeleteTestData();
        }

        [Fact]
        public void ImportMonthlyReturnsFileShouldInsertCorrectSeriesWhenGivenValidNetOfFeesFile()
        {
            DeleteTestData();

            ImportTestFile(GetNetReturnsTestFilePath());

            var series = _returnsSeriesDataGateway.GetReturnSeries(
                new[] { 100 });

            Assert.Equal(series[0].InvestmentVehicleNumber, 100);
            Assert.Equal(series[0].FeeTypeCode, FeeType.NetOfFees.Code);

            DeleteTestData();
        }

        [Fact]
        public void ImportMonthlyReturnsFileShouldInsertCorrectNumberOfMonthlyReturns()
        {
            DeleteTestData();

            ImportTestFile(GetNetReturnsTestFilePath());

            var monthlyReturns = GetMonthlyReturnSeries(100);

            Assert.Equal(9, monthlyReturns.Length);

            DeleteTestData();
        }

        [Fact]
        public void ImportMonthlyReturnsFileShouldCorrectlyMapMonthlyReturns()
        {
            DeleteTestData();

            ImportTestFile(GetNetReturnsTestFilePath());

            var monthlyReturns = GetMonthlyReturnSeries(100);

            var target = monthlyReturns.FirstOrDefault(r => r.Month == 10 && r.Year == 2013);

            Assert.NotNull(target);
            Assert.Equal(0.0440055m, target.ReturnValue);

            DeleteTestData();
        }

        private MonthlyReturnDto[] GetMonthlyReturnSeries(int entityNumber)
        {
            var returnSeries = _returnsSeriesDataGateway.GetReturnSeries(new[] {entityNumber}).First();
            var monthlyReturns = _monthlyReturnsDataGateway.GetMonthlyReturns(returnSeries.ReturnSeriesId);
            return monthlyReturns;
        }

        private void ImportTestFile(string filePath)
        {
            var serviceLocator = new ServiceLocatorForIntegrationTests();
            var importer = serviceLocator.GetService<CitiReturnSeriesImporter>();

            importer.ImportMonthlyReturnsFile(filePath);
        }

        private void DeleteTestData()
        {
            var serviceLocator = new ServiceLocatorForIntegrationTests();
            var importer = serviceLocator.GetService<CitiReturnSeriesImporter>();

            importer.DeleteAllReturns();
        }

        private string GetNetReturnsTestFilePath()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            var targetFile = currentDirectory
                 + @"\Core\TestNetReturnsForEntity100_101_102.csv";

            return targetFile;
        }
    }
}
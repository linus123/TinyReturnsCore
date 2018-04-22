﻿using System.Linq;
using TinyReturns.Core.DataRepositories;
using Xunit;

namespace TinyReturns.IntegrationTests.Core.DataRepository
{
    public class InvestmentVehicleDataRepositoryTests : DatabaseTestBase
    {
        [Fact]
        public void GetAllEntitiesShouldReturnCorrectNumberOfEntities()
        {
            var serviceLocator = new ServiceLocatorForIntegrationTests();
            var entityDataRepository = serviceLocator.GetService<IInvestmentVehicleDataGateway>();

            var results = entityDataRepository.GetAllEntities();

            Assert.Equal(5, results.Length);
        }

        [Fact]
        public void GetAllEntitiesShouldReturnMapAllColumnsToProperties()
        {
            var serviceLocator = new ServiceLocatorForIntegrationTests();
            var entityDataRepository = serviceLocator.GetService<IInvestmentVehicleDataGateway>();

            var results = entityDataRepository.GetAllEntities();

            var target = results.FirstOrDefault(r => r.InvestmentVehicleNumber == 100);

            Assert.NotNull(target);

            Assert.Equal('P', target.InvestmentVehicleTypeCode);
            Assert.Equal("Portfolio 100 - Large", target.Name);
        }
    }
}
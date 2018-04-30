using System;
using TinyReturns.Core;
using TinyReturns.Core.MutualFundManagement;
using Xunit;

namespace TinyReturns.IntegrationTests.Core.MutualFundManagement
{
    public class MutualFundManagementTests
    {
        [Fact(DisplayName = "Repository should not find a mutual fund when ticker symbol not found.")]
        public void Test0010()
        {
            var serviceLocator = new ServiceLocatorForIntegrationTests();

            var mutualFundEvenDataTableGateway = serviceLocator.GetService<IMutualFundEvenDataTableGateway>();

            mutualFundEvenDataTableGateway.DeleteAll();

            var mutualFundRepository = new MutualFundRepository(
                mutualFundEvenDataTableGateway);

            var mutualFundResult = mutualFundRepository.GetByTickerSymbol("ABC");

            Assert.False(mutualFundResult.HasValue);
            Assert.True(mutualFundResult.HasNoValue());

            mutualFundEvenDataTableGateway.DeleteAll();
        }

        [Fact(DisplayName = "We should be able to create mutual fund.")]
        public void Test0020()
        {
            var serviceLocator = new ServiceLocatorForIntegrationTests();

            var mutualFundEvenDataTableGateway = serviceLocator.GetService<IMutualFundEvenDataTableGateway>();

            mutualFundEvenDataTableGateway.DeleteAll();

            var tickerSymbol = "ABC";

            var mutualFundEvenDto = new MutualFundEvenDto()
            {
                TickerSymbol = tickerSymbol,
                EventType = "Create",
                JsonPayload = string.Empty,
                EffectiveDate = new DateTime(2010, 1, 1),
                DateCreated = new DateTime(2012, 1, 1)
            };

            mutualFundEvenDataTableGateway.Insert(new []{ mutualFundEvenDto });

            var mutualFundRepository = new MutualFundRepository(
                mutualFundEvenDataTableGateway);

            var mutualFundResult = mutualFundRepository.GetByTickerSymbol(tickerSymbol);

            Assert.True(mutualFundResult.HasValue);
            Assert.False(mutualFundResult.HasNoValue());
            Assert.Equal(tickerSymbol, mutualFundResult.Value.TickerSymbol);

            mutualFundEvenDataTableGateway.DeleteAll();
        }

        [Fact(DisplayName = "We should be able to create mutual fund and set name.")]
        public void Test0030()
        {
            var serviceLocator = new ServiceLocatorForIntegrationTests();

            var mutualFundEvenDataTableGateway = serviceLocator.GetService<IMutualFundEvenDataTableGateway>();

            mutualFundEvenDataTableGateway.DeleteAll();

            var tickerSymbol = "ABC";

            var mutualFundEvenDto1 = new MutualFundEvenDto()
            {
                TickerSymbol = tickerSymbol,
                EventType = "Create",
                JsonPayload = tickerSymbol,
                EffectiveDate = new DateTime(2010, 1, 1),
                DateCreated = new DateTime(2012, 1, 1)
            };

            var mutualFundEvenDto2 = new MutualFundEvenDto()
            {
                TickerSymbol = tickerSymbol,
                EventType = "NameChange",
                JsonPayload = "My New Fund",
                EffectiveDate = new DateTime(2010, 1, 2),
                DateCreated = new DateTime(2012, 1, 1)
            };

            mutualFundEvenDataTableGateway.Insert(new[] { mutualFundEvenDto1, mutualFundEvenDto2,  });

            var mutualFundRepository = new MutualFundRepository(
                mutualFundEvenDataTableGateway);

            var mutualFundResult = mutualFundRepository.GetByTickerSymbol(tickerSymbol);

            Assert.True(mutualFundResult.HasValue);
            Assert.False(mutualFundResult.HasNoValue());
            Assert.Equal(tickerSymbol, mutualFundResult.Value.TickerSymbol);
            Assert.Equal("My New Fund", mutualFundResult.Value.Name);

            mutualFundEvenDataTableGateway.DeleteAll();
        }

    }
}

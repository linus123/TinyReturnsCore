using System;
using TinyReturns.Core;
using TinyReturns.Core.MutualFundManagement;
using Xunit;

namespace TinyReturns.IntegrationTests.Core
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

            var mutualFundCreateEvent = new MutualFundCreateEvent(
                new DateTime(2010, 1, 1),
                tickerSymbol);

            var mutualFundEventProcessor = new MutualFundEventProcessor();

            mutualFundEventProcessor.Process(mutualFundCreateEvent);

            var mutualFundDomainEventRepository = new MutualFundDomainEventRepository(
                mutualFundEvenDataTableGateway,
                new ClockStub(new DateTime(2010, 1, 1)));

            mutualFundDomainEventRepository.SaveEvents(mutualFundEventProcessor.GetDomainEvents());

            var mutualFundRepository = new MutualFundRepository(
                mutualFundEvenDataTableGateway);

            var mutualFundResult = mutualFundRepository.GetByTickerSymbol(tickerSymbol);

            Assert.True(mutualFundResult.HasValue);
            Assert.False(mutualFundResult.HasNoValue());
            Assert.Equal(tickerSymbol, mutualFundResult.Value.TickerSymbol);

            mutualFundEvenDataTableGateway.DeleteAll();
        }

    }
}

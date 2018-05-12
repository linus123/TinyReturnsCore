using System;
using TinyReturns.Core.MutualFundManagement;
using Xunit;

namespace TinyReturns.IntegrationTests.Core.MutualFundManagement
{
    public class MutualFundManagementTests
    {
        public class TestHelper
        {
            private readonly IMutualFundEvenDataTableGateway _mutualFundEvenDataTableGateway;
            private MutualFundDomainEventRepository _mutualFundDomainEventRepository;

            public TestHelper()
            {
                var serviceLocator = new ServiceLocatorForIntegrationTests();

                _mutualFundEvenDataTableGateway = serviceLocator.GetService<IMutualFundEvenDataTableGateway>();

                _mutualFundDomainEventRepository = new MutualFundDomainEventRepository(
                    _mutualFundEvenDataTableGateway,
                    new ClockStub(new DateTime(2010, 1, 1)));
            }

            public void DeleteData(
                Action act)
            {
                _mutualFundEvenDataTableGateway.DeleteAll();
                act();
                _mutualFundEvenDataTableGateway.DeleteAll();
            }

            public void InsertMutualFundEvenDto(
                MutualFundEvenDto dto)
            {
                _mutualFundEvenDataTableGateway.Insert(new []{ dto });
            }

            public void InsertMutualFundDomainEvent(
                IMutualFundDomainEvent e)
            {
                _mutualFundDomainEventRepository.SaveEvents(new []{ e });
            }

            public MutualFundForReadRepository CreateRepository()
            {
                return new MutualFundForReadRepository(
                    _mutualFundEvenDataTableGateway);
            }
        }

        [Fact(DisplayName = "Repository should not find a mutual fund when ticker symbol not found.")]
        public void Test0010()
        {
            var testHelper = new TestHelper();

            testHelper.DeleteData(() =>
            {
                var mutualFundRepository = testHelper.CreateRepository();

                var mutualFundResult = mutualFundRepository.GetByTickerSymbol("ABC");

                Assert.False(mutualFundResult.HasValue);
                Assert.True(mutualFundResult.DoesNotHaveValue);
            });
        }

        [Fact(DisplayName = "We should be able to create mutual fund.")]
        public void Test0020()
        {
            var testHelper = new TestHelper();

            testHelper.DeleteData(() =>
            {
                var tickerSymbol = "ABC";
                var fundName = "My Mutual Fund";
                var currencyCode = "USD";

                var mutualFundCreateEvent = new MutualFundCreateEvent(
                    new DateTime(2010, 1, 1),
                    tickerSymbol,
                    fundName,
                    new CurrencyCode(currencyCode));

                testHelper.InsertMutualFundDomainEvent(mutualFundCreateEvent);

                var mutualFundRepository = testHelper.CreateRepository();

                var mutualFundResult = mutualFundRepository.GetByTickerSymbol(tickerSymbol);

                Assert.True(mutualFundResult.HasValue);
                Assert.False(mutualFundResult.DoesNotHaveValue);
                Assert.Equal(tickerSymbol, mutualFundResult.Value.TickerSymbol);
                Assert.Equal("My Mutual Fund", mutualFundResult.Value.Name);
                Assert.Equal("USD", mutualFundResult.Value.CurrencyCodeAsString);

            });
        }

        [Fact(DisplayName = "We should be able to create mutual fund and change name.")]
        public void Test0030()
        {
            var testHelper = new TestHelper();

            testHelper.DeleteData(() =>
            {
                var tickerSymbol = "ABC";
                var fundName = "Original Fund Name";
                var currencyCode = "USD";

                var mutualFundEvenDto1 = new MutualFundEvenDto()
                {
                    TickerSymbol = tickerSymbol,
                    EventType = MutualFundCreateEvent.EventType,
                    JsonPayload = MutualFundCreateEvent.CreateJsonPayload(fundName, currencyCode),
                    Priority = MutualFundCreateEvent.PriorityConts,
                    EffectiveDate = new DateTime(2010, 1, 1),
                    DateCreated = new DateTime(2012, 1, 1)
                };

                testHelper.InsertMutualFundEvenDto(mutualFundEvenDto1);

                var mutualFundEvenDto2 = new MutualFundEvenDto()
                {
                    TickerSymbol = tickerSymbol,
                    EventType = MutualFundNameChangeEvent.EventType,
                    JsonPayload = MutualFundNameChangeEvent.CreateJsonPayload("My New Fund"),
                    Priority = MutualFundNameChangeEvent.PriorityConts,
                    EffectiveDate = new DateTime(2010, 1, 2),
                    DateCreated = new DateTime(2012, 1, 1)
                };

                testHelper.InsertMutualFundEvenDto(mutualFundEvenDto2);

                var mutualFundRepository = testHelper.CreateRepository();

                var mutualFundResult = mutualFundRepository.GetByTickerSymbol(tickerSymbol);

                Assert.True(mutualFundResult.HasValue);
                Assert.False(mutualFundResult.DoesNotHaveValue);
                Assert.Equal(tickerSymbol, mutualFundResult.Value.TickerSymbol);
                Assert.Equal("My New Fund", mutualFundResult.Value.Name);
            });
        }

        [Fact(DisplayName = "We should be able to create mutual fund and change currency code.")]
        public void Test0040()
        {
            var testHelper = new TestHelper();

            testHelper.DeleteData(() =>
            {
                var tickerSymbol = "ABC";
                var fundName = "Original Fund Name";
                var currencyCode = "USD";

                var mutualFundEvenDto1 = new MutualFundEvenDto()
                {
                    TickerSymbol = tickerSymbol,
                    EventType = MutualFundCreateEvent.EventType,
                    JsonPayload = MutualFundCreateEvent.CreateJsonPayload(fundName, currencyCode),
                    Priority = MutualFundCreateEvent.PriorityConts,
                    EffectiveDate = new DateTime(2010, 1, 1),
                    DateCreated = new DateTime(2012, 1, 1)
                };

                testHelper.InsertMutualFundEvenDto(mutualFundEvenDto1);

                var mutualFundEvenDto2 = new MutualFundEvenDto()
                {
                    TickerSymbol = tickerSymbol,
                    EventType = MutualFundCurrencyChangeEvent.EventType,
                    JsonPayload = MutualFundCurrencyChangeEvent.CreateJsonPayload("AUD"),
                    Priority = MutualFundCurrencyChangeEvent.PriorityConts,
                    EffectiveDate = new DateTime(2010, 1, 2),
                    DateCreated = new DateTime(2012, 1, 1)
                };

                testHelper.InsertMutualFundEvenDto(mutualFundEvenDto2);

                var mutualFundRepository = testHelper.CreateRepository();

                var mutualFundResult = mutualFundRepository.GetByTickerSymbol(tickerSymbol);

                Assert.True(mutualFundResult.HasValue);
                Assert.False(mutualFundResult.DoesNotHaveValue);
                Assert.Equal(tickerSymbol, mutualFundResult.Value.TickerSymbol);
                Assert.Equal("AUD", mutualFundResult.Value.CurrencyCodeAsString);
            });
        }

        [Fact(DisplayName = "A create event should be process before any other event.")]
        public void Test0050()
        {
            var testHelper = new TestHelper();

            testHelper.DeleteData(() =>
            {
                var tickerSymbol = "ABC";
                var fundName = "Original Fund Name";
                var currencyCode = "USD";

                var mutualFundEvenDto1 = new MutualFundEvenDto()
                {
                    TickerSymbol = tickerSymbol,
                    EventType = MutualFundNameChangeEvent.EventType,
                    JsonPayload = MutualFundNameChangeEvent.CreateJsonPayload("My New Fund"),
                    Priority = MutualFundNameChangeEvent.PriorityConts,
                    EffectiveDate = new DateTime(2010, 1, 1),
                    DateCreated = new DateTime(2012, 1, 1)
                };

                testHelper.InsertMutualFundEvenDto(mutualFundEvenDto1);

                var mutualFundEvenDto2 = new MutualFundEvenDto()
                {
                    TickerSymbol = tickerSymbol,
                    EventType = MutualFundCreateEvent.EventType,
                    JsonPayload = MutualFundCreateEvent.CreateJsonPayload(fundName, currencyCode),
                    Priority = MutualFundCreateEvent.PriorityConts,
                    EffectiveDate = new DateTime(2010, 1, 1),
                    DateCreated = new DateTime(2012, 1, 2)
                };

                testHelper.InsertMutualFundEvenDto(mutualFundEvenDto2);

                var mutualFundRepository = testHelper.CreateRepository();

                var mutualFundResult = mutualFundRepository.GetByTickerSymbol(tickerSymbol);

                Assert.True(mutualFundResult.HasValue);
                Assert.False(mutualFundResult.DoesNotHaveValue);
                Assert.Equal(tickerSymbol, mutualFundResult.Value.TickerSymbol);
                Assert.Equal("My New Fund", mutualFundResult.Value.Name);

            });
        }

    }
}

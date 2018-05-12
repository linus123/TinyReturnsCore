using System;
using Newtonsoft.Json.Linq;
using TinyReturns.Core.MutualFundManagement;
using Xunit;

namespace TinyReturns.IntegrationTests.Core.MutualFundManagement
{
    public class MutualFundManagementTests
    {
        public class TestHelper
        {
            private readonly IMutualFundEvenDataTableGateway _mutualFundEvenDataTableGateway;

            public TestHelper()
            {
                var serviceLocator = new ServiceLocatorForIntegrationTests();

                _mutualFundEvenDataTableGateway = serviceLocator.GetService<IMutualFundEvenDataTableGateway>();
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

            public MutualFundRepository CreateRepository()
            {
                return new MutualFundRepository(
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
                var stringCurrency = "USD";

                var jsonPayLoad = GetCreateJsonPayLoad(
                    fundName,
                    stringCurrency);

                var mutualFundEvenDto = new MutualFundEvenDto()
                {
                    TickerSymbol = tickerSymbol,
                    EventType = "Create",
                    JsonPayload = jsonPayLoad,
                    EffectiveDate = new DateTime(2010, 1, 1),
                    DateCreated = new DateTime(2012, 1, 1)
                };

                testHelper.InsertMutualFundEvenDto(mutualFundEvenDto);

                var mutualFundRepository = testHelper.CreateRepository();

                var mutualFundResult = mutualFundRepository.GetByTickerSymbol(tickerSymbol);

                Assert.True(mutualFundResult.HasValue);
                Assert.False(mutualFundResult.DoesNotHaveValue);
                Assert.Equal(tickerSymbol, mutualFundResult.Value.TickerSymbol);
                Assert.Equal("My Mutual Fund", mutualFundResult.Value.Name);
                Assert.Equal("USD", mutualFundResult.Value.CurrencyCodeAsString);

            });
        }

        [Fact(DisplayName = "We should be able to create mutual fund and set name.")]
        public void Test0030()
        {
            var testHelper = new TestHelper();

            testHelper.DeleteData(() =>
            {
                var tickerSymbol = "ABC";
                var fundName = "Original Fund Name";
                var stringCurrency = "USD";

                var jsonPayLoad = GetCreateJsonPayLoad(
                    fundName,
                    stringCurrency);

                var mutualFundEvenDto1 = new MutualFundEvenDto()
                {
                    TickerSymbol = tickerSymbol,
                    EventType = "Create",
                    JsonPayload = jsonPayLoad,
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

                testHelper.InsertMutualFundEvenDto(mutualFundEvenDto1);
                testHelper.InsertMutualFundEvenDto(mutualFundEvenDto2);

                var mutualFundRepository = testHelper.CreateRepository();

                var mutualFundResult = mutualFundRepository.GetByTickerSymbol(tickerSymbol);

                Assert.True(mutualFundResult.HasValue);
                Assert.False(mutualFundResult.DoesNotHaveValue);
                Assert.Equal(tickerSymbol, mutualFundResult.Value.TickerSymbol);
                Assert.Equal("My New Fund", mutualFundResult.Value.Name);

            });
        }

        private static string GetCreateJsonPayLoad(
            string fundName,
            string stringCurrency)
        {
            var jObject = new JObject();

            jObject.Add("name", fundName);
            jObject.Add("currencyCode", stringCurrency);

            var jsonPayLoad = jObject.ToString();
            return jsonPayLoad;
        }
    }
}

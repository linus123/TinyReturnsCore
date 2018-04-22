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

            var mutualFundRepository = new MutualFundRepository();

            var mutualFundResult = mutualFundRepository.GetByTickerSymbol("ABC");

            Assert.False(mutualFundResult.HasValue);
            Assert.True(mutualFundResult.HasNoValue());
        }

    }
}

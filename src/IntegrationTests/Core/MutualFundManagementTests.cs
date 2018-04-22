using TinyReturns.Core.MutualFundManagement;
using Xunit;

namespace TinyReturns.IntegrationTests.Core
{
    public class MutualFundManagementTests
    {
        [Fact]
        public void ShouldWork()
        {
            var serviceLocator = new ServiceLocatorForIntegrationTests();

            serviceLocator.GetService<IMutualFundEvenDataTableGateway>();
        }

    }
}
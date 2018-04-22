using System;
using TinyReturns.Core.MutualFundManagement;
using Xunit;

namespace TinyReturns.UnitTests.Core.MutualFundManagement
{
    public class MutualFundManagementTests
    {
        [Fact]
        public void ShouldWork()
        {
            var eventProcessor = new EventProcessor();

            var mutualFund = new MutualFund("ABC");

            var nameChangeEvent = new NameChangeEvent(
                new DateTime(2010, 1, 1),
                "My Super Fund",
                mutualFund);

            eventProcessor.Process(nameChangeEvent);

            Assert.Equal("My Super Fund", mutualFund.Name);
        }

    }
}
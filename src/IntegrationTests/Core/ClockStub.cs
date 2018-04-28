using System;
using TinyReturns.Core;

namespace TinyReturns.IntegrationTests.Core
{
    public class ClockStub : IClock
    {
        private readonly DateTime _currentDateTime;

        public ClockStub(
            DateTime currentDateTime)
        {
            _currentDateTime = currentDateTime;
        }

        public DateTime GetCurrentDateTime()
        {
            return _currentDateTime;
        }
    }
}
using System;
using TinyReturns.SharedKernel;

namespace TinyReturns.IntegrationTests.SharedKernel
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
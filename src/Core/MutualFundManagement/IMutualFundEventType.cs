using System;

namespace TinyReturns.Core.MutualFundManagement
{
    public interface IMutualFundEventType
    {
        string EventName { get; }
        int Priority { get; }

        IMutualFundDomainEvent CreateEvent(
            DateTime effectiveDate,
            string tickerSymbol,
            string jsonPayload,
            MutualFund mutualFund);
    }
}
using System;

namespace TinyReturns.Core.MutualFundManagement
{
    public interface IMutualFundEventType
    {
        string EventName { get; }
        int Priority { get; }

        IMutualFundDomainEvent CreateEventFromJson(
            DateTime effectiveDate,
            string tickerSymbol,
            string jsonPayload,
            MutualFund mutualFund);
    }
}
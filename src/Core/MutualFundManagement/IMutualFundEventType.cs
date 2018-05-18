using System;

namespace TinyReturns.SharedKernel.MutualFundManagement
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
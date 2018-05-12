using System;

namespace TinyReturns.Core.MutualFundManagement
{
    public interface IMutualFundDomainEvent
    {
        DateTime EffectiveDate { get; }
        string TickerSymbol { get; }
        MutualFund Process();
        string EventType { get; }
        int Priority { get; }
        string GetJsonPayload();
    }
}
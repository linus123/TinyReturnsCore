using System;

namespace TinyReturns.Core.MutualFundManagement
{
    public interface IMutualFundDomainEvent
    {
        DateTime EffectiveDate { get; }
        string TickerSymbol { get; }
        MutualFund Process();
        string GetEventType();
    }
}
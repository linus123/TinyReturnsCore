using System;

namespace TinyReturns.Core.MutualFundManagement
{
    public abstract class MutualFundDomainEvent
    {

        protected MutualFundDomainEvent(
            DateTime effectiveDate,
            string tickerSymbol)
        {
            TickerSymbol = tickerSymbol;
            EffectiveDate = effectiveDate;
        }

        public string TickerSymbol { get; }
        public DateTime EffectiveDate { get; }

        public abstract void Process();
        public abstract string GetEventType();
    }
}
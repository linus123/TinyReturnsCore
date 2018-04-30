using System;

namespace TinyReturns.Core.MutualFundManagement
{
    public abstract class MutualFundDomainEvent
    {
        protected MutualFundDomainEvent(
            DateTime effectiveDate,
            MutualFund mutualFund)
        {
            MutualFund = mutualFund;
            EffectiveDate = effectiveDate;
        }

        protected MutualFund MutualFund { get; }
        public DateTime EffectiveDate { get; }

        public string TickerSymbol
        {
            get { return MutualFund.TickerSymbol; }
        }

        public abstract void Process();
        public abstract string GetEventType();
    }
}
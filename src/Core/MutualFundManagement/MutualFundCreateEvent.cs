using System;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundCreateEvent : DomainEvent
    {
        private readonly DateTime _effectiveDate;
        private string _tickerSymbol;

        public MutualFundCreateEvent(
            DateTime effectiveDate,
            string tickerSymbol) : base(effectiveDate)
        {
            _tickerSymbol = tickerSymbol;
            _effectiveDate = effectiveDate;
        }

        public override void Process()
        {
        }
    }
}
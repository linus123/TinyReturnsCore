using System;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundCreateEvent : MutualFundDomainEvent
    {
        public MutualFundCreateEvent(
            DateTime effectiveDate,
            string tickerSymbol) : base(effectiveDate, tickerSymbol)
        {
        }

        public override void Process()
        {
        }

        public override string GetEventType()
        {
            return "Create";
        }
    }
}
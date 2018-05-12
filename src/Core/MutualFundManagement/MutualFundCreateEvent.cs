using System;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundCreateEvent : IMutualFundDomainEvent
    {
        private readonly string _fundName;
        private CurrencyCode _currencyCode;

        public const string EventType = "Create";

        public MutualFundCreateEvent(
            DateTime effectiveDate,
            string tickerSymbol,
            string fundName,
            CurrencyCode currencyCode)
        {
            _currencyCode = currencyCode;
            EffectiveDate = effectiveDate;
            TickerSymbol = tickerSymbol;
            _fundName = fundName;
        }

        public DateTime EffectiveDate { get; }

        public string TickerSymbol { get; }

        public MutualFund Process()
        {
            return new MutualFund(
                TickerSymbol,
                _fundName,
                _currencyCode);
        }

        public string GetEventType()
        {
            return EventType;
        }
    }
}
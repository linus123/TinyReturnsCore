using System;
using Newtonsoft.Json.Linq;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundCreateEvent : IMutualFundDomainEvent
    {
        private readonly string _fundName;
        private readonly CurrencyCode _currencyCode;

        public const string EventType = "Create";
        public const int PriorityConts = 100;

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

        public int Priority
        {
            get { return PriorityConts; }
        }

        public string GetJsonPayload()
        {
            var jObject = new JObject();

            jObject.Add("name", _fundName);
            jObject.Add("currencyCode", _currencyCode.Code);

            return jObject.ToString();
        }
    }
}
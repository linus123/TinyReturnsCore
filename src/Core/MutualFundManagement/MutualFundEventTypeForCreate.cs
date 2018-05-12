using System;
using Newtonsoft.Json.Linq;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundEventTypeForCreate : IMutualFundEventType
    {
        public string EventName => "Create";
        public int Priority => 100;

        public IMutualFundDomainEvent CreateEventFromJson(
            DateTime effectiveDate,
            string tickerSymbol,
            string json,
            MutualFund mutualFund)
        {
            var jObject = JObject.Parse(json);

            var nameToken = jObject.SelectToken("name");
            var currencyCodeToken = jObject.SelectToken("currencyCode");

            var currencyCode = new CurrencyCode(currencyCodeToken.ToString());

            return new DomainEvent(
                this,
                effectiveDate,
                tickerSymbol,
                nameToken.ToString(),
                currencyCode);
        }

        public IMutualFundDomainEvent CreateEvent(
            DateTime effectiveDate,
            string tickerSymbol,
            string name,
            CurrencyCode currencyCode)
        {
            return new DomainEvent(
                this,
                effectiveDate,
                tickerSymbol,
                name,
                currencyCode);
        }

        public class DomainEvent : IMutualFundDomainEvent
        {
            private readonly string _fundName;
            private readonly CurrencyCode _currencyCode;

            private readonly MutualFundEventTypeForCreate _eventType;

            public DomainEvent(
                MutualFundEventTypeForCreate eventType,
                DateTime effectiveDate,
                string tickerSymbol,
                string fundName,
                CurrencyCode currencyCode)
            {
                _eventType = eventType;
                _currencyCode = currencyCode;
                EffectiveDate = effectiveDate;
                TickerSymbol = tickerSymbol;
                _fundName = fundName;
            }

            public DateTime EffectiveDate { get; }
            public string TickerSymbol { get; }

            public string EventType => _eventType.EventName;
            public int Priority => _eventType.Priority;

            public MutualFund Process()
            {
                return new MutualFund(
                    TickerSymbol,
                    _fundName,
                    _currencyCode);
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
}
using System;
using Newtonsoft.Json.Linq;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundEventTypeForCurrencyChange : IMutualFundEventType
    {
        public string EventName => "CurrencyChange";
        public int Priority => 500;

        public IMutualFundDomainEvent CreateEventFromJson(
            DateTime effectiveDate,
            string tickerSymbol,
            string json,
            MutualFund mutualFund)
        {
            var jObject = JObject.Parse(json);

            var currencyCodeToken = jObject.SelectToken("currencyCode");

            var currencyCode = new CurrencyCode(currencyCodeToken.ToString());

            return new DomainEvent(
                this,
                effectiveDate,
                currencyCode,
                mutualFund);
        }

        public IMutualFundDomainEvent CreateEvent(
            DateTime effectiveDate,
            CurrencyCode currencyCode,
            MutualFund mutualFund)
        {
            return new DomainEvent(
                this,
                effectiveDate,
                currencyCode,
                mutualFund);
        }
        
        public class DomainEvent : IMutualFundDomainEvent
        {
            private readonly MutualFund _mutualFund;
            private readonly CurrencyCode _currencyCode;
            private readonly MutualFundEventTypeForCurrencyChange _eventType;

            public DomainEvent(
                MutualFundEventTypeForCurrencyChange eventType,
                DateTime effectiveDate,
                CurrencyCode currencyCode,
                MutualFund mutualFund)
            {
                _eventType = eventType;
                _currencyCode = currencyCode;
                EffectiveDate = effectiveDate;
                _mutualFund = mutualFund;
            }
            public DateTime EffectiveDate { get; }

            public string TickerSymbol => _mutualFund.TickerSymbol;
            public string EventType => _eventType.EventName;
            public int Priority => _eventType.Priority;

            public MutualFund Process()
            {
                _mutualFund.SetCurrencyCode(_currencyCode);

                return _mutualFund;
            }

            public string GetJsonPayload()
            {
                var jObject = new JObject();

                jObject.Add("currencyCode", _currencyCode.Code);

                return jObject.ToString();
            }
        }
    }
}
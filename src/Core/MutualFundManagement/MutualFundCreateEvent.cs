using System;
using Newtonsoft.Json.Linq;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundCreateEvent : IMutualFundDomainEvent
    {
        public static string CreateJsonPayload(
            string fundName,
            string stringCurrency)
        {
            var jObject = new JObject();

            jObject.Add("name", fundName);
            jObject.Add("currencyCode", stringCurrency);

            return jObject.ToString();
        }

        public static MutualFundCreateEvent CreateFromJson(
            DateTime effectiveDate,
            string tickerSymbol,
            string json)
        {
            var jObject = JObject.Parse(json);

            var nameToken = jObject.SelectToken("name");
            var currencyCodeToken = jObject.SelectToken("currencyCode");

            var currencyCode = new CurrencyCode(currencyCodeToken.ToString());

            return new MutualFundCreateEvent(
                effectiveDate,
                tickerSymbol,
                nameToken.ToString(),
                currencyCode);
        }

        private readonly string _fundName;
        private readonly CurrencyCode _currencyCode;

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
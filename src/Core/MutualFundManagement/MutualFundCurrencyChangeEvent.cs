using System;
using Newtonsoft.Json.Linq;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundCurrencyChangeEvent : IMutualFundDomainEvent
    {
        public static string CreateJsonPayload(
            string stringCurrency)
        {
            var jObject = new JObject();

            jObject.Add("currencyCode", stringCurrency);

            return jObject.ToString();
        }

        public static MutualFundCurrencyChangeEvent CreateFromJson(
            DateTime effectiveDate,
            string json,
            MutualFund mutualFund)
        {
            var jObject = JObject.Parse(json);

            var currencyCodeToken = jObject.SelectToken("currencyCode");

            var currencyCode = new CurrencyCode(currencyCodeToken.ToString());

            return new MutualFundCurrencyChangeEvent(
                effectiveDate,
                currencyCode,
                mutualFund);
        }

        private readonly MutualFund _mutualFund;
        private readonly CurrencyCode _currencyCode;

        public const string EventType = "CurrencyChange";
        public const int PriorityConts = 500;

        public MutualFundCurrencyChangeEvent(
            DateTime effectiveDate,
            CurrencyCode currencyCode,
            MutualFund mutualFund)
        {
            _currencyCode = currencyCode;
            EffectiveDate = effectiveDate;
            _mutualFund = mutualFund;
        }
        public DateTime EffectiveDate { get; }

        public string TickerSymbol
        {
            get { return _mutualFund.TickerSymbol; }
        }

        public MutualFund Process()
        {
            _mutualFund.SetCurrencyCode(_currencyCode);

            return _mutualFund;
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

            jObject.Add("currencyCode", _currencyCode.Code);

            return jObject.ToString();
        }
    }
}
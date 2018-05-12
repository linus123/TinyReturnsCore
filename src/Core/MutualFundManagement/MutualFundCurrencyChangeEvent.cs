using System;
using Newtonsoft.Json.Linq;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundCurrencyChangeEvent : IMutualFundDomainEvent
    {
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
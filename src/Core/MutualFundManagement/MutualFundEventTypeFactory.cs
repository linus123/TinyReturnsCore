using System;
using Newtonsoft.Json.Linq;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundEventTypeFactory
    {
        public IMutualFundEventType[] GetEventTypes()
        {
            return new IMutualFundEventType[]
            {
                new MutualFundEventTypeForCreate(), 
                new MutualFundEventTypeForNameChange(), 
                new MutualFundEventTypeForCurrencyChange(), 
            };
        }
    }

    public class MutualFundEventTypeForCreate : IMutualFundEventType
    {
        public string EventName => "Create";
        public int Priority => 100;

        public IMutualFundDomainEvent CreateEvent(
            DateTime effectiveDate,
            string tickerSymbol,
            string json,
            MutualFund mutualFund)
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
    }

    public class MutualFundEventTypeForNameChange : IMutualFundEventType
    {
        public string EventName => "NameChange";
        public int Priority => 500;

        public IMutualFundDomainEvent CreateEvent(
            DateTime effectiveDate,
            string tickerSymbol,
            string json,
            MutualFund mutualFund)
        {
            var jObject = JObject.Parse(json);

            var nameToken = jObject.SelectToken("name");

            return new MutualFundNameChangeEvent(
                effectiveDate,
                nameToken.ToString(),
                mutualFund);
        }
    }

    public class MutualFundEventTypeForCurrencyChange : IMutualFundEventType
    {
        public string EventName => "CurrencyChange";
        public int Priority => 500;

        public IMutualFundDomainEvent CreateEvent(
            DateTime effectiveDate,
            string tickerSymbol,
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
    }


}
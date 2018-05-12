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

            return new DomainEvent(
                effectiveDate,
                tickerSymbol,
                nameToken.ToString(),
                currencyCode);
        }

        public class DomainEvent : IMutualFundDomainEvent
        {
            private readonly string _fundName;
            private readonly CurrencyCode _currencyCode;

            public const string EventType = "Create";
            public const int PriorityConts = 100;

            public DomainEvent(
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

            return new DomainEvent(
                effectiveDate,
                nameToken.ToString(),
                mutualFund);
        }

        public class DomainEvent : IMutualFundDomainEvent
        {
            private readonly string _newNameValue;
            private readonly MutualFund _mutualFund;

            public const string EventType = "NameChange";
            public const int PriorityConts = 500;

            public DomainEvent(
                DateTime effectiveDate,
                string newNameValue,
                MutualFund mutualFund)
            {
                EffectiveDate = effectiveDate;
                _mutualFund = mutualFund;
                _newNameValue = newNameValue;
            }

            public DateTime EffectiveDate { get; }

            public string TickerSymbol
            {
                get { return _mutualFund.TickerSymbol; }
            }

            public MutualFund Process()
            {
                _mutualFund.Name = _newNameValue;
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

                jObject.Add("name", _newNameValue);

                return jObject.ToString();
            }
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

            return new DomainEvent(
                effectiveDate,
                currencyCode,
                mutualFund);
        }

        public class DomainEvent : IMutualFundDomainEvent
        {
            private readonly MutualFund _mutualFund;
            private readonly CurrencyCode _currencyCode;

            public const string EventType = "CurrencyChange";
            public const int PriorityConts = 500;

            public DomainEvent(
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


}
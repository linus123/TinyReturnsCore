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

    public class MutualFundEventTypeForNameChange : IMutualFundEventType
    {
        public string EventName => "NameChange";
        public int Priority => 500;

        public IMutualFundDomainEvent CreateEventFromJson(
            DateTime effectiveDate,
            string tickerSymbol,
            string json,
            MutualFund mutualFund)
        {
            var jObject = JObject.Parse(json);

            var nameToken = jObject.SelectToken("name");

            return new DomainEvent(
                this,
                effectiveDate,
                nameToken.ToString(),
                mutualFund);
        }

        public IMutualFundDomainEvent CreateEvent(
            DateTime effectiveDate,
            string newName,
            MutualFund mutualFund)
        {
            return new DomainEvent(
                this,
                effectiveDate,
                newName,
                mutualFund);
        }

        public class DomainEvent : IMutualFundDomainEvent
        {
            private readonly string _newNameValue;
            private readonly MutualFund _mutualFund;

            private readonly MutualFundEventTypeForNameChange _eventType;

            public DomainEvent(
                MutualFundEventTypeForNameChange eventType,
                DateTime effectiveDate,
                string newNameValue,
                MutualFund mutualFund)
            {
                _eventType = eventType;
                EffectiveDate = effectiveDate;
                _mutualFund = mutualFund;
                _newNameValue = newNameValue;
            }

            public DateTime EffectiveDate { get; }

            public string TickerSymbol => _mutualFund.TickerSymbol;
            public string EventType => _eventType.EventName;
            public int Priority => _eventType.Priority;

            public MutualFund Process()
            {
                _mutualFund.Name = _newNameValue;
                return _mutualFund;
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
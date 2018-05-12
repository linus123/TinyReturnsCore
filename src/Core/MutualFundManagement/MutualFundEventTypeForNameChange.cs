using System;
using Newtonsoft.Json.Linq;

namespace TinyReturns.Core.MutualFundManagement
{
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
}
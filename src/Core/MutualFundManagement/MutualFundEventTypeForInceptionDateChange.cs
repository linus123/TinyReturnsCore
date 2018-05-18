using System;
using Newtonsoft.Json.Linq;

namespace TinyReturns.SharedKernel.MutualFundManagement
{
    public class MutualFundEventTypeForInceptionDateChange : IMutualFundEventType
    {
        public string EventName => "InceptChange";
        public int Priority => 500;

        public IMutualFundDomainEvent CreateEventFromJson(
            DateTime effectiveDate,
            string tickerSymbol,
            string json,
            MutualFund mutualFund)
        {
            var jObject = JObject.Parse(json);

            var inceptionDateToken = jObject.SelectToken("inceptionDate");

            var incpetionDateString = inceptionDateToken.ToString();

            var strings = incpetionDateString.Split("-");

            var inceptionDate = new DateTime(int.Parse(strings[0]), int.Parse(strings[1]), int.Parse(strings[2]));

            return new DomainEvent(
                this,
                effectiveDate,
                inceptionDate,
                mutualFund);
        }

        public IMutualFundDomainEvent CreateEvent(
            DateTime effectiveDate,
            DateTime inceptionDate,
            MutualFund mutualFund)
        {
            return new DomainEvent(
                this,
                effectiveDate,
                inceptionDate,
                mutualFund);
        }

        public class DomainEvent : IMutualFundDomainEvent
        {
            private readonly MutualFundEventTypeForInceptionDateChange _eventType;
            private readonly DateTime _inceptionDate;
            private readonly MutualFund _mutualFund;

            public DomainEvent(
                MutualFundEventTypeForInceptionDateChange eventType,
                DateTime effectiveDate,
                DateTime inceptionDate,
                MutualFund mutualFund)
            {
                _mutualFund = mutualFund;
                _inceptionDate = inceptionDate;
                _eventType = eventType;
                EffectiveDate = effectiveDate;
            }

            public DateTime EffectiveDate { get; }
            public string TickerSymbol => _mutualFund.TickerSymbol;
            public string EventType => _eventType.EventName;
            public int Priority => _eventType.Priority;

            public MutualFund Process()
            {
                _mutualFund.InceptionDate = _inceptionDate;

                return _mutualFund;
            }

            public string GetJsonPayload()
            {
                var jObject = new JObject();

                jObject.Add("inceptionDate", _inceptionDate.ToString("yyyy-MM-dd"));

                return jObject.ToString();
            }
        }
    }
}
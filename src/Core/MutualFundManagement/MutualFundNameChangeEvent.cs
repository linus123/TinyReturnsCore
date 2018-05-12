using System;
using Newtonsoft.Json.Linq;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundNameChangeEvent : IMutualFundDomainEvent
    {
        public static string CreateJsonPayload(
            string fundName)
        {
            var jObject = new JObject();

            jObject.Add("name", fundName);

            return jObject.ToString();
        }

        public static MutualFundNameChangeEvent CreateFromJson(
            DateTime effectiveDate,
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

        private readonly string _newNameValue;
        private readonly MutualFund _mutualFund;

        public const string EventType = "NameChange";
        public const int PriorityConts = 500;

        public MutualFundNameChangeEvent(
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
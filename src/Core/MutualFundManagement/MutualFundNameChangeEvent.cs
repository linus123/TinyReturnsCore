using System;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundNameChangeEvent : IMutualFundDomainEvent
    {
        private readonly string _newNameValue;
        private readonly MutualFund _mutualFund;

        public const string EventType = "NameChange";

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
    }
}
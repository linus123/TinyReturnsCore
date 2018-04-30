using System;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundNameChangeEvent : MutualFundDomainEvent
    {
        private readonly string _newNameValue;
        private readonly MutualFund _mutualFund;

        public const string EventType = "NameChange";

        public MutualFundNameChangeEvent(
            DateTime effectiveDate,
            string newNameValue,
            MutualFund mutualFund) : base(effectiveDate, mutualFund)
        {
            _mutualFund = mutualFund;
            _newNameValue = newNameValue;
        }

        public override void Process()
        {
            _mutualFund.Name = _newNameValue;
        }

        public override string GetEventType()
        {
            return EventType;
        }
    }
}
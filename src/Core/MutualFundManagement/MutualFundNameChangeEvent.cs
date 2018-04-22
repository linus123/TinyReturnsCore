using System;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundNameChangeEvent : DomainEvent
    {
        private readonly DateTime _effectiveDate;
        private readonly string _newNameValue;
        private readonly MutualFund _mutualFund;

        public MutualFundNameChangeEvent(
            DateTime effectiveDate,
            string newNameValue,
            MutualFund mutualFund) : base(effectiveDate)
        {
            _mutualFund = mutualFund;
            _newNameValue = newNameValue;
            _effectiveDate = effectiveDate;
        }

        public override void Process()
        {
            _mutualFund.Name = _newNameValue;
        }
    }
}
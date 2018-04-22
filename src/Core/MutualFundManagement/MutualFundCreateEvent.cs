using System;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundCreateEvent : DomainEvent
    {
        private readonly DateTime _effectiveDate;
        private readonly string _newNameValue;
        private readonly MutualFund _mutualFund;

        public MutualFundCreateEvent(
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
        }
    }
}
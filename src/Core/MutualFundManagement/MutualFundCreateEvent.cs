using System;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundCreateEvent : MutualFundDomainEvent
    {
        private readonly string _fundName;

        public const string EventType = "Create";

        public MutualFundCreateEvent(
            DateTime effectiveDate,
            MutualFund mutualFund,
            string fundName) : base(effectiveDate, mutualFund)
        {
            _fundName = fundName;
        }

        public override void Process()
        {
            MutualFund.Name = _fundName;
        }

        public override string GetEventType()
        {
            return EventType;
        }
    }
}
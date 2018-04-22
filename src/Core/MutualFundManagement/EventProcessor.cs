using System;

namespace TinyReturns.Core.MutualFundManagement
{
    public class EventProcessor
    {
        public void Process(DomainEvent @event)
        {
            @event.Process();
        }
    }

    public class MutualFund
    {
        private string _tickerSymbol;

        public MutualFund(
            string tickerSymbol)
        {
            _tickerSymbol = tickerSymbol;
        }

        public string Name { get; set; }
    }

    public class NameChangeEvent : DomainEvent
    {
        private readonly DateTime _effectiveDate;
        private readonly string _newNameValue;
        private readonly MutualFund _mutualFund;

        public NameChangeEvent(
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

    public abstract class DomainEvent
    {
        private DateTime _effectiveDate;

        protected DomainEvent(
            DateTime effectiveDate)
        {
            _effectiveDate = effectiveDate;
        }

        public abstract void Process();
    }
}
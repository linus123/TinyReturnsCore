using System.Collections.Generic;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundEventProcessor
    {
        private readonly List<MutualFundDomainEvent> _domainEvents;

        public MutualFundEventProcessor()
        {
            _domainEvents = new List<MutualFundDomainEvent>();
        }

        public void Process(MutualFundDomainEvent mutualFundDomainEvent)
        {
            mutualFundDomainEvent.Process();

            _domainEvents.Add(mutualFundDomainEvent);
        }

        public MutualFundDomainEvent[] GetDomainEvents()
        {
            return _domainEvents.ToArray();
        }
    }
}
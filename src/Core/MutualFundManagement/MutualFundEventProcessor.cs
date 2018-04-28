using System.Collections.Generic;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundEventProcessor
    {
        private readonly List<DomainEvent> _domainEvents;

        public MutualFundEventProcessor()
        {
            _domainEvents = new List<DomainEvent>();
        }

        public void Process(DomainEvent domainEvent)
        {
            domainEvent.Process();

            _domainEvents.Add(domainEvent);
        }
    }
}
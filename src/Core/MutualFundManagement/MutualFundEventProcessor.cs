using System.Collections.Generic;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundEventProcessor
    {
        private readonly List<DomainEvent> _domainEvents;

        public MutualFundEventProcessor(
            IMutualFundEvenDataTableGateway mutualFundEvenDataTableGateway)
        {
            _domainEvents = new List<DomainEvent>();
        }

        public void Process(DomainEvent @event)
        {
            @event.Process();

            _domainEvents.Add(@event);
        }
    }
}
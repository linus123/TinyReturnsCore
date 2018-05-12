using System.Collections.Generic;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundEventProcessor
    {
        private readonly List<IMutualFundDomainEvent> _domainEvents;

        public MutualFundEventProcessor()
        {
            _domainEvents = new List<IMutualFundDomainEvent>();
        }

        public void Process(IMutualFundDomainEvent mutualFundDomainEvent)
        {
            mutualFundDomainEvent.Process();

            _domainEvents.Add(mutualFundDomainEvent);
        }

        public IMutualFundDomainEvent[] GetDomainEvents()
        {
            return _domainEvents.ToArray();
        }
    }
}
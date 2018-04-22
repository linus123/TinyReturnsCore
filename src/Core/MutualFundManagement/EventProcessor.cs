namespace TinyReturns.Core.MutualFundManagement
{
    public class EventProcessor
    {
        public void Process(DomainEvent @event)
        {
            @event.Process();
        }
    }
}
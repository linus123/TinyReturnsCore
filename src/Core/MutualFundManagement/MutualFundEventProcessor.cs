namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundEventProcessor
    {
        public void Process(DomainEvent @event)
        {
            @event.Process();
        }
    }
}
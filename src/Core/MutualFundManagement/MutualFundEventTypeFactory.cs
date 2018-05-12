namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundEventTypeFactory
    {
        public IMutualFundEventType[] GetEventTypes()
        {
            return new IMutualFundEventType[]
            {
                new MutualFundEventTypeForCreate(), 
                new MutualFundEventTypeForNameChange(), 
                new MutualFundEventTypeForCurrencyChange(), 
            };
        }
    }
}
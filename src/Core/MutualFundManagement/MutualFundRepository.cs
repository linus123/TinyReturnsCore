namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundRepository
    {
        public IMaybe<MutualFund> GetByTickerSymbol(
            string tickerSymbol)
        {
            return new MaybeNoValue<MutualFund>();
        }
    }
}
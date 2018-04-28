namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFund
    {
        public MutualFund(
            string tickerSymbol)
        {
            TickerSymbol = tickerSymbol;
        }

        public string TickerSymbol { get; }

        public string Name { get; set; }
    }
}
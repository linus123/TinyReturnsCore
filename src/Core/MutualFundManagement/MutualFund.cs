namespace TinyReturns.Core.MutualFundManagement
{
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
}
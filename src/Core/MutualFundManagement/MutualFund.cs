namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFund
    {
        private CurrencyCode _currencyCode;

        public MutualFund(
            string tickerSymbol,
            string name,
            CurrencyCode currencyCode)
        {
            _currencyCode = currencyCode;
            TickerSymbol = tickerSymbol;
            Name = name;
        }

        public string TickerSymbol { get; }

        public string Name { get; set; }

        public string CurrencyCodeAsString
        {
            get { return _currencyCode.Code; }
        }

        public void SetCurrencyCode(
            CurrencyCode currencyCode)
        {
            _currencyCode = currencyCode;
        }

    }

    public class CurrencyCode
    {
        public string Code { get; }

        public CurrencyCode(
            string code)
        {
            Code = code.ToUpper();
        }

        public override string ToString()
        {
            return Code;
        }
    }
}
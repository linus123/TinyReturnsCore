namespace TinyReturns.Core.MutualFundManagement
{
    public interface IMutualFundEvenDataTableGateway
    {
        MutualFundEvenDto[] GetAllForTickerSymbol(
            string tickerSymbol);

        void DeleteAll();
    }
}
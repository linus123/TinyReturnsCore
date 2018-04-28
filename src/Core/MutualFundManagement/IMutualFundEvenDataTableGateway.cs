namespace TinyReturns.Core.MutualFundManagement
{
    public interface IMutualFundEvenDataTableGateway
    {
        MutualFundEvenDto[] GetForTickerSymbol(
            string tickerSymbol);

        void Insert(MutualFundEvenDto[] dto);

        void DeleteAll();
    }
}
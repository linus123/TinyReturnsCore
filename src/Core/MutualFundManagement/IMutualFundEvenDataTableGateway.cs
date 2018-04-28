namespace TinyReturns.Core.MutualFundManagement
{
    public interface IMutualFundEvenDataTableGateway
    {
        MutualFundEvenDto[] GetAllForTickerSymbol(
            string tickerSymbol);

        void Insert(MutualFundEvenDto[] dto);

        void DeleteAll();
    }
}
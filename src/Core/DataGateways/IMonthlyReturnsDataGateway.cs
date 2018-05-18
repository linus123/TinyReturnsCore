namespace TinyReturns.SharedKernel.DataGateways
{
    public interface IMonthlyReturnsDataGateway
    {
        void InsertMonthlyReturns(MonthlyReturnDto[] monthlyReturns);

        MonthlyReturnDto[] GetMonthlyReturns(int returnSeriesId);

        MonthlyReturnDto[] GetMonthlyReturns(int[] returnSeriesIds);

        void DeleteMonthlyReturns(int returnSeriesId);

        void DeleteAllMonthlyReturns();
    }
}
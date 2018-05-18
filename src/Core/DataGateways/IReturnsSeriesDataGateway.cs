namespace TinyReturns.Core.DataGateways
{
    public interface IReturnsSeriesDataGateway
    {
        int InsertReturnSeries(ReturnSeriesDto returnSeries);

        Maybe<ReturnSeriesDto> GetReturnSeries(int returnSeriesId);

        void DeleteReturnSeries(int returnSeriesId);

        ReturnSeriesDto[] GetReturnSeries(int[] entityNumbers);

        void DeleteAllReturnSeries();
    }
}
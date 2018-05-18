using System.Collections.Generic;
using System.Linq;

namespace TinyReturns.UnitTests.SharedKernel.DataRepositories
{
    public class ReturnsSeriesDataGatewayStub : IReturnsSeriesDataGateway
    {
        private readonly List<ReturnSeriesDto> _returnSeriesDtos;
        private int _index;

        public ReturnsSeriesDataGatewayStub()
        {
            _returnSeriesDtos = new List<ReturnSeriesDto>();

            _index = 0;
        }

        public int InsertReturnSeries(ReturnSeriesDto returnSeries)
        {
            _returnSeriesDtos.Add(returnSeries);
            _index ++;
            return _index;
        }

        public Maybe<ReturnSeriesDto> GetReturnSeries(int returnSeriesId)
        {
            var returnSeriesDto = _returnSeriesDtos
                .FirstOrDefault(dto => dto.ReturnSeriesId == returnSeriesId);

            if (returnSeriesDto == null)
                return Maybe<ReturnSeriesDto>.None;

            return Maybe<ReturnSeriesDto>.Some(returnSeriesDto);
        }

        public void DeleteReturnSeries(int returnSeriesId)
        {
            _returnSeriesDtos
                .RemoveAll(dto => dto.ReturnSeriesId == returnSeriesId);
        }

        public ReturnSeriesDto[] GetReturnSeries(int[] entityNumbers)
        {
            return _returnSeriesDtos
                .Where(dto => entityNumbers.Any(n => dto.InvestmentVehicleNumber == n))
                .ToArray();
        }

        public void DeleteAllReturnSeries()
        {
            _returnSeriesDtos.Clear();
        }
    }
}
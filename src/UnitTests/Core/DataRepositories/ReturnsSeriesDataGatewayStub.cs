using System.Collections.Generic;
using System.Linq;
using TinyReturns.Core;
using TinyReturns.Core.DataRepositories;

namespace TinyReturns.UnitTests.Core.DataRepositories
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

        public IMaybe<ReturnSeriesDto> GetReturnSeries(int returnSeriesId)
        {
            var returnSeriesDto = _returnSeriesDtos
                .FirstOrDefault(dto => dto.ReturnSeriesId == returnSeriesId);

            if (returnSeriesDto == null)
                return new MaybeNoValue<ReturnSeriesDto>();

            return new MaybeValue<ReturnSeriesDto>(returnSeriesDto);
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
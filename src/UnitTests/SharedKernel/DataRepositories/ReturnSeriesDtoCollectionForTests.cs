using System.Collections.Generic;

namespace TinyReturns.UnitTests.SharedKernel.DataRepositories
{
    public class ReturnSeriesDtoCollectionForTests
    {
        private readonly List<ReturnSeriesDto> _returnSeriesDtoList;

        public ReturnSeriesDtoCollectionForTests()
        {
            _returnSeriesDtoList = new List<ReturnSeriesDto>();
        }

        public ReturnSeriesDtoCollectionForTests AddNetOfFeesReturnSeries(
            int returnSeriesId,
            int entityNumber)
        {
            var n = ReturnSeriesDto.CreateForNetOfFees(returnSeriesId, entityNumber);
            _returnSeriesDtoList.Add(n);

            return this;
        }

        public ReturnSeriesDtoCollectionForTests AddNetOfGrossReturnSeries(
            int returnSeriesId,
            int entityNumber)
        {
            var n = ReturnSeriesDto.CreateForGrossOfFees(returnSeriesId, entityNumber);
            _returnSeriesDtoList.Add(n);

            return this;
        }

        public ReturnSeriesDto[] GetReturnSeriesDtos()
        {
            return _returnSeriesDtoList.ToArray();
        }
    }
}
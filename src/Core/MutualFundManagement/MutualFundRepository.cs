using System.Linq;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundRepository
    {
        private readonly IMutualFundEvenDataTableGateway _mutualFundEvenDataTableGateway;

        public MutualFundRepository(
            IMutualFundEvenDataTableGateway mutualFundEvenDataTableGateway)
        {
            _mutualFundEvenDataTableGateway = mutualFundEvenDataTableGateway;
        }

        public IMaybe<MutualFund> GetByTickerSymbol(
            string tickerSymbol)
        {
            var events = _mutualFundEvenDataTableGateway
                .GetAllForTickerSymbol(tickerSymbol)
                .OrderBy(e => e.EffectiveDate)
                .ToArray();

            if (events.Any(e => e.EventType == "Create"))
            {
                var mutualFund = new MutualFund(tickerSymbol);

                return new MaybeValue<MutualFund>(mutualFund);
            }

            return new MaybeNoValue<MutualFund>();
        }
    }
}
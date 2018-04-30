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
            var eventDtos = _mutualFundEvenDataTableGateway
                .GetForTickerSymbol(tickerSymbol)
                .OrderBy(e => e.EffectiveDate);

            MutualFund mutualFund = null;

            var mutualFundEventProcessor = new MutualFundEventProcessor();

            foreach (var mutualFundEvenDto in eventDtos)
            {
                if (mutualFundEvenDto.EventType == MutualFundCreateEvent.EventType)
                {
                    mutualFund = new MutualFund(mutualFundEvenDto.TickerSymbol);
                }

                if (mutualFund != null)
                {
                    if (mutualFundEvenDto.EventType == MutualFundNameChangeEvent.EventType)
                    {
                        var mutualFundNameChangeEvent = new MutualFundNameChangeEvent(
                            mutualFundEvenDto.EffectiveDate,
                            mutualFundEvenDto.JsonPayload,
                            mutualFund);

                        mutualFundEventProcessor.Process(mutualFundNameChangeEvent);
                    }
                }
            }

            if (mutualFund == null)
                return new MaybeNoValue<MutualFund>();

            return new MaybeValue<MutualFund>(mutualFund);
        }
    }
}
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

        public Maybe<MutualFund> GetByTickerSymbol(
            string tickerSymbol)
        {
            var eventDtos = _mutualFundEvenDataTableGateway
                .GetForTickerSymbol(tickerSymbol)
                .OrderBy(e => e.EffectiveDate);

            MutualFund mutualFund = null;

            foreach (var mutualFundEvenDto in eventDtos)
            {
                if (mutualFundEvenDto.EventType == MutualFundCreateEvent.EventType)
                {
                    var createEvent = MutualFundCreateEvent.CreateFromJson(
                        mutualFundEvenDto.EffectiveDate,
                        mutualFundEvenDto.TickerSymbol,
                        mutualFundEvenDto.JsonPayload);

                    mutualFund = createEvent.Process();
                }

                if (mutualFund != null)
                {
                    if (mutualFundEvenDto.EventType == MutualFundNameChangeEvent.EventType)
                    {
                        var mutualFundNameChangeEvent = MutualFundNameChangeEvent.CreateFromJson(
                            mutualFundEvenDto.EffectiveDate,
                            mutualFundEvenDto.JsonPayload,
                            mutualFund);

                        mutualFund = mutualFundNameChangeEvent.Process();
                    }

                    if (mutualFundEvenDto.EventType == MutualFundCurrencyChangeEvent.EventType)
                    {
                        var mutualFundNameChangeEvent = MutualFundCurrencyChangeEvent.CreateFromJson(
                            mutualFundEvenDto.EffectiveDate,
                            mutualFundEvenDto.JsonPayload,
                            mutualFund);

                        mutualFund = mutualFundNameChangeEvent.Process();
                    }

                }
            }

            if (mutualFund == null)
                return Maybe<MutualFund>.None;

            return Maybe<MutualFund>.Some(mutualFund);
        }
    }
}
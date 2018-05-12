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
                .GetForTickerSymbol(tickerSymbol);

            var mutualFundEventTypeFactory = new MutualFundEventTypeFactory();

            var mutualFundEventTypes = mutualFundEventTypeFactory.GetEventTypes();

            MutualFund mutualFund = null;

            foreach (var mutualFundEvenDto in eventDtos)
            {
                var mutualFundEventType = mutualFundEventTypes.First(t => t.EventName == mutualFundEvenDto.EventType);

                var mutualFundDomainEvent = mutualFundEventType.CreateEvent(
                    mutualFundEvenDto.EffectiveDate,
                    mutualFundEvenDto.TickerSymbol,
                    mutualFundEvenDto.JsonPayload,
                    mutualFund);

                mutualFund = mutualFundDomainEvent.Process();
            }

            if (mutualFund == null)
                return Maybe<MutualFund>.None;

            return Maybe<MutualFund>.Some(mutualFund);
        }
    }
}
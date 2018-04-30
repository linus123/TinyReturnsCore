using System.Collections.Generic;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundDomainEventRepository
    {
        private readonly IMutualFundEvenDataTableGateway _mutualFundEvenDataTableGateway;
        private readonly IClock _clock;

        public MutualFundDomainEventRepository(
            IMutualFundEvenDataTableGateway mutualFundEvenDataTableGateway,
            IClock clock)
        {
            _clock = clock;
            _mutualFundEvenDataTableGateway = mutualFundEvenDataTableGateway;
        }

        public void SaveEvents(
            MutualFundDomainEvent[] mutualFundDomainEvent)
        {
            var mutualFundEvenDtos = new List<MutualFundEvenDto>();

            foreach (var fundDomainEvent in mutualFundDomainEvent)
            {
                var mutualFundEvenDto = new MutualFundEvenDto()
                {
                    EventType = fundDomainEvent.GetEventType(),
                    EffectiveDate = fundDomainEvent.EffectiveDate,
                    TickerSymbol = fundDomainEvent.TickerSymbol,
                    JsonPayload = string.Empty,
                    DateCreated = _clock.GetCurrentDateTime()
                };

                mutualFundEvenDtos.Add(mutualFundEvenDto);
            }

            _mutualFundEvenDataTableGateway.Insert(
                mutualFundEvenDtos.ToArray());
        }
    }
}
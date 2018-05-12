﻿using System.Linq;
using Newtonsoft.Json.Linq;

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

            var mutualFundEventProcessor = new MutualFundEventProcessor();

            foreach (var mutualFundEvenDto in eventDtos)
            {
                if (mutualFundEvenDto.EventType == MutualFundCreateEvent.EventType)
                {
                    var jObject = JObject.Parse(mutualFundEvenDto.JsonPayload);

                    var nameToken = jObject.SelectToken("name");
                    var currencyCodeToken = jObject.SelectToken("currencyCode");

                    var currencyCode = new CurrencyCode(currencyCodeToken.ToString());

                    mutualFund = new MutualFund(
                        mutualFundEvenDto.TickerSymbol,
                        nameToken.ToString(),
                        currencyCode);
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
                return Maybe<MutualFund>.None;

            return Maybe<MutualFund>.Some(mutualFund);
        }
    }
}
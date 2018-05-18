using System;

namespace TinyReturns.SharedKernel.MutualFundManagement
{
    public class MutualFundEvenDto
    {
        public int EventId { get; set; }
        public string TickerSymbol { get; set; }
        public string EventType { get; set; }
        public string JsonPayload { get; set; }
        public int Priority { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
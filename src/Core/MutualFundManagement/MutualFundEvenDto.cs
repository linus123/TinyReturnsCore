using System;

namespace TinyReturns.Core.MutualFundManagement
{
    public class MutualFundEvenDto
    {
        public int EventId { get; set; }
        public string EventType { get; set; }
        public string NewValue { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
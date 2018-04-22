using System;

namespace TinyReturns.Core.MutualFundManagement
{
    public abstract class DomainEvent
    {
        private DateTime _effectiveDate;

        protected DomainEvent(
            DateTime effectiveDate)
        {
            _effectiveDate = effectiveDate;
        }

        public abstract void Process();
    }
}
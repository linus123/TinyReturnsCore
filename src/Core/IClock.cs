using System;

namespace TinyReturns.SharedKernel
{
    public interface IClock
    {
        DateTime GetCurrentDateTime();
    }

    public class Clock : IClock
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
using System;

namespace TinyReturns.Core
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
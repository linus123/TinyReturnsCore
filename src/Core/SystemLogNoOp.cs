using System;

namespace Dimensional.TinyReturns.Core
{
    public class SystemLogNoOp : ISystemLog
    {
        public void Error(string message)
        {
        }

        public void Error(string message, Exception ex)
        {
        }

        public void Warn(string message)
        {
        }

        public void Warn(string message, Exception ex)
        {
        }

        public void Info(string message)
        {
        }

        public void Info(string message, Exception ex)
        {
        }

        public void Debug(string message)
        {
        }
    }
}
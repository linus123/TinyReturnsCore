using System;

namespace TinyReturns.Core
{
    public interface IMaybe<T>
    {
        T Value { get; }
        bool HasValue { get; }
    }

    public class MaybeValue<T> : IMaybe<T>
    {
        public MaybeValue(T value)
        {
            Value = value;
        }

        public T Value { get; }

        public bool HasValue
        {
            get { return true; }
        }
    }

    public class MaybeNoValue<T> : IMaybe<T>
    {
        public T Value
        {
            get
            {
                throw new Exception("No value set.");
            }

        }

        public bool HasValue
        {
            get { return false; }
        }
    }

    public static class MaybeExtensions
    {
        public static bool HasNoValue<T>(
            this IMaybe<T> m)
        {
            return !m.HasValue;
        }
    }
}
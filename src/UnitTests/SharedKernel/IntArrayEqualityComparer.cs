﻿using System.Collections.Generic;
using System.Linq;

namespace TinyReturns.UnitTests.SharedKernel
{
    public class IntArrayEqualityComparer : IEqualityComparer<int[]>
    {
        public bool Equals(int[] x, int[] y)
        {
            var sequenceEqual = x.SequenceEqual(y);
            return sequenceEqual;
        }

        public int GetHashCode(int[] obj)
        {
            return 0;
        }
    }
}
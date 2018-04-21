﻿using TinyReturns.Core;
using Xunit;

namespace TinyReturns.UnitTests.Core
{
    public static class ReturnResultTestExtensions
    {
        public static void ShouldBeSameAs(
            this ReturnResult resultUnderTest,
            ReturnResult expectedResult)
        {
            Assert.Equal(expectedResult.Value, resultUnderTest.Value);
            Assert.Equal(expectedResult.Calculation, resultUnderTest.Calculation);
            Assert.Equal(expectedResult.HasError, resultUnderTest.HasError);
            Assert.Equal(expectedResult.ErrorMessage, resultUnderTest.ErrorMessage);
        }
    }
}

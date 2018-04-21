﻿using TinyReturns.Core;
using Xunit;

namespace Dimensional.TinyReturns.UnitTests.Core
{
    public class InvestmentVehicleTypeTests
    {
        [Fact]
        public void FromCodeShouldReturnEntityTypeWhenGivenMatchingCode()
        {
            var entityType = InvestmentVehicleType.FromCode(InvestmentVehicleType.Portfolio.Code);
            Assert.Equal(entityType, InvestmentVehicleType.Portfolio);
        }

        [Fact]
        public void FromCodeShouldReturnNullWhenGivenNonMatchingCode()
        {
            var entityType = InvestmentVehicleType.FromCode('3');
            Assert.Null(entityType);
        }
    }
}
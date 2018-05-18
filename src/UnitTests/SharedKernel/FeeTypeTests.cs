using TinyReturns.SharedKernel;
using Xunit;

namespace TinyReturns.UnitTests.SharedKernel
{
    public class FeeTypeTests
    {
        [Fact]
        public void GetFeeTypeForFileNameShouldReturnNoneByDefault()
        {
            Assert.Equal(FeeType.GetFeeTypeForFileName(""), FeeType.None);
        }

        [Fact]
        public void GetFeeTypeForFileNameShouldReturnNetOfFeeWhenFileNameContainsNet()
        {
            Assert.Equal(FeeType.GetFeeTypeForFileName("FileNameNet"), FeeType.NetOfFees);
        }

        [Fact]
        public void GetFeeTypeForFileNameShouldReturnGrossOfFeeWhenFileNameContainsGross()
        {
            Assert.Equal(FeeType.GetFeeTypeForFileName("FileNameGross"), FeeType.GrossOfFees);
        }

        [Fact]
        public void FromCodeShouldReturnTypeGivenValidCode()
        {
            Assert.Equal(FeeType.FromCode(FeeType.GrossOfFees.Code), FeeType.GrossOfFees);
        }

        [Fact]
        public void FromCodeShouldReturnNullGivenInvalidCode()
        {
            Assert.Null(FeeType.FromCode('X'));
        }
    }
}
using Xunit;
using static NResult.Helpers;

namespace NResult.Tests
{
    public class BasicResult_T_E_ExpectationsTests
    {
        private Result<int, string> IsPositive(int i)
        {
            if (i > 0)
            {
                return OK(i);
            }
            else
            {
                return Fail("wrong");
            }
        }

        [Fact]
        public void IsAssignableToExpectedInterface()
        {
            var sut = IsPositive(1);

            Assert.IsAssignableFrom<IResult<int,string>>(sut);
        }

        [Fact]
        public void IsOkAndSetCorrectValuePart()
        {
            var sut = IsPositive(1);

            Assert.True(sut.IsOK);
            Assert.Equal(1, sut.Value);
        }

        [Fact]
        public void IsNotOkAndSetErrPartAsExpected()
        {
            var sut = IsPositive(-1);

            Assert.False(sut.IsOK);
            Assert.Equal("wrong", sut.Err);
        }

    }
}

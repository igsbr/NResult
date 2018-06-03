using System;
using Xunit;
using static NResult.Helpers;

namespace NResult.Tests
{
    public class BasicResult_T_ExpectationsTests
    {
        class ValidationError : Exception { }

        private Result<int> IsPositive(int i)
        {
            if (i > 0)
            {
                return OK(i);
            }
            else
            {
                return Error(new ValidationError());
            }
        }

        [Fact]
        public void IsAssignableToExpectedInterface()
        {
            var sut = IsPositive(2);

            Assert.IsAssignableFrom<IResult<int>>(sut);
            Assert.IsAssignableFrom<IResult<int, Exception>>(sut);
        }

        [Fact]
        public void IsOkAndSetCorrectValuePart()
        {
            var sut = IsPositive(2);

            Assert.True(sut.IsOK);
            Assert.Equal(2, sut.Value);
        }

        [Fact]
        public void IsNotOkAndSetCorrectExceptionAsErrPart()
        {
            var sut = IsPositive(-1);

            Assert.False(sut.IsOK);
            Assert.IsAssignableFrom<ValidationError>(sut.Err);
        }

    }
}

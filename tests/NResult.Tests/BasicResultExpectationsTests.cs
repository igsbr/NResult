using System;
using Xunit;
using static NResult.Helpers;

namespace NResult.Tests
{
    public class BasicResultExpectationsTests
    {
        class ValidationError : Exception { }

        private Result IsPositive(int i)
        {
            if (i > 0)
            {
                return OK();
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

            Assert.IsAssignableFrom<IResult>(sut);
            Assert.IsAssignableFrom<IResult<Nothing>>(sut);
            Assert.IsAssignableFrom<IResult<Nothing, Exception>>(sut);
        }

        [Fact]
        public void IsOkAndSetCorrectValuePart()
        {
            var sut = IsPositive(2);

            Assert.True(sut.IsOK);
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

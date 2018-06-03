using Xunit;
using static NResult.Helpers;

namespace NResult.Tests
{
    public class ResultUnwrapExtensionsTests
    {
        [Fact]
        public void DeconstructionOkVariantIsCorrect()
        {
            IResult<int,string> sut = new Result<int, string>(1);
            var (isOK, Value, _) = sut;

            Assert.True(isOK);
            Assert.Equal(1, Value);
        }

        [Fact]
        public void DeconstructionNotOkVariantIsCorrect()
        {
            IResult<int, string> sut = new Result<int, string>("foo");
            var (isOK, _, Err) = sut;

            Assert.False(isOK);
            Assert.Equal("foo", Err);
        }

        [Fact]
        public void IsMatchFunctionCallsFirstArgLambdaWhenResultIsOK()
        {
            IResult<int, string> sut = new Result<int, string>(1);

            var actual = sut.Match(
                    ok => ok.ToString(),
                    err => err
                );

            Assert.Equal(1.ToString(), actual);
        }

        [Fact]
        public void IsMatchFunctionCallsSecondArgLambdaWhenResultIsNotOK()
        {
            IResult<int, string> sut = new Result<int, string>("wrong");

            var actual = sut.Match(
                    ok => ok.ToString(),
                    err => err
                );

            Assert.Equal("wrong", actual);
        }

        [Fact]
        public void IsMatchMethodCallsFirstArgLambdaWhenResultIsOK()
        {
            IResult<int, string> sut = new Result<int, string>(1);

            int? actualOk = null;
            string actualErr = "nothing";

            sut.Match(
                    ok => actualOk = ok,
                    err => actualErr = err
                );

            Assert.Equal(1, actualOk);
            Assert.Equal("nothing", actualErr);
        }

        [Fact]
        public void IsMatchMethodCallsSecondArgLambdaWhenResultIsNotOK()
        {
            IResult<int, string> sut = new Result<int, string>("wrong");

            int? actualOk = null;
            string actualErr = "nothing";

            sut.Match(
                    ok => actualOk = ok,
                    err => actualErr = err
                );

            Assert.Null(actualOk);
            Assert.Equal("wrong", actualErr);
        }
    }
}

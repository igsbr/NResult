using Xunit;

namespace NResult.Tests
{
    public class ResultFromMethodsTests
    {
        [Fact]
        public void VerifyCorrectlyWrapsValue()
        {
            var actual = Result.FromValue("foo");

            Assert.IsAssignableFrom<IResult<string>>(actual);

            Assert.True(actual.IsOK);
            Assert.Equal("foo", actual.Value);
        }

        [Fact]
        public void VerifyCorrectlyWrapsActionDelegate()
        {
            int? i = null;
            var sut = Result.FromAction(() => i = 42);

            var actual = sut();

            Assert.IsAssignableFrom<IResult>(actual);

            Assert.True(actual.IsOK);
            Assert.Equal(42, i);
        }

        [Fact]
        public void VerifyCorrectlyWrapsFuncDelegate()
        {
            var sut = Result.FromFunc(() => "foo");

            var actual = sut();

            Assert.IsAssignableFrom<IResult<string>>(actual);

            Assert.True(actual.IsOK);
            Assert.Equal("foo", actual.Value);
        }

        [Fact]
        public void VerifyCorrectlyWrapsFunc1Delegate()
        {
            var sut = Result.FromFunc((int x1) => 7 + x1);

            var actual = sut(11);

            Assert.IsAssignableFrom<IResult<int>>(actual);

            Assert.True(actual.IsOK);
            Assert.Equal(7 + 11, actual.Value);
        }

        [Fact]
        public void VerifyCorrectlyWrapsFunc2Delegate()
        {
            var sut = Result.FromFunc((int x1, int x2) => 7 + x1 + x2);

            var actual = sut(11, 13);

            Assert.IsAssignableFrom<IResult<int>>(actual);

            Assert.True(actual.IsOK);
            Assert.Equal(7 + 11 + 13, actual.Value);
        }

        [Fact]
        public void VerifyCorrectlyWrapsFunc3Delegate()
        {
            var sut = Result.FromFunc((int x1, int x2, int x3) => 7 + x1 + x2 + x3);

            var actual = sut(11, 13, 17);

            Assert.IsAssignableFrom<IResult<int>>(actual);

            Assert.True(actual.IsOK);
            Assert.Equal(7 + 11 + 13 + 17, actual.Value);
        }

        [Fact]
        public void VerifyCorrectlyWrapsFunc4Delegate()
        {
            var sut = Result.FromFunc((int x1, int x2, int x3, int x4) => 7 + x1 + x2 + x3 + x4);

            var actual = sut(11, 13, 17, 19);

            Assert.IsAssignableFrom<IResult<int>>(actual);

            Assert.True(actual.IsOK);
            Assert.Equal(7 + 11 + 13 + 17 + 19, actual.Value);
        }

        [Fact]
        public void VerifyCorrectlyWrapsFunc5Delegate()
        {
            var sut = Result.FromFunc((int x1, int x2, int x3, int x4, int x5) 
                => 7 + x1 + x2 + x3 + x4 + x5);

            var actual = sut(11, 13, 17, 19, 23);

            Assert.IsAssignableFrom<IResult<int>>(actual);

            Assert.True(actual.IsOK);
            Assert.Equal(7 + 11 + 13 + 17 + 19 + 23, actual.Value);
        }
    }
}

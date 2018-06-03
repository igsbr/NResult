using System;
using Xunit;

namespace NResult.Tests
{
    public class ResultStaticCreationMethodsTests
    {
        class ValidationError : Exception { }

        [Fact]
        public void CreatedResultUsing_Ok_WithArgIsCorrect()
        {
            var sut = Result.OK(42);

            Assert.IsAssignableFrom<IResult<int, Nothing>>(sut);

            Assert.True(sut.IsOK);
            Assert.Equal(42, sut.Value);
        }

        [Fact]
        public void CreatedResultUsing_Ok_WithoutArgsIsCorrect()
        {
            var sut = Result.OK();

            Assert.IsAssignableFrom<IResult<Nothing, Exception>>(sut);

            Assert.True(sut.IsOK);
        }

        [Fact]
        public void CreatedResultUsing_AsOk_WithArgIsCorrect()
        {
            var sut = 42.AsOK();

            Assert.IsAssignableFrom<IResult<int, Nothing>>(sut);

            Assert.True(sut.IsOK);
            Assert.Equal(42, sut.Value);
        }

        [Fact]
        public void CreatedResultUsing_Error_IsCorrect()
        {
            var sut = Result.Error(new ValidationError());

            Assert.IsAssignableFrom<IResult<Nothing, Exception>>(sut);

            Assert.False(sut.IsOK);
            Assert.IsAssignableFrom<ValidationError>(sut.Err);
        }

        [Fact]
        public void CreatedResultUsing_AsError_IsCorrect()
        {
            var sut = (new ValidationError()).AsError();

            Assert.IsAssignableFrom<IResult<Nothing, Exception>>(sut);

            Assert.False(sut.IsOK);
            Assert.IsAssignableFrom<ValidationError>(sut.Err);
        }

        [Fact]
        public void CreatedResultUsing_Fail_IsCorrect()
        {
            var sut = Result.Fail("bar");

            Assert.IsAssignableFrom<IResult<Nothing, string>>(sut);

            Assert.False(sut.IsOK);
            Assert.Equal("bar", sut.Err);
        }

        [Fact]
        public void CreatedResultUsing_AsFail_IsCorrect()
        {
            var sut = "bar".AsFail();

            Assert.IsAssignableFrom<IResult<Nothing, string>>(sut);

            Assert.False(sut.IsOK);
            Assert.Equal("bar", sut.Err);
        }
    }
}

using System;
using Xunit;

namespace NResult.Tests
{
    public class QueryExpressionResultCombinatorsTests
    {
        [Fact]
        public void WhenBothResult_T_E_AreOKExpressionReturnsCorrectResult()
        {
            var foo = new Result<int, string>(42);
            var bar = new Result<int, string>(17);

            var actual = from i in foo
                         from j in bar
                         select i + j;

            Assert.IsAssignableFrom<IResult<int, string>>(actual);

            Assert.True(actual.IsOK);
            Assert.Equal(42 + 17, actual.Value);
        }

        [Fact]
        public void WhenBothResult_T_AreOkExpressionReturnsCorrectResult()
        {
            var foo = new Result<int>(42);
            var bar = new Result<int>(17);

            var actual = from i in foo
                         from j in bar
                         select i + j;

            Assert.IsAssignableFrom<IResult<int>>(actual);

            Assert.True(actual.IsOK);
            Assert.Equal(42 + 17, actual.Value);
        }

        [Fact]
        public void WhenFirstResult_T_E_IsNotOkExpressionReturnsErr()
        {
            var foo = new Result<int, string>("wrong");
            var bar = new Result<int, string>(17);

            var actual = from i in foo
                         from j in bar
                         select i + j;

            Assert.IsAssignableFrom<IResult<int, string>>(actual);

            Assert.False(actual.IsOK);
            Assert.Equal("wrong", actual.Err);
        }

        [Fact]
        public void WhenFirstResult_T_IsNotOkExpressionReturnsErr()
        {
            var foo = new Result<int>(new ApplicationException("wrong"));
            var bar = new Result<int>(17);

            var actual = from i in foo
                         from j in bar
                         select i + j;

            Assert.IsAssignableFrom<IResult<int>>(actual);

            Assert.False(actual.IsOK);
            Assert.IsAssignableFrom<ApplicationException>(actual.Err);
            Assert.Equal("wrong", actual.Err.Message);
        }

        [Fact]
        public void WhenSecondResult_T_E_IsNotOkExpressionReturnsErr()
        {
            var foo = new Result<int, string>(17);
            var bar = new Result<int, string>("wrong");

            var actual = from i in foo
                         from j in bar
                         select i + j;

            Assert.IsAssignableFrom<IResult<int, string>>(actual);

            Assert.False(actual.IsOK);
            Assert.Equal("wrong", actual.Err);
        }

        [Fact]
        public void WhenSecondResult_T_IsNotOkExpressionReturnsErr()
        {
            var foo = new Result<int>(17);
            var bar = new Result<int>(new ApplicationException("wrong"));

            var actual = from i in foo
                         from j in bar
                         select i + j;

            Assert.IsAssignableFrom<IResult<int>>(actual);

            Assert.False(actual.IsOK);
            Assert.IsAssignableFrom<ApplicationException>(actual.Err);
            Assert.Equal("wrong", actual.Err.Message);
        }

        [Fact]
        public void WhenBothResult_T_E_AreOkWithLetClauseIsOkExpressionReturnsCorrectResult()
        {
            var foo = new Result<int, string>(42);
            var bar = new Result<int, string>(17);

            var actual = from i in foo
                         let k = i / 2
                         from j in bar
                         select k + j;

            Assert.IsAssignableFrom<IResult<int, string>>(actual);

            Assert.True(actual.IsOK);
            Assert.Equal(42 / 2 + 17, actual.Value);
        }

        [Fact]
        public void WhenBothResult_T_AreOkWithLetClauseIsOkExpressionReturnsCorrectResult()
        {
            var foo = new Result<int>(42);
            var bar = new Result<int>(17);

            var actual = from i in foo
                         let k = i / 2
                         from j in bar
                         select k + j;

            Assert.IsAssignableFrom<IResult<int>>(actual);

            Assert.True(actual.IsOK);
            Assert.Equal(42 / 2 + 17, actual.Value);
        }

        [Fact]
        public void WhenCombineResult_T_Exception_AndResult_T_WhenExpressionReturnsCorrectResult()
        {
            var foo = new Result<int, Exception>(42);
            var bar = new Result<int>(17);

            var actualFooBar = from i in foo
                               from j in bar
                               select i + j;

            var actualBarFoo = from j in bar
                               from i in foo
                               select i + j;

            Assert.IsAssignableFrom<IResult<int>>(actualFooBar);
            Assert.IsAssignableFrom<IResult<int, Exception>>(actualBarFoo);
        }
    }
}

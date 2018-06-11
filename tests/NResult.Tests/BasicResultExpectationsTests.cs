using System;
using System.Collections.Generic;
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

        #region Equality tests

        private static Exception exception = new Exception();
        private static Exception appException = new ApplicationException();

        public class EqualityVerifyingTests
        {
            private static (Result, Result, bool)[] DataForEqualityVerifying =>
                new(Result, Result, bool)[]
                {
                    (new Result(), new Result(), true),
                    (new Result(), new Result(exception), false),
                    (new Result(), new Result((Exception)null), false),
                    (new Result(), (Result)null, false),

                    (new Result(exception), new Result(), false),
                    (new Result(exception), new Result(exception), true),
                    (new Result(exception), new Result(appException), false),
                    (new Result(exception), new Result((Exception)null), false),
                    (new Result(exception), (Result)null, false),

                    ((Result)null, new Result(), false),            // for '==' or '!=' only
                    ((Result)null, new Result(exception), false),
                    ((Result)null, new Result((Exception)null), false),
                    ((Result)null, (Result)null, true),
                };

            public static IEnumerable<object[]> EqualOnlyVerifyingIndex => DataForEqualityVerifying.ToIndex(9);

            [Theory]
            [MemberData(nameof(EqualOnlyVerifyingIndex))]
            public void VerifyEqualMethod(int idx)
            {
                var (first, second, expected) = DataForEqualityVerifying[idx];

                var actual = first.Equals(second);

                Assert.Equal(actual, expected);
            }

            public static IEnumerable<object[]> EqualOperatorsVerifyingIndex => DataForEqualityVerifying.ToIndex();

            [Theory]
            [MemberData(nameof(EqualOperatorsVerifyingIndex))]
            public void VerifyEqualOperator(int idx)
            {
                var (first, second, expected) = DataForEqualityVerifying[idx];

                var actual = (first == second);

                Assert.Equal(actual, expected);
            }

            [Theory]
            [MemberData(nameof(EqualOperatorsVerifyingIndex))]
            public void VerifyNotEqualOperator(int idx)
            {
                var (first, second, expected) = DataForEqualityVerifying[idx];

                var actual = (first != second);

                Assert.NotEqual(actual, expected);
            }
        }

        #endregion
    }
}

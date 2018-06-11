using System;
using System.Collections.Generic;
using Xunit;
using static NResult.Helpers;

namespace NResult.Tests
{
    public class BasicResult_T_ExpectationsTests
    {
        private Result<int> IsPositive(int i)
        {
            if (i > 0)
            {
                return OK(i);
            }
            else
            {
                return Error(new SomeUsefulException());
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
            Assert.IsAssignableFrom<SomeUsefulException>(sut.Err);
        }

        [Fact]
        public void CorrectImpliciteConversionFromResult_T_Exception()
        {
            var sut = new Result<int, Exception>(42);
            Result<int> actual = sut;

            Assert.True(sut.IsOK);
            Assert.Equal(42, actual.Value);
        }

        #region Equality tests

        private static Exception exception = new Exception();
        private static Exception appException = new ApplicationException();

        public class WhenValueIsValueTypeEqualityTests
        {
            private static (Result<int>, Result<int>, bool)[] DataForEqualityVerifying =>
                new(Result<int>, Result<int>, bool)[]
                {
                    (new Result<int>(42), new Result<int>(42), true),
                    (new Result<int>(42), new Result<int>(17), false),
                    (new Result<int>(42), new Result<int>(exception), false),
                    (new Result<int>(42), new Result<int>((Exception)null), false),
                    (new Result<int>(42), (Result<int>)null, false),

                    (new Result<int>(exception), new Result<int>(42), false),
                    (new Result<int>(exception), new Result<int>(exception), true),
                    (new Result<int>(exception), new Result<int>(appException), false),
                    (new Result<int>(exception), new Result<int>((Exception)null), false),
                    (new Result<int>(exception), (Result<int>)null, false),

                    ((Result<int>)null, new Result<int>(42), false),            // for '==' or '!=' only
                    ((Result<int>)null, new Result<int>(exception), false),
                    ((Result<int>)null, new Result<int>((Exception)null), false),
                    ((Result<int>)null, (Result<int>)null, true),
                };

            public static IEnumerable<object[]> EqualOnlyVerifyingIndex
                => DataForEqualityVerifying.ToIndex(10);

            [Theory]
            [MemberData(nameof(EqualOnlyVerifyingIndex))]
            public void VerifyEqualMethod(int idx)
            {
                var (first, second, expected) = DataForEqualityVerifying[idx];

                var actual = first.Equals(second);

                Assert.Equal(actual, expected);
            }

            public static IEnumerable<object[]> EqualOperatorsVerifyingIndex
                => DataForEqualityVerifying.ToIndex();

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

        public class WhenValueIsReferenceTypeEqualityTests
        {
            private static (Result<string>, Result<string>, bool)[] DataForEqualityVerifying =>
                new(Result<string>, Result<string>, bool)[]
                {
                    (new Result<string>("foo"), new Result<string>("foo"), true),
                    (new Result<string>("foo"), new Result<string>("bar"), false),
                    (new Result<string>("foo"), new Result<string>(exception), false),
                    (new Result<string>("foo"), new Result<string>((Exception)null), false),
                    (new Result<string>("foo"), (Result<string>)null, false),

                    (new Result<string>(exception), new Result<string>("foo"), false),
                    (new Result<string>(exception), new Result<string>(exception), true),
                    (new Result<string>(exception), new Result<string>(appException), false),
                    (new Result<string>(exception), new Result<string>((Exception)null), false),
                    (new Result<string>(exception), (Result<string>)null, false),

                    ((Result<string>)null, new Result<string>("foo"), false),            // for '==' or '!=' only
                    ((Result<string>)null, new Result<string>(exception), false),
                    ((Result<string>)null, new Result<string>((Exception)null), false),
                    ((Result<string>)null, (Result<string>)null, true),
                };

            public static IEnumerable<object[]> EqualOnlyVerifyingIndex => DataForEqualityVerifying.ToIndex(10);

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

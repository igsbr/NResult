using System.Collections.Generic;
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

            Assert.IsAssignableFrom<IResult<int, string>>(sut);
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

        #region Equality tests

        public class WhenValueIsValueTypeAndErrIsRefTypeEqualityTests
        {
            private static (Result<int,string>, Result<int,string>, bool)[] DataForEqualityVerifying =>
                new(Result<int, string>, Result<int, string>, bool)[]
                {
                    (new Result<int, string>(42), new Result<int, string>(42), true),
                    (new Result<int, string>(42), new Result<int, string>(17), false),
                    (new Result<int, string>(42), new Result<int, string>("foo"), false),
                    (new Result<int, string>(42), new Result<int, string>((string)null), false),
                    (new Result<int, string>(42), (Result<int, string>)null, false),

                    (new Result<int, string>("foo"), new Result<int, string>(42), false),
                    (new Result<int, string>("foo"), new Result<int, string>("foo"), true),
                    (new Result<int, string>("foo"), new Result<int, string>("bar"), false),
                    (new Result<int, string>("foo"), new Result<int, string>((string)null), false),
                    (new Result<int, string>("foo"), (Result<int, string>)null, false),

                    // for '==' or '!=' only

                    ((Result<int, string>)null, new Result<int, string>(42), false),    
                    ((Result<int, string>)null, new Result<int, string>("foo"), false),
                    ((Result<int, string>)null, new Result<int, string>((string)null), false),
                    ((Result<int, string>)null, (Result<int, string>)null, true),
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

        public class WhenValueIsRefTypeAndErrIsValueTypeEqualityTests
        {
            private static (Result<string, int>, Result<string, int>, bool)[] DataForEqualityVerifying =>
                new(Result<string, int>, Result<string, int>, bool)[]
                {
                    (new Result<string, int>("foo"), new Result<string, int>("foo"), true),
                    (new Result<string, int>("foo"), new Result<string, int>("bar"), false),
                    (new Result<string, int>("foo"), new Result<string, int>(42), false),
                    (new Result<string, int>("foo"), new Result<string, int>((string)null), false),
                    (new Result<string, int>("foo"), (Result<string, int>)null, false),

                    (new Result<string, int>(42), new Result<string, int>(42), true),
                    (new Result<string, int>(42), new Result<string, int>(17), false),
                    (new Result<string, int>(42), new Result<string, int>("foo"), false),
                    (new Result<string, int>(42), new Result<string, int>((string)null), false),
                    (new Result<string, int>(42), (Result<string, int>)null, false),

                    // for '==' or '!=' only

                    ((Result<string, int>)null, new Result<string, int>(42), false),
                    ((Result<string, int>)null, new Result<string, int>("foo"), false),
                    ((Result<string, int>)null, new Result<string, int>((string)null), false),
                    ((Result<string, int>)null, (Result<string, int>)null, true),
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

        #endregion
    }
}

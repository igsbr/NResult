using System;
using Xunit;
using static NResult.Helpers;

namespace NResult.Tests
{
    public class CombinatorsTests
    {
        public class MapCombinator
        {
            [Fact]
            public void VerifyOKVariantWithResult_T_E()
            {
                var expected = new Result<double, string>(42 * 2.0);
                var sut = new Result<int, string>(42);

                var actual = sut.Map(i => i * 2.0);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void VerifyErrVariantWithResult_T_E()
            {
                var expected = new Result<double, string>("foo");
                var sut = new Result<int, string>("foo");

                var actual = sut.Map(i => i * 2.0);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void VerifyOKVariantWithResult_T()
            {
                var expected = new Result<double>(42 * 2.0);
                var sut = new Result<int>(42);

                var actual = sut.Map(i => i * 2.0);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void VerifyErrVariantWithResult_T()
            {
                var ex = new Exception();
                var expected = new Result<double>(ex);
                var sut = new Result<int>(ex);

                var actual = sut.Map(i => i * 2.0);

                Assert.Equal(expected, actual);
            }
        }

        public class MapErrCombinator
        {
            [Fact]
            public void VerifyOKVariantWithResult_T_E()
            {
                var expected = new Result<int, string>(42);
                var sut = new Result<int, string>(42);

                var actual = sut.MapFail(e => e + "bar");

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void VerifyErrVariantWithResult_T_E()
            {
                var expected = new Result<int, string>("foobar");
                var sut = new Result<int, string>("foo");

                var actual = sut.MapFail(e => e + "bar");

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void VerifyOKVariantWithResult_T()
            {
                var expected = new Result<int>(42);
                var sut = new Result<int>(42);

                var actual = sut.MapErr(e => new AggregateException(e));

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void VerifyErrVariantWithResult_T_()
            {
                var ex = new Exception();
                var sut = new Result<int>(ex);

                var actual = sut.MapErr(e => new AggregateException(e));

                Assert.False(actual.IsOK);
                Assert.IsType<AggregateException>(actual.Err);
                Assert.Equal(ex, ((AggregateException)actual.Err).InnerExceptions[0]);
            }
        }

        public class AndCombinator
        {
            [Fact]
            public void VerifyOKVariantWithResult_T_E()
            {
                var foo = new Result<int, string>(42);
                var bar = new Result<int, string>(17);

                var actual = foo.And(bar);

                Assert.Equal(bar, actual);
            }

            [Fact]
            public void VerifyErrVariantWithResult_T_E()
            {
                var foo = new Result<int, string>("wrong");
                var bar = new Result<int, string>(17);

                var actual = foo.And(bar);

                Assert.Equal(foo, actual);
            }

            [Fact]
            public void VerifyOKVariantWithResult_T()
            {
                var foo = new Result<int>(42);
                var bar = new Result<int>(17);

                var actual = foo.And(bar);

                Assert.Equal(bar, actual);
            }

            [Fact]
            public void VerifyErrVariantWithResult_T()
            {
                var foo = new Result<int>(new Exception());
                var bar = new Result<int>(17);

                var actual = foo.And(bar);

                Assert.Equal(foo, actual);
            }
        }

        public class OrCombinator
        {
            [Fact]
            public void VerifyOKVariantWithResult_T_E()
            {
                var foo = new Result<int, string>(42);
                var bar = new Result<int, string>(17);

                var actual = foo.Or(bar);

                Assert.Equal(foo, actual);
            }

            [Fact]
            public void VerifyErrVariantWithResult_T_E()
            {
                var foo = new Result<int, string>("wrong");
                var bar = new Result<int, string>(17);

                var actual = foo.Or(bar);

                Assert.Equal(bar, actual);
            }

            [Fact]
            public void VerifyOKVariantWithResult_T()
            {
                var foo = new Result<int>(42);
                var bar = new Result<int>(17);

                var actual = foo.Or(bar);

                Assert.Equal(foo, actual);
            }

            [Fact]
            public void VerifyErrVariantWithResult_T()
            {
                var foo = new Result<int>(new Exception());
                var bar = new Result<int>(17);

                var actual = foo.Or(bar);

                Assert.Equal(bar, actual);
            }
        }

        public class ThenCombinator
        {
            [Fact]
            public void VerifyOKVariantWithResult_T_E()
            {
                Result<int, string> Foo() => OK(42);
                Result<double, string> Bar(int i) => OK(i * 2.0);
                var expected = new Result<double, string>(42 * 2.0);

                var actual = Foo().Then(Bar);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void VerifyErrVariantWithResult_T_E()
            {
                Result<int, string> Foo() => Fail("wrong");
                Result<double, string> Bar(int i) => OK(i * 2.0);
                var expected = new Result<double, string>("wrong");

                var actual = Foo().Then(Bar);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void VerifyOKVariantWithResult_T()
            {
                Result<int> Foo() => OK(42);
                Result<double> Bar(int i) => OK(i * 2.0);
                var expected = new Result<double>(42 * 2.0);

                var actual = Foo().Then(Bar);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void VerifyErrVariantWithResult_T()
            {
                var ex = new Exception();

                Result<int> Foo() => Fail(ex);
                Result<double> Bar(int i) => OK(i * 2.0);
                var expected = new Result<double>(ex);

                var actual = Foo().Then(Bar);

                Assert.Equal(expected, actual);
            }
        }

        public class ElseCombinator
        {
            [Fact]
            public void VerifyOKVariantWithResult_T_E()
            {
                Result<int, bool> Foo() => OK(42);
                Result<int, string> Bar(bool e) => Fail(e.ToString());
                var expected = new Result<int, string>(42);

                var actual = Foo().Else(Bar);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void VerifyErrVariantWithResult_T_E()
            {
                Result<int, bool> Foo() => Fail(true);
                Result<int, string> Bar(bool e) { if (e) return OK(42); else return Fail("wrong"); }
                var expected = new Result<int, string>(42);

                var actual = Foo().Else(Bar);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void VerifyOKVariantWithResult_T()
            {
                Result<int> Foo() => OK(42);
                Result<int> Bar(Exception e) => Error(new AggregateException(e));
                var expected = new Result<int>(42);

                var actual = Foo().Else(Bar);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void VerifyErrVariantWithResult_T()
            {
                Result<int> Foo() => Error(new ApplicationException());
                var expected = new Result<int>(42);

                var actual = Foo().Else(e =>
                {
                    if (e is ApplicationException)
                        return OK(42);
                    else
                        return Error(new AggregateException(e, new Exception()));
                });

                Assert.Equal(expected, actual);
            }
        }
    }
}

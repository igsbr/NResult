using System;

namespace NResult
{
    public partial class Result
    {
        // these methods are common for the most of Result classes
        // because it's much better to use something like: Result.Fail("wrong")
        // instead of: Result<int>.Fail("wrong")

        public static Result<Nothing, Exception> Error(Exception err) => new Result<Nothing, Exception>(err);
        public static Result<Nothing, E> Fail<E>(E err) => new Result<Nothing, E>(err);

        public static Result<T, Nothing> OK<T>(T value) => new Result<T, Nothing>(value);

        // special cases

        public static Result OK() => new Result();

        #region "Result.From" methods

        public static Result<T> FromValue<T>(T value) => new Result<T>(value);

        // wrapped delegates also can be useful

        public static Func<Result> FromAction(Action action)
        {
            return () =>
            {
                try
                {
                    action();
                    return new Result();
                }
                catch (Exception e)
                {
                    return new Result(e);
                }
            };
        }

        public static Func<Result<T>> FromFunc<T>(Func<T> func)
        {
            return () =>
            {
                try
                {
                    return new Result<T>(func());
                }
                catch (Exception e)
                {
                    return new Result<T>(e);
                }
            };
        }

        public static Func<T1, Result<T>> FromFunc<T1, T>(Func<T1, T> func)
        {
            return x1 =>
            {
                try
                {
                    return new Result<T>(func(x1));
                }
                catch (Exception e)
                {
                    return new Result<T>(e);
                }
            };
        }

        public static Func<T1, T2, Result<T>> FromFunc<T1, T2, T>(Func<T1, T2, T> func)
        {
            return (x1, x2) =>
            {
                try
                {
                    return new Result<T>(func(x1, x2));
                }
                catch (Exception e)
                {
                    return new Result<T>(e);
                }
            };
        }

        public static Func<T1, T2, T3, Result<T>> FromFunc<T1, T2, T3, T>(Func<T1, T2, T3, T> func)
        {
            return (x1, x2, x3) =>
            {
                try
                {
                    return new Result<T>(func(x1, x2, x3));
                }
                catch (Exception e)
                {
                    return new Result<T>(e);
                }
            };
        }

        public static Func<T1, T2, T3, T4, Result<T>> FromFunc<T1, T2, T3, T4, T>(Func<T1, T2, T3, T4, T> func)
        {
            return (x1, x2, x3, x4) =>
            {
                try
                {
                    return new Result<T>(func(x1, x2, x3, x4));
                }
                catch (Exception e)
                {
                    return new Result<T>(e);
                }
            };
        }

        public static Func<T1, T2, T3, T4, T5, Result<T>> FromFunc<T1, T2, T3, T4, T5, T>(Func<T1, T2, T3, T4, T5, T> func)
        {
            return (x1, x2, x3, x4, x5) =>
            {
                try
                {
                    return new Result<T>(func(x1, x2, x3, x4, x5));
                }
                catch (Exception e)
                {
                    return new Result<T>(e);
                }
            };
        }

        #endregion
    }
}

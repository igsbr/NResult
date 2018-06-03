using System;

namespace NResult
{
    public class Result<T, E> : IResult<T, E>
    {
        public bool IsOK { get; }
        public T Value { get; }
        public E Err { get; }

        public Result(E err)
        {
            IsOK = false;
            Value = default;
            Err = err;
        }

        public Result(T value)
        {
            IsOK = true;
            Value = value;
            Err = default;
        }

        public static implicit operator Result<T, E>(Result<Nothing, E> resultErr) => new Result<T, E>(resultErr.Err);

        public static implicit operator Result<T, E>(Result<T, Nothing> resultOK) => new Result<T, E>(resultOK.Value);
    }

    public class Result<T> : IResult<T>
    {
        public bool IsOK { get; }
        public T Value { get; }
        public Exception Err { get; }

        public Result(Exception err)
        {
            IsOK = false;
            Value = default;
            Err = err;
        }

        public Result(T value)
        {
            IsOK = true;
            Value = value;
            Err = default;
        }

        public static implicit operator Result<T>(Result<Nothing, Exception> resultErr) => new Result<T>(resultErr.Err);
        public static implicit operator Result<T>(Result<T, Nothing> resultOK) => new Result<T>(resultOK.Value);
    }

    public class Result : IResult
    {
        public bool IsOK { get; }
        public Nothing Value { get; }
        public Exception Err { get; }

        public Result(Exception err)
        {
            IsOK = false;
            Err = err;
        }

        public Result()
        {
            IsOK = true;
            Err = default;
        }

        public static implicit operator Result(Result<Nothing, Exception> resultErr) => new Result(resultErr.Err);

        // these methods are common for the all Result classes
        // because it's much better to use something like: Result.Fail("wrong");
        // instead of: Result<int>.Fail("wrong");

        public static Result<Nothing, Exception> Error(Exception err) => new Result<Nothing, Exception>(err);
        public static Result<Nothing, E> Fail<E>(E err) => new Result<Nothing, E>(err);

        public static Result<T, Nothing> OK<T>(T value) => new Result<T, Nothing>(value);
        public static Result OK() => new Result();
    }
}

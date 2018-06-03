using System;

namespace NResult
{
    public interface IResult<out T, E>
    {
        bool IsOK { get; }
        T Value { get; }
        E Err { get; }
    }

    public interface IResult<out T> : IResult<T, Exception>
    { }

    public interface IResult : IResult<Nothing>
    { }
}

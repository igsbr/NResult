using System;

namespace NResult
{
    public static class ResultUnwrapExtensions
    {
        public static void Deconstruct<T, E>(this IResult<T, E> src, out bool isOK, out T value, out E err)
        {
            isOK = src.IsOK;
            value = src.Value;
            err = src.Err;
        }

        public static TOut Match<T, E, TOut>(
            this IResult<T, E> src,
            Func<T, TOut> ok,
            Func<E, TOut> err)
        {
            return src.IsOK ? ok(src.Value) : err(src.Err);
        }

        public static void Match<T, E>(
            this IResult<T, E> src,
            Action<T> ok,
            Action<E> err)
        {
            if (src.IsOK)
            {
                ok(src.Value);
            }
            else
            {
                err(src.Err);
            }
        }

    }
}

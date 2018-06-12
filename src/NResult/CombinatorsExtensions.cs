using System;

namespace NResult
{
    public static class CombinatorsExtensions
    {
        public static Result<U, E> Map<T, U, E>(this IResult<T, E> src, Func<T, U> op)
        {
            if (!src.IsOK) return new Result<U, E>(src.Err);

            return new Result<U, E>(op(src.Value));
        }

        public static Result<U> Map<T, U>(this IResult<T> src, Func<T, U> op)
        {
            if (!src.IsOK) return new Result<U>(src.Err);

            return new Result<U>(op(src.Value));
        }

        public static Result<T, F> MapFail<T, E, F>(this IResult<T, E> src, Func<E, F> op)
        {
            if (src.IsOK) return new Result<T, F>(src.Value);

            return new Result<T, F>(op(src.Err));
        }

        public static Result<T> MapErr<T>(this IResult<T> src, Func<Exception, Exception> op)
        {
            if (src.IsOK) return new Result<T>(src.Value);

            return new Result<T>(op(src.Err));
        }

        public static Result<T, E> And<T, E>(this IResult<T, E> src, Result<T, E> res)
        {
            if (!src.IsOK) return new Result<T,E>(src.Err);

            return res;
        }

        public static Result<T> And<T>(this IResult<T> src, Result<T> res)
        {
            if (!src.IsOK) return new Result<T>(src.Err);

            return res;
        }

        public static Result<T, E> Or<T, E>(this IResult<T, E> src, Result<T, E> res)
        {
            if (!src.IsOK) return res;

            return new Result<T, E>(src.Value);
        }

        public static Result<T> Or<T>(this IResult<T> src, Result<T> res)
        {
            if (!src.IsOK) return res;

            return new Result<T>(src.Value);
        }

        public static Result<U, E> Then<T, U, E>(this IResult<T, E> src, Func<T, Result<U, E>> op)
        {
            if (!src.IsOK) return new Result<U, E>(src.Err);

            return op(src.Value);
        }

        public static Result<U> Then<T, U>(this IResult<T> src, Func<T, Result<U>> op)
        {
            if (!src.IsOK) return new Result<U>(src.Err);

            return op(src.Value);
        }

        public static Result<T, F> Else<T, E, F>(this IResult<T, E> src, Func<E, Result<T, F>> op)
        {
            if (src.IsOK) return new Result<T, F>(src.Value);

            return op(src.Err);
        }

        public static Result<T> Else<T>(this IResult<T> src, Func<Exception, Result<T>> op)
        {
            if (src.IsOK) return new Result<T>(src.Value);

            return op(src.Err);
        }
    }
}

using System;
using static NResult.Helpers;

namespace NResult
{
    public static class QueryExpressionExtensions
    {
        public static Result<TOut, E> SelectMany<TSrc, TMid, TOut, E>(
            this IResult<TSrc, E> src,
            Func<TSrc, Result<TMid, E>> midSelector,
            Func<TSrc, TMid, TOut> resSelector)
        {
            if (!src.IsOK) return Fail(src.Err);

            var srcValue = src.Value;

            var mid = midSelector(srcValue);

            if (!mid.IsOK) return Fail(mid.Err);

            return OK(resSelector(srcValue, mid.Value));
        }

        public static Result<TOut> SelectMany<TSrc, TMid, TOut>(
            this IResult<TSrc> src,
            Func<TSrc, Result<TMid>> midSelector,
            Func<TSrc, TMid, TOut> resSelector)
        {
            if (!src.IsOK) return Error(src.Err);

            var srcValue = src.Value;

            var mid = midSelector(srcValue);

            if (!mid.IsOK) return Fail(mid.Err);

            return OK(resSelector(srcValue, mid.Value));
        }

        public static Result<TOut> SelectMany<TSrc, TMid, TOut>(
            this IResult<TSrc, Exception> src,
            Func<TSrc, Result<TMid>> midSelector,
            Func<TSrc, TMid, TOut> resSelector)
        {
            if (!src.IsOK) return Error(src.Err);

            var srcValue = src.Value;

            var mid = midSelector(srcValue);

            if (!mid.IsOK) return Fail(mid.Err);

            return OK(resSelector(srcValue, mid.Value));
        }

        public static Result<TOut, E> Select<TSrc, TOut, E>(
            this IResult<TSrc, E> src,
            Func<TSrc, TOut> selector)
        {
            if (!src.IsOK) return Fail(src.Err);

            return OK(selector(src.Value));
        }

        public static Result<TOut> Select<TSrc, TOut>(
            this IResult<TSrc> src,
            Func<TSrc, TOut> selector)
        {
            if (!src.IsOK) return Error(src.Err);

            return OK(selector(src.Value));
        }

    }
}

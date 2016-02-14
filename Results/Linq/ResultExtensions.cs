using System;
using System.Linq;

namespace Results.Linq
{
    public static class ResultExtensions
    {
        public static Result<TResult> SelectMany<TSource, TMiddle, TResult>(
		  this Result<TSource> source,
		  Func<TSource, Result<TMiddle>> middleSelector,
		  Func<TSource, TMiddle, TResult> resultSelector)
        {
            var sourceValue = source.Value;
            var middle = middleSelector(sourceValue);

            return source.Success() && middle.Success()
                ? Result.Return(resultSelector(sourceValue, middle.Value))
                : Result.Error(source.Errors.Concat(middle.Errors));
        }

        public static Result<TResult> Select<TSource, TResult>(
            this Result<TSource> source,
            Func<TSource, TResult> selector)
        {
            var sourceValue = source.Value;

            return source.Success()
                ? Result.Return(selector(sourceValue))
                : Result.Error(source.Errors);
        }
    }
}

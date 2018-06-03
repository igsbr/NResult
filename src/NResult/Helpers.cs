using System;

namespace NResult
{
    public static class Helpers
    {
        // beautify overall usability (with "using static" directive)

        public static Result<Nothing, Exception> Error(Exception err) => Result.Error(err);
        public static Result<Nothing, E> Fail<E>(E err) => Result.Fail(err);

        public static Result<T, Nothing> OK<T>(T value) => Result.OK(value);
        public static Result OK() => Result.OK();

        // more classic extensions

        public static Result<Nothing, Exception> AsError<E>(this E err) where E : Exception => Result.Error(err);
        public static Result<Nothing, E> AsFail<E>(this E err) => Result.Fail(err);
        public static Result<T, Nothing> AsOK<T>(this T value) => Result.OK(value);
    }
}

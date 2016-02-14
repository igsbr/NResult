using System.Linq;
using System.Collections.Generic;

namespace Results
{
    public class Result<T>
    {
        public T Value { get; private set; }

        public List<string> Errors { get; private set; }

        protected Result(T value, List<string> errors)
        {
            this.Value = value;
            this.Errors = errors;
        }

        protected Result(T value)
            : this(value, new List<string>())
        {
        }

        public bool Success()
        {
            return Errors.Count == 0;
        }

        public static Result<T> Return(T value)
        {
            return new Result<T>(value);
        }

        public static implicit operator Result<T>(Result result)
        {
            return new Result<T>(
                value: default(T),
                errors: result.Errors);
        }

    }

    public class Result : Result<Nothing>
    {
        private Result(List<string> errors)
            : base(default(Nothing), errors)
        {
        }

        private Result()
            : base(default(Nothing))
        {
        }

        public static Result Error(params string[] errors)
        {
            return Error(errors as IEnumerable<string>);
        }

        public static Result Error(IEnumerable<string> errors)
        {
            return new Result(errors.ToList());
        }

        public static Result Ok()
        {
            return new Result();
        }

        public static Result<T> Return<T>(T value)
        {
            return Result<T>.Return(value);
        }

        public static Result All(IEnumerable<Result> results)
        {
            return Result.Error(results.SelectMany(r => r.Errors));
        }

        public static Result All(Result first, params Result[] other)
        {
            var results = Enumerable
                .Repeat(first, 1)
                .Concat(other);

            return Result.All(results);
        }
    }

    public sealed class Nothing
    {
    }
}

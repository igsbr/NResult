using System;

namespace NResult
{
    public class Result<T, E> : IResult<T, E>, IEquatable<Result<T, E>>
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

        #region Equality boilerplate

        public bool Equals(Result<T, E> other)
        {
            if (other is null) return false;

            if (this.IsOK && other.IsOK)
            {
                if (this.Value.Equals(default))
                {
                    return other.Value.Equals(default);
                }
                else
                {
                    return this.Value.Equals(other.Value);
                }
            }
            else if (!this.IsOK && !other.IsOK)
            {
                if (this.Err.Equals(default))
                {
                    return other.Err.Equals(default);
                }
                else
                {
                    return this.Err.Equals(other.Err);
                }
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            return this.Equals(obj as Result<T, E>);
        }

        public override int GetHashCode() => IsOK ? (IsOK, Value).GetHashCode() : (IsOK, Err).GetHashCode();

        public static bool operator ==(Result<T, E> first, Result<T, E> second)
        {
            if (first is null) return (second is null);

            return first.Equals(second);
        }

        public static bool operator !=(Result<T, E> first, Result<T, E> second)
        {
            if (first is null) return !(second is null);

            return !first.Equals(second);
        }

        #endregion
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

        public static implicit operator Result<T>(Result<T, Exception> res) => new Result<T>(res.Value);

        #region Equality boilerplate

        public bool Equals(Result<T> other)
        {
            if (other is null) return false;

            if (this.IsOK && other.IsOK)
            {
                if (this.Value.Equals(default))
                {
                    return other.Value.Equals(default);
                }
                else
                {
                    return this.Value.Equals(other.Value);
                }
            }
            else if(!this.IsOK && !other.IsOK)
            {
                if (this.Err is null)
                {
                    return other.Err is null;
                }
                else
                {
                    return this.Err.Equals(other.Err);
                }
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            return this.Equals(obj as Result<T>);
        }

        public override int GetHashCode() => IsOK ? (IsOK, Value).GetHashCode() : (IsOK, Err).GetHashCode();

        public static bool operator ==(Result<T> first, Result<T> second)
        {
            if (first is null) return (second is null);

            return first.Equals(second);
        }

        public static bool operator !=(Result<T> first, Result<T> second)
        {
            if (first is null) return !(second is null);

            return !first.Equals(second);
        }

        #endregion
    }

    public partial class Result : IResult
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

        #region Equality boilerplate

        public bool Equals(Result other)
        {
            if (other is null) return false;

            if (this.IsOK && other.IsOK) return true;

            if (!this.IsOK && !other.IsOK)
            {
                if(this.Err is null)
                {
                    return other.Err is null;
                }
                else
                {
                    return this.Err.Equals(other.Err);
                }
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            return this.Equals(obj as Result);
        }

        public override int GetHashCode() => IsOK ? (IsOK).GetHashCode() : (IsOK, Err).GetHashCode();

        public static bool operator ==(Result first, Result second)
        {
            if (first is null) return (second is null);

            return first.Equals(second);
        }

        public static bool operator !=(Result first, Result second)
        {
            if (first is null) return !(second is null);

            return !first.Equals(second);
        }

        #endregion
    }
}

#nullable enable
using System.Diagnostics.CodeAnalysis;

namespace Celeste.Mods.Tas.Query
{
    /// Represents a void result value which might not be available, due to errors
    public readonly record struct VoidResult<TError>(TError? Error) where TError : notnull
    {
        public static readonly VoidResult<TError> Ok = new(default);
        public static VoidResult<TError> Fail(TError error) => new(error);

        public static implicit operator TError(VoidResult<TError> result) => result.Error!;

        /// Checks if the operation was successfully
        [MemberNotNullWhen(false, nameof(Error))]
        public bool Success => Error == null;

        /// Checks if the operations has failed
        [MemberNotNullWhen(true, nameof(Error))]
        public bool Failure => Error != null;

        /// Provides the result error if the operation has failed
        public bool CheckFailure([NotNullWhen(true)] out TError? error)
        {
            error = Error;
            return Failure;
        }

        public static VoidResult<T> AggregateError<T>(VoidResult<T> lhs, VoidResult<T> rhs) where T : notnull
        {
            if (lhs.Error == null && rhs.Error == null)
            {
                return VoidResult<T>.Ok;
            }

            if (lhs.Error != null && rhs.Error == null)
            {
                return lhs;
            }

            if (lhs.Error == null && rhs.Error != null)
            {
                return rhs;
            }

            if (typeof(T) == typeof(string))
            {
                return VoidResult<T>.Fail((T)(object)$"{lhs.Error}\n{rhs.Error}");
            }

            return lhs; // Drop rhs error since it can't be properly accumulated
        }
    }
}

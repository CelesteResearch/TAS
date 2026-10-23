#nullable enable
using System.Diagnostics.CodeAnalysis;

namespace Celeste.Mods.Tas.Query
{
    /// Represents a result value which might not be available, due to errors
    public readonly record struct Result<TValue, TError>(TValue Value, TError? Error) where TError : notnull
    {
        public static Result<TValue, TError> Ok(TValue value) => new(value, default);
        public static Result<TValue, TError> Fail(TError error) => new(default!, error);

        public static implicit operator TValue(Result<TValue, TError> result) => result.Value;
        public static implicit operator TError(Result<TValue, TError> result) => result.Error!;

        /// Checks if the operation was successfully
        [MemberNotNullWhen(false, nameof(Error))]
        public bool Success => Error == null;

        /// Checks if the operations has failed
        [MemberNotNullWhen(true, nameof(Error))]
        public bool Failure => Error != null;

        /// Provides the result value if the operation was successful
        public bool CheckSuccess([NotNullWhen(true)] out TValue? value)
        {
            value = Value;
            return Success;
        }

        /// Provides the result error if the operation has failed
        public bool CheckFailure([NotNullWhen(true)] out TError? error)
        {
            error = Error;
            return Failure;
        }
    }
}

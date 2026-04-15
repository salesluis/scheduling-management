using System;
using System.Collections.Generic;
using System.Linq;

namespace scheduling_management.Application.Common;

public enum ResultErrorType
{
    Validation = 1,
    NotFound = 2,
    Conflict = 3,
    Forbidden = 4,
    Unauthorized = 5,
    Unexpected = 6
}

public sealed record ResultError(string Code, string Message, ResultErrorType Type = ResultErrorType.Validation);

public sealed class Result<T>
{
    private Result(bool success, T? value, IReadOnlyList<ResultError> errors)
    {
        Success = success;
        Value = value;
        Errors = errors;
    }

    public bool Success { get; }
    public bool Failure => !Success;

    public T? Value { get; }
    public IReadOnlyList<ResultError> Errors { get; }

    public static Result<T> Ok(T value) => new(true, value, Array.Empty<ResultError>());

    public static Result<T> Fail(params ResultError[] errors) =>
        new(false, default, Normalize(errors));

    public static Result<T> Fail(IEnumerable<ResultError> errors) =>
        new(false, default, Normalize(errors?.ToArray() ?? Array.Empty<ResultError>()));

    public static Result<T> Fail(string code, string message, ResultErrorType type = ResultErrorType.Validation) =>
        new(false, default, new[] { new ResultError(code, message, type) });

    private static IReadOnlyList<ResultError> Normalize(IReadOnlyList<ResultError> errors)
    {
        if (errors.Count == 0)
            return new[] { new ResultError("UNEXPECTED", "An unexpected error occurred.", ResultErrorType.Unexpected) };

        return errors.Where(e => e is not null).ToArray();
    }
}

public readonly record struct Unit
{
    public static readonly Unit Value = new();
}


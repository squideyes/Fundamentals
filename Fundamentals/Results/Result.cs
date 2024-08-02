// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.Empty)
            throw new InvalidOperationException();

        if (!isSuccess && error == Error.Empty)
            throw new InvalidOperationException();

        IsSuccess = isSuccess;

        Errors = IsSuccess ? [] : [error];
    }

    protected internal Result(bool isSuccess, Error[] errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error[] Errors { get; }

    public static Result Success() => new(true, Error.Empty);

    public static Result<T> Success<T>(T value) =>
        new(value, true, Error.Empty);

    public static Result Failure(Error error) =>
        new(false, error);

    public static Result Failure(Error[] errors) =>
        new(false, errors);

    public static Result<T> Failure<T>(Error error) =>
        new(default, false, error);

    public static Result<T> Failure<T>(Error[] errors) =>
        new(default, false, errors);

    public static Result<T> Create<T>(T? value) =>
        value is not null ? Success(value) : Failure<T>(Error.NullValue);

    public static Result<T> Ensure<T>(T value, Func<T, bool> predicate, Error error) =>
        predicate(value) ? Success(value) : Failure<T>(error);

    public static Result<T> Ensure<T>(T value,
        params (Func<T, bool> predicate, Error error)[] functions)
    {
        var results = new List<Result<T>>();

        foreach ((Func<T, bool> predicate, Error error) in functions)
            results.Add(Ensure(value, predicate, error));

        return Combine(results.ToArray());
    }

    public static Result<T> Combine<T>(params Result<T>[] results)
    {
        if (results.Any(r => r.IsFailure))
        {
            return Failure<T>(results
                .SelectMany(r => r.Errors)
                .Where(e => e != Error.Empty)
                .Distinct()
                .ToArray());
        }

        return Success(results[0].Value);
    }

    public static Result<(T1, T2)> Combine<T1, T2>(
        Result<T1> result1, Result<T2> result2)
    {
        if (result1.IsFailure)
            return Failure<(T1, T2)>(result1.Errors);

        if (result2.IsFailure)
            return Failure<(T1, T2)>(result2.Errors);

        return Success((result1.Value, result2.Value));
    }
}
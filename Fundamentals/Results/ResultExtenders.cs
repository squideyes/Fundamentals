// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public static class ResultExtenders
{
    public static Result<T> AddFromResult<T>(this List<Error> errors, Result<T> result)
    {
        if (result.IsFailure)
            errors.AddRange(result.Errors);

        return result;
    }

    public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, Error error)
    {
        if (result.IsFailure)
            return result;

        return predicate(result.Value) ? result : Result.Failure<T>(error);
    }

    public static Result<O> Map<I, O>(this Result<I> result, Func<I, O> func)
    {
        return result.IsSuccess ? Result.Success(func(result.Value))
            : Result.Failure<O>(result.Errors);
    }

    public static Result<O> Bind<I, O>(this Result<I> result, Func<I, Result<O>> func)
    {
        if (result.IsFailure)
            return Result.Failure<O>(result.Errors);

        return func(result.Value);
    }

    public static async Task<Result> Bind<I>(this Result<I> result, Func<I, Task<Result>> func)
    {
        if (result.IsFailure)
            return Result.Failure(result.Errors);

        return await func(result.Value);
    }

    public static async Task<Result<O>> Bind<I, O>(this Result<I> result, Func<I, Task<Result<O>>> func)
    {
        if (result.IsFailure)
            return Result.Failure<O>(result.Errors);

        return await func(result.Value);
    }

    public static Result<I> Tap<I>(this Result<I> result, Action<I> action)
    {
        if (result.IsSuccess)
            action(result.Value);

        return result;
    }

    public static async Task<Result<I>> Tap<I>(this Result<I> result, Func<Task> func)
    {
        if (result.IsSuccess)
            await func();

        return result;
    }

    public static async Task<Result<I>> Tap<I>(this Task<Result<I>> resultTask, Func<I, Task> func)
    {
        var result = await resultTask;

        if (result.IsSuccess)
            await func(result.Value);

        return result;
    }
}
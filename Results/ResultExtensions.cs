using LH.Results.Errors;

namespace LH.Results;

public static class ResultExtensions
{
    public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, Error error)
    {
        if (result.IsSuccess)
        {
            if (predicate(result.Value))
            {
                return result;
            }
            return Result.Failure<T>(error);
        }

        return result;
    }

    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> func)
    {
        if (result.IsSuccess)
        {
            return func(result.Value);
        }

        return Result.Failure<TOut>(result.Error);
    }

    public static async Task<Result> Bind<TIn>(this Result<TIn> result, Func<TIn, Task<Result>> func)
    {
        if (result.IsSuccess)
        {
            return await func(result.Value);
        }
        return result;
    }


    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Task<Result<TOut>>> func)
    {
        if (result.IsSuccess)
        {
            return await func(result.Value);
        }
        return Result.Failure<TOut>(result.Error);
    }

    public static async Task<T> Match<T>(this Task<Result> resultTask, Func<T> successCallback, Func<Error, T> failureCallback)
    {
        var result = await resultTask;

        return result.IsSuccess ? successCallback() : failureCallback(result.Error);
    }

    public static async Task<TOut> Match<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, TOut> successCallback, Func<Error, TOut> failureCallback)
    {
        var result = await resultTask;

        return result.IsSuccess ? successCallback(result.Value) : failureCallback(result.Error);
    }
}
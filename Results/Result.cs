using LH.Results.Errors;

namespace LH.Results;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException("Not empty error with success result");
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException("Empty error with failure result");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
    public static Result<TValue> Create<TValue>(TValue value, Error error) => value is null ? Failure<TValue>(error) : Success(value);
    public static Result Failure(Error error) => new(false, error);
    public static Result<TValue> Failure<TValue>(Error error) => new(default!, false, error);
    public static Result FirstFailureOrSuccess(params Result[] results)
    {
        foreach (var result in results)
        {
            if (result.IsFailure)
            {
                return result;
            }
        }

        return Success();
    }
}
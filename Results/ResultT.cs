using LH.Results.Errors;

namespace LH.Results;


public class Result<TValue> : Result
{
    private readonly TValue _value;

    protected internal Result(TValue value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public static implicit operator Result<TValue>(TValue value) => Success(value);

    public TValue Value => IsSuccess ? _value : throw new InvalidOperationException("Value is not available for failure result");
}
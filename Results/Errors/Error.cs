
namespace LH.Results.Errors;

public class Error : IEquatable<Error>
{
    public string Code { get; }
    public string Message { get; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    private IEnumerable<object> GetAtomicValues()
    {
        yield return Code;
        yield return Message;
    }

    public static Error None => new(string.Empty, string.Empty);

    public bool Equals(Error? other)
    {
        return other is not null && GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        if (obj is not Error error)
        {
            return false;
        }

        return GetAtomicValues().SequenceEqual(error.GetAtomicValues());
    }

    public static bool operator ==(Error? a, Error? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Error? a, Error? b)
    {
        return !(a == b);
    }

    public override string ToString()
    {
        return $"[Code: {Code}]: {Message}";
    }

    public override int GetHashCode()
    {
        HashCode hashCode = default;

        foreach (var obj in GetAtomicValues())
        {
            hashCode.Add(obj);
        }

        return hashCode.ToHashCode();
    }
}
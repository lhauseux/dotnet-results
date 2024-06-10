namespace LH.Results.Errors;

public static class ExceptionErrors
{
    public static Error Unhandled(string exceptionMessage) => new("Unhandled.Exception", $"Unhandled exception {exceptionMessage}");
}
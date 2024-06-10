
using LH.Results.Errors;

namespace LH.Results.UnitTests.Fakes.Errors;

public static class FakeError
{
    public static Error Error => new("Error.code", "Error.message");
}
using LH.Results.Errors;
using LH.Results;
using LH.Results.UnitTests.Fakes.Errors;

namespace LH.Results.UnitTests;

public class UnitTest1
{
    [Fact]
    public void Create_Successful_Ok()
    {
        var exception = Record.Exception(Result.Success);
        Assert.Null(exception);
    }


    [Fact]
    public void Create_Failure_Ok()
    {
        var exception = Record.Exception(() => Result.Failure(FakeError.Error));
        Assert.Null(exception);
    }

    [Fact]
    public void Create_Failure_Exception()
    {
        var exception = Record.Exception(() => Result.Failure(Error.None));
        Assert.NotNull(exception);
    }
}
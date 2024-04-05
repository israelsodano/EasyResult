using Xunit;

namespace EResult.UnitTests;

public class ResultTests
{
    [Fact]
    public void Should_Create_Result_Success_And_Failled()
    {
        Result r1 = null;
        Assert.True(r1.Success);

        Result r2 = "MUST_BE_AN_ERROR_RESULT";
        Assert.False(r2.Success);

         Result r3 = ("MUST_BE_AN_ERROR_RESULT", "SOME DESCRIPTION");
        Assert.False(r3.Success);
    }

    [Fact]
    public void Should_Create_Resul_Value_Success_And_Failled()
    {
        int br = 0;
        Result<int> r1 = br;
        Assert.True(r1.Success);

        var r = (int)r1;
        Assert.Equal(r, br);

        Result<int> r2 = "MUST_BE_AN_ERROR_RESULT";
        Assert.False(r2.Success);

        Result<string> r3 = new("SUCCESS_VALUE_STRING");
        Assert.True(r3.Success);

        Result<int> r4 = ("MUST_BE_AN_ERROR_RESULT", "SOME DESCRIPTION");
        Assert.False(r4.Success);
        
        Assert.Throws<ResultException>(() => r4 + 10);
    }
}
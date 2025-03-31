using Xunit;
using SecurePaste.Helpers;

namespace SecurePaste.Test.Helpers;

public class RandomDataProviderTests
{

    public RandomDataProviderTests()
    {
    }

    [Fact]
    public void GenerateRandomString_ShouldReturnCorrectLength()
    {
        int length = 10;

        var result = RandomDataProvider.GenerateRandomString(length);

        Assert.Equal(length, result.Length);
    }

    [Fact]
    public void GenerateRandomString_ShouldReturnRandomStrings()
    {
        int length = 10;
        var string1 = RandomDataProvider.GenerateRandomString(length);
        var string2 = RandomDataProvider.GenerateRandomString(length);

        // Ensure the strings are different
        Assert.NotEqual(string1, string2);
    }

    [Fact]
    public void GenerateRandomString_ShouldContainValidCharacters()
    {
        int length = 10;
        var result = RandomDataProvider.GenerateRandomString(length);

        foreach (char c in result)
        {
            Assert.Contains(c, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");
        }
    }
}

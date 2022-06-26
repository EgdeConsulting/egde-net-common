namespace Egde.Common.Cryptography.Tests;

public class HashingTests
{
    [Fact]
    public void Hashing_With_Salt_Two_Times_Equal()
    {
        var inputString = "sda1932jfwlefasd23qf";
        var salt = "$2a$11$xRQIrLehMJU2PILbKu1T.u";
        Dictionary<string, string>? appSettings = new() {{"Hashing:Salt", $"{salt}"}};
        var hasher = new Hasher(HashTestUtils.GetConfigurationMock(false, appSettings).Object);

        var result1 = hasher.GetSaltedHash(inputString);
        var result2 = hasher.GetSaltedHash(inputString);
        Assert.Equal(result1, result2);
    }

    [Fact]
    public void Hashing_With_Salt_Two_Times_Equal_With_Mock()
    {
        var inputString = "sda1932jfwlefasd23qf";
        var hasher = new Hasher(HashTestUtils.GetConfigurationMock(true).Object);

        var result1 = hasher.GetSaltedHash(inputString);
        var result2 = hasher.GetSaltedHash(inputString);
        Assert.Equal(result1, result2);
    }
}

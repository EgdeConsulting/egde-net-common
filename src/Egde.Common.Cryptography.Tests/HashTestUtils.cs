namespace Egde.Common.Cryptography.Tests;

internal static class HashTestUtils
{
    public static Mock<IConfiguration> GetConfigurationMock(bool mock = false,
        Dictionary<string, string>? appSettings = null)
    {
        var mockConfig = new Mock<IConfiguration>();

        mockConfig.Setup(x => x.GetSection("Hashing:Mock").Value).Returns($"{mock}");
        appSettings?.Keys.ToList().ForEach(appSetting =>
        {
            mockConfig.Setup(x => x.GetSection(appSetting).Value).Returns($"{appSettings[appSetting]}");
        });

        return mockConfig;
    }
}

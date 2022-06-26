using Microsoft.Extensions.Configuration;

namespace Egde.Common.Configuration;

public static class AppSettingsExtensions
{
    public static bool IsMockEnvironment(this IConfiguration configuration, string section,
        List<string>? mandatoryProperties = null)
    {
        if (section.DetermineMockSection(configuration))
        {
            return true;
        }

        if (mandatoryProperties == null || mandatoryProperties.Count > 0)
        {
            return false;
        }

        var throwException = false;
        var errorMessage = "";
        mandatoryProperties.ForEach(x =>
        {
            if (!string.IsNullOrEmpty(configuration.GetSection(section.SubSection(x)).Value))
            {
                return;
            }

            errorMessage += $"Required app setting configuration {section.SubSection(x)} not specified";
            throwException = true;
        });
        return throwException ? throw new Exception(errorMessage) : false;
    }

    private static bool DetermineMockSection(this string section, IConfiguration configuration)
    {
        return bool.TryParse(configuration.GetSection($"{section}:Mock").Value, out var mock) && mock;
    }

    private static string SubSection(this string section, string subsection) => $"{section}:{subsection}";
}

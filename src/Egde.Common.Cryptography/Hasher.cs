using Egde.Common.Configuration;
using Microsoft.Extensions.Configuration;

namespace Egde.Common.Cryptography;

public class Hasher : IHasher
{
    private readonly string _configurationSection = "Hashing";
    private readonly List<string> _mandatoryConfigurationValues = new() {"Salt"};
    private readonly bool _mock;

    private readonly string? _salt;

    public Hasher(IConfiguration configuration)
    {
        _mock = configuration.IsMockEnvironment(_configurationSection, _mandatoryConfigurationValues);
        _salt = _mock switch
        {
            false => configuration.GetSection("Hashing:Salt").Value,
            _ => _salt
        };
    }

    public string GetSaltedHash(string input) => _mock
        ? input
        : BCrypt.Net.BCrypt.HashPassword(input, _salt);
}

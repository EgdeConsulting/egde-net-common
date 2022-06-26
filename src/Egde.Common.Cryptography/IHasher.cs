namespace Egde.Common.Cryptography;

public interface IHasher
{
    string GetSaltedHash(string input);
}

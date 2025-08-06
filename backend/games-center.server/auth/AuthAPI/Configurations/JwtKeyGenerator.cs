using System.Security.Cryptography;

namespace AuthAPI.Configurations;

public static class JwtKeyGenerator
{
    public static string GenerateSecureKey(int length = 64)
    {
        var key = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(key);
        return Convert.ToBase64String(key);
    }
}
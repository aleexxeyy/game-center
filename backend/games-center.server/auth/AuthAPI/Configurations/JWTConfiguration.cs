using System.Security.Cryptography;

namespace AuthAPI.Configurations;

public class JWTConfiguration
{
    public string Secret { get; set; } = GenerateSecureKey(); // дефолт
    public TimeSpan TokenLifetime { get; set; } = TimeSpan.FromMinutes(30);
    public string Issuer { get; set; } = "AuthAPI";
    public string Audience { get; set; } = "AuthAPIUsers";
    public bool AlwaysGenerateNewToken { get; set; } = true;
    public List<string> AllowedRoles { get; set; } = new();

    public static string GenerateSecureKey(int length = 64)
    {
        var key = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(key);
        return Convert.ToBase64String(key); // возвращаем как Base64
    }
}
// AuthAPI/Configurations/JwtSettings.cs
namespace AuthAPI.Configurations;

public class JwtSettings
{
    public string Secret { get; set; } = JwtKeyGenerator.GenerateSecureKey();
    public TimeSpan TokenLifetime { get; set; } = TimeSpan.FromMinutes(30);
    public string Issuer { get; set; } = "AuthAPI";
    public string Audience { get; set; } = "AuthAPIUsers";
}   
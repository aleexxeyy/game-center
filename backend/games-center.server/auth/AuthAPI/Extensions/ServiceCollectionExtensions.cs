using AuthAPI.Configurations;
using AuthAPI.Services;

namespace AuthAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
    }
}
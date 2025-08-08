using AuthAPI.Configurations;
using AuthAPI.Data;
using AuthAPI.Middlewares;
using Microsoft.AspNetCore.Identity;

namespace AuthAPI.Extensions;

public static class AuthBuilderExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        // Инициализация ролей и разрешений...
    }

    public static void ConfigureMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
using System.Security.Claims;

namespace AuthAPI.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuthMiddleware> _logger;

    public AuthMiddleware(RequestDelegate next, ILogger<AuthMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Логируем только факт аутентификации
        if (context.User.Identity?.IsAuthenticated == true)
        {
            _logger.LogInformation("Authenticated user: {UserId}", 
                context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
        
        await _next(context);
    }
}
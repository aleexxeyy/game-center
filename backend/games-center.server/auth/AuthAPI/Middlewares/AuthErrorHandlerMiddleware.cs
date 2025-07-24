namespace AuthAPI.Middlewares;

public class AuthErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuthErrorHandlerMiddleware> _logger;

    public AuthErrorHandlerMiddleware(RequestDelegate next, ILogger<AuthErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access.");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Unauthorized",
                message = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "ServerError",
                message = ex.Message
            });
        }
    }
}
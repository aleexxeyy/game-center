using Auth.API.DataAccess;
using Auth.API.Models;
using Auth.API.Repositories;
using Auth.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using DotNetEnv;
using Auth.API.Middlewares;
Env.Load(); 

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Auth API",
        Version = "v1",
        Description = "API for authentication or register users"
    });
});

// PostgreSQL
var connectionString =
    $"Host={Environment.GetEnvironmentVariable("POSTGRES_HOST")};" +
    $"Port={Environment.GetEnvironmentVariable("POSTGRES_PORT")};" +
    $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};" +
    $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
    $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};";

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(connectionString));

// Dependency Injection
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

var app = builder.Build();

// Сonnected Swagger only in Dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth API v1");
        options.RoutePrefix = string.Empty;
    });
    app.UseDeveloperExceptionPage();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseMiddleware<ExceptionHandlingMiddleware>();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();

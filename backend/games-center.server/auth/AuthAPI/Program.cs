using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using AuthAPI.Data;
using AuthAPI.Middlewares;
using AuthAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Загружаем переменные окружения из .env
Env.Load();

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException("CONNECTION_STRING is not set in environment variables.");

// Настройка JWT-конфигурации
var jwtSection = builder.Configuration.GetSection("JWT");
var jwtConfig = jwtSection.Get<JWTConfiguration>() ?? new JWTConfiguration();

// Если переменная окружения установлена — переопределяем ключ
var jwtSecretFromEnv = Environment.GetEnvironmentVariable("JWT_SECRET");
if (!string.IsNullOrWhiteSpace(jwtSecretFromEnv))
{
    jwtConfig.Secret = jwtSecretFromEnv;
}

// Генерируем ключ, если всё ещё пусто
if (string.IsNullOrWhiteSpace(jwtConfig.Secret))
{
    jwtConfig.Secret = JWTConfiguration.GenerateSecureKey();
}

// Регистрируем JWT-конфигурацию в DI
builder.Services.Configure<JWTConfiguration>(options =>
{
    options.Secret = jwtConfig.Secret;
    options.TokenLifetime = jwtConfig.TokenLifetime;
    options.Issuer = jwtConfig.Issuer;
    options.Audience = jwtConfig.Audience;
    options.AlwaysGenerateNewToken = jwtConfig.AlwaysGenerateNewToken;
    options.AllowedRoles = jwtConfig.AllowedRoles;
});

// Настройка базы данных и Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// JWT аутентификация
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig.Issuer,
        ValidAudience = jwtConfig.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret))
    };
});

// Swagger + JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Введите JWT токен: Bearer {your token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Swagger в Dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Пользовательские middleware
app.UseMiddleware<AuthErrorHandlerMiddleware>();
app.UseMiddleware<AuthMiddleware>();

// Аутентификация/авторизация
app.UseAuthentication();
app.UseAuthorization();

// Маршруты
app.MapControllers();

app.Run();

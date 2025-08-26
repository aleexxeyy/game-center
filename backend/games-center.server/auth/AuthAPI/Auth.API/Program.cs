using Auth.API.Configurations;
using Auth.API.Extensions;
using Auth.Infrastructure.Configurations;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Загрузка .env
Env.Load();

// Конфигурация сервисов
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
                       ?? throw new InvalidOperationException("CONNECTION_STRING is not set");
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET")
                ?? JwtKeyGenerator.GenerateSecureKey();

builder.Services.ConfigureApplicationServices(builder.Configuration);
builder.Services.ConfigureDatabase(connectionString);
builder.Services.ConfigureJwtAuthentication(jwtSecret, "AuthAPI", "AuthAPIUsers");
builder.Services.AddConfiguredSwagger();
builder.Services.AddControllers();

var app = builder.Build();

// Инициализация и middleware
//await app.InitializeDatabaseAsync();
app.ConfigureMiddlewares();
app.MapControllers();

app.Run();
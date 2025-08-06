using AuthAPI.Configurations;
using AuthAPI.Extensions;
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
builder.Services.ConfigureSwagger();
builder.Services.AddControllers();

var app = builder.Build();

// Инициализация и middleware
await app.InitializeDatabaseAsync();
app.ConfigureMiddlewares();
app.MapControllers();

app.Run();
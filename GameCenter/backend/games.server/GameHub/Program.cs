using Microsoft.EntityFrameworkCore;
using GameHub.Services;
using GameHub.Hubs;
using GameHub.DataBaseContext;
using GameHub.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GameHubDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IGameHubRepository, GameHubRepository>();
builder.Services.AddScoped<IGameHubsService, GameHubsService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameHub API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseRouting();
app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<GamesHub>("/hub");
});

app.Run();
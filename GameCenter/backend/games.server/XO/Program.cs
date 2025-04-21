using XO.Services;
using GameHub.DataBaseContext;
using GameHub.Hubs;
using GameHub.Repositories;
using GameHub.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GameHubDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IGameHubRepository, GameHubRepository>();
builder.Services.AddScoped<IGameHubsService>(provider =>
{
    var repository = provider.GetRequiredService<IGameHubRepository>();
    return new GameHubsService(repository);
});


builder.Services.AddScoped<IXOService, XOService>();

builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Game API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseRouting();
app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
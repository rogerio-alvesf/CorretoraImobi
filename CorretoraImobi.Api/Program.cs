using CorretoraImobi.Api.Config;
using CorretoraImobi.Application.Services;
using CorretoraImobi.Domain.Interfaces.Repositories;
using CorretoraImobi.Domain.Interfaces.Services;
using CorretoraImobi.Infrastructure.Data;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Adiciona arquivos de configuração
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

// Adicionar o logging para console
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// Configurar MongoDB com logging
builder.Services.AddSingleton<IMongoClient>(s =>
{
    var settings = MongoClientSettings.FromConnectionString(builder.Configuration.GetConnectionString("DefaultMongoDbConnection"));

    // Configurar logs para exibir operações do MongoDB no console
    settings.ClusterConfigurator = cb =>
    {
        var loggerFactory = s.GetRequiredService<ILoggerFactory>();
        cb.Subscribe<MongoDB.Driver.Core.Events.CommandStartedEvent>(e => 
            loggerFactory.CreateLogger("MongoDB.Command").LogInformation($"MongoDB Command Started: {e.CommandName} - {e.Command.ToJson()}")
        );
    };

    return new MongoClient(settings);
});

// Injeta a implementação do repositório na camada de domínio
builder.Services.AddScoped<IImovelRepository, MongoDbImovelRepository>();
builder.Services.AddScoped<IImovelService, ImovelService>();
builder.Services.AddScoped<ILazerImovelRepository, MongoDbLazerImovelRepository>();
builder.Services.AddScoped<ILazerImovelService, LazerImovelService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "CorretoraImobi.Api", Version = "v1" }));

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

ConfigureEndpoints.AddEndpoints(app);
app.UseHttpsRedirection();

app.Run();

public partial class Program
{
}
using ARS.Infrastructure;
using ARS.Application.Services;
using ARS.Application.Interfaces;
using ARS.Infrastructure.Repositories;
using Serilog;
using System.Diagnostics;
using ARS.Infrastructure.Dapper;
using ARS.Infrastructure.Dapper;
using ARS.Application.Services;
using ARS.Application;
using ARS.API.Middleware;
var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddHealthChecks();
builder.Services.AddInfrastructureDatabase(builder.Configuration);
builder.Services.AddInfrastructureHealthChecks(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddSingleton(builder.Configuration);
// Add controllers
builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddOpenApi();
//builder.WebHost.UseUrls("http://localhost:6000");

// Configure Serilog as the logging provider
builder.Host.UseSerilog((context, services, config) =>
{
    config.ReadFrom.Configuration(context.Configuration)
          .ReadFrom.Services(services)
          .Enrich.FromLogContext();
});

// API project - Program.cs
Console.WriteLine("Step 2");
var app = builder.Build();
app.UseMiddleware<CorrelationIdMiddleware>();
Console.WriteLine("Step 3");
// This endpoint will always return healthy, indicating that the application is running.
/*app.MapHealthChecks("/health/live", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => false
});*/
// This endpoint will check the health of the infrastructure, including the database connection.
///*app.MapHealthChecks("/health/ready");

Console.WriteLine("Step 6");
// Optional: Log all HTTP requests automatically
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.MapControllers();
Console.WriteLine("Step 7");

app.Run();

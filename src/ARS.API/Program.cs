using ARS.Infrastructure;
using ARS.Application.Services;
using ARS.Application.Interfaces;
using ARS.Infrastructure.Repositories;
using Serilog;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Register services
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
// Configure Serilog as the logging provider
builder.Host.UseSerilog((context, services, config) =>
{
    config.ReadFrom.Configuration(context.Configuration)
          .ReadFrom.Services(services)
          .Enrich.FromLogContext();
});


// API project - Program.cs
builder.Services.AddInfrastructureHealthChecks(builder.Configuration);

var app = builder.Build();


// This endpoint will always return healthy, indicating that the application is running.
app.MapHealthChecks("/health/live", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => false
});

// This endpoint will check the health of the infrastructure, including the database connection.
app.MapHealthChecks("/health/ready");
app.Use(async (context, next) =>
{
    var correlationId =
        context.Request.Headers["X-Correlation-ID"].FirstOrDefault()
        ?? Activity.Current?.Id
        ?? context.TraceIdentifier;

    // Add to response
    context.Response.Headers["X-Correlation-ID"] = correlationId;

    // Push into Serilog context
    using (Serilog.Context.LogContext.PushProperty("CorrelationId", correlationId))
    {
        await next();
    }
});

// Optional: Log all HTTP requests automatically
app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();
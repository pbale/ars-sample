using ARS.Infrastructure;
using ARS.Application.Services;
using ARS.Application.Interfaces;
using ARS.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Register services
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

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

app.MapControllers();

app.Run();
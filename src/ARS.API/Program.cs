using ARS.Application.Services;
using ARS.Application.Interfaces;
using ARS.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Register services
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();
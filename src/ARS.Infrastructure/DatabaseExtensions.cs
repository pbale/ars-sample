// Infrastructure project
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ARS.Infrastructure.Dapper;
using ARS.Application.Interfaces;
namespace ARS.Infrastructure;
using ARS.Infrastructure.Repositories;  

public static class DatabaseExtensions
{
    public static IServiceCollection AddInfrastructureDatabase(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
    services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        return services;
    }
}

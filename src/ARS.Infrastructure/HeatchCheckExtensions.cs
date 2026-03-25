// Infrastructure project
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ARS.Infrastructure;

/// <summary>
/// This class contains extension methods for adding health checks to the service collection.
/// </summary>
/// <remarks>
/// The AddInfrastructureHealthChecks method adds a health check for the SQL Server database connection using the connection string from the configuration.
/// </remarks> 
public static class HealthCheckExtensions
{
    /// <summary>
    /// Adds health checks to the service collection, including a check for the SQL Server database connection.
    /// </summary>
    /// <param name="services"></param>The dependency injection service collection to which the health checks will be added.
    /// <param name="config"></param>The application configuration from which the database connection string will be retrieved.
    /// <returns>A health check service collection indicating the status of the infrastructure checks.</returns>
    public static IServiceCollection AddInfrastructureHealthChecks(
        this IServiceCollection services,
        IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");

        Console.WriteLine($"Health Check - Connection String: {connectionString}");

        //Open a db cnnection to check if the database is reachable. 
        // return healthy if it is, unhealthy if it isn't.
        services.AddHealthChecks()
            .AddSqlServer(connectionString, name: "database");

        return services;
    }
}

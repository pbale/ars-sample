using Microsoft.Extensions.DependencyInjection;
using ARS.Application.Services;
using ARS.Application.Interfaces;

namespace ARS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();

        return services; //.AddScoped<EmployeeDeptService>();
    }
}
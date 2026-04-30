using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;   
using ARS.Application.Interfaces;
using ARS.Infrastructure.Dapper;

namespace ARS.Infrastructure.Dapper;
public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _configuration;
    public DbConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }
}
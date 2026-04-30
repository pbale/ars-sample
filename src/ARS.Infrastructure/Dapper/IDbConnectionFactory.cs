using System;
using System.Data;

namespace ARS.Infrastructure.Dapper;
public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
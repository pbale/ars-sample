using ARS.Application.Interfaces;
using ARS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ARS.Application.DTO;
using Dapper;  
using System.Data;
using ARS.Infrastructure.Dapper;
using ARS.Application.Exceptions;
using ARS.Application.DTO.Error;
using ARS.Application.Exceptions;
using ARS.Application.Interfaces;
using System.Net;

namespace ARS.Infrastructure.Repositories
{
    public class EmployeeRepository(IDbConnectionFactory connection) : IEmployeeRepository
    {
        private readonly IDbConnectionFactory _db=connection;

   
    public async Task<EmployeeDTO> GetEmployeeDepartments(string uin, int year)
    {
        using var connection = _db.CreateConnection();
        var default_year = 2026;
        string sql = $"""
          SELECT DISTINCT
            a.name,
            d.c_dept as unitcode,
            a.uin,
            d.deptname AS department,
            a.year
          FROM ARS.dbo.PAYACTVALL a,
            utility.dbo.ddd d
          WHERE (a.unitcode=d.campus+'-'+d.dept OR a.unitcode=d.unitcode)
            AND a.UIN ='{uin}' AND a.year={default_year}
          ORDER BY d.deptname
          """;

        EmployeeDTO employeeDept = await connection.QueryFirstOrDefaultAsync<EmployeeDTO>(sql, new { uin, default_year})
            ?? throw new NotFoundException(404, $"Employee with UIN {uin} and Year {default_year} not found.");
           

       
       return employeeDept;
        
    }
    }
}
using ARS.Application.Interfaces;
using ARS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ARS.Application.DTO;
namespace ARS.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public Task<EmployeeDTO> GetEmployeeDepartments(string uin, int year)
    {
        return _repository.GetEmployeeDepartments(uin, year);
    }

        
    }
}
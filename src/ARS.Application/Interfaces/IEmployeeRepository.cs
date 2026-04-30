using ARS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ARS.Application.DTO;
namespace ARS.Application.Interfaces;
    public interface IEmployeeRepository
    {
        //Task<IEnumerable<Employee>> GetAllAsync();
        Task<EmployeeDTO> GetEmployeeDepartments(string uin, int year);
        //Task<EmployeeDTO> GetEmployeeDepartments(string uin);
    }
using ARS.Application.DTO;
namespace ARS.Application.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeDTO> GetEmployeeDepartments(string uin, int year);
}
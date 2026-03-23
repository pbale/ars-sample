using ARS.Application.Interfaces;
using ARS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ARS.Application.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Employee>> GetEmployees()
        {
            return _repository.GetAllAsync();
        }
    }
}
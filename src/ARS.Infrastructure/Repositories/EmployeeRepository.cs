using ARS.Application.Interfaces;
using ARS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ARS.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetAllAsync()
        {
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Department = "IT" },
                new Employee { Id = 2, Name = "Jane Smith", Department = "HR" }
            };
            return Task.FromResult<IEnumerable<Employee>>(employees);
        }
    }
}
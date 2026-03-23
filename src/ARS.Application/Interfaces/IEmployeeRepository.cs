using ARS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ARS.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
    }
}
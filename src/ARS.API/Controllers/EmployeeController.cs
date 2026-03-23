using Microsoft.AspNetCore.Mvc;
using ARS.Application.Services;
using ARS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ARS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await _service.GetEmployees();
        }
    }
}
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
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeController(ILogger<EmployeeService> logger)
        {
            _logger = logger;
        }
        private readonly EmployeeService _service;

       /* public EmployeeController(EmployeeService service)
        {
            _service = service;
        }*/

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("Successfully fetched employees");

            return "There are 10 employees in the system.";
        }
    }
}
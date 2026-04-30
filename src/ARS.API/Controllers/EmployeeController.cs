using Microsoft.AspNetCore.Mvc;
using ARS.Application.Services;
using ARS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ARS.Application.DTO;
using ARS.Application.Interfaces;
namespace ARS.API.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController (ILogger<EmployeeController> logger, IEmployeeService employeeService) : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger = logger;
        private readonly IEmployeeService _employeeService = employeeService;

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("Successfully fetched employees");

            return "There are 10 employees in the system.";
        }
    
    
        [HttpGet("{uin}")]
        public async Task<ActionResult<object>> GetEmployeeByUIN(string uin, string? year)
        {
            Console.WriteLine($"Received request for UIN: {uin} and Year: {year}");
        //EmployeeDeptService employeeDeptService = new(new EmployeeDeptRepository());
            string default_year = "2026";
            Console.WriteLine($"Received request for UIN: {uin} and Year: {default_year}");
            EmployeeDTO employee = await _employeeService.GetEmployeeDepartments(uin, int.Parse(default_year));
            
    #pragma warning disable CA1848 // Use the LoggerMessage delegates
            _logger.LogInformation($"Successfully fetched employeeDept {uin}");
    #pragma warning restore CA1848 // Use the LoggerMessage delegates

            return Ok(employee);
        }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RazorMvc.Models;
using RazorMvc.Services;

namespace RazorMvc.Controllers
{
    [Route("employee/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbService employeeService;

        public EmployeeController(EmployeeDbService employeeService)
        {
            this.employeeService = employeeService;
        }

        // GET: employee/<EmployeeController>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return employeeService.GetEmployees();
        }

        // GET employee/<EmployeeController>/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return employeeService.GetEmployeeById(id);
        }

        // POST employee/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] Employee employee)
        {
            var newEmployee = employeeService.AddEmployee(employee);
        }
    }
}
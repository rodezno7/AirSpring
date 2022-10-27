using AirSpring.DAL.Interface;
using AirSpring.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AirSpring.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        // Construct
        private readonly IEmployee _employee;
        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }

        // Save new employee
        [HttpPost]
        public IActionResult Post([FromBody] NewEmployeeDTO newEmployee)
        {
            try
            {
                var result = _employee.CreateEmployee(newEmployee);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // Get all employees
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_employee.GetAllEmployees());
        }

        // Update Employee
        [HttpPut]
        public IActionResult Put([FromBody] EmployeeDTO employee)
        {
            try
            {
                if (employee.EmployeeId == 0)
                {
                    return BadRequest();
                }
                else
                {
                    var result = _employee.UpdateEmployee(employee);
                    return Ok(result);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // Get individual employee
        [HttpPost]
        [Route("GetEmployee")]
        public IActionResult GetEmployee([FromBody] int id)
        {
            return Ok(_employee.GetEmployeeById(id));
        }

        // Method to Delete Employees
        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(DeleteEmployeeDTO employee)
        {
            return Ok(_employee.DeleteEmployee(employee));
        }
    }
}

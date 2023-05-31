using APIfinish.Models;
using Microsoft.AspNetCore.Mvc;

namespace Generics_and_Collections.Controllers;


[ApiController]

[Route("[controller]/[action]")]
public class EmployeeController : ControllerBase
{
            private static List<Employee> employees = new List<Employee>()
        {
            new Employee { Id = 1, Name = "Mahmoud", Department = "HR" },
            new Employee { Id = 2, Name = "Ibrahim", Department = "IT" },
            new Employee { Id = 3, Name = "Ronaldo", Department = "Finance" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            return employees;
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpPost]
        public ActionResult<Employee> ADDEmployee(Employee employee)
        {
            try
            {
            employee.Id = employees.Count + 1;
            employees.Add(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
            }
            catch (Exception)
            {
                return  StatusCode(500, "An error occurred while creating the employee.");
            }   
        }
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, Employee employee)
        {
            var existingEmployee = employees.SingleOrDefault(e => e.Id == id);
            if (existingEmployee == null)
            {
                return NotFound();
            }
            existingEmployee.Name = employee.Name;
            existingEmployee.Department = employee.Department;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = employees.SingleOrDefault (e=> e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            employees.Remove(employee);
            return NoContent();
        }
}
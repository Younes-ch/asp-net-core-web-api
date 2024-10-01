using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.Employee;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController(IServiceManager service) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var employees = service.EmployeeService.GetEmployees(companyId, trackChanges: false);
            return Ok(employees);
        }

        [HttpGet("{employeeId:guid}", Name = "GetEmployeeById")]
        public IActionResult GetEmployee(Guid companyId, Guid employeeId)
        {
            var employee = service.EmployeeService.GetEmployee(companyId, employeeId, trackChanges: false);
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployee(Guid companyId, [FromBody] CreateEmployeeDto employee)
        {
            if (employee is null) return BadRequest("CreateEmployeeDto is null");

            var employeeToReturn = service.EmployeeService.CreateEmployee(companyId, employee, false);

            return CreatedAtRoute("GetEmployeeById", new { companyId, employeeId = employeeToReturn.Id }, employeeToReturn);
        }
    }

}

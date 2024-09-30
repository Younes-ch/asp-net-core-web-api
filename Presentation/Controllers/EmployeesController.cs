using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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

        [HttpGet("{employeeId:guid}")]
        public IActionResult GetEmployee(Guid companyId, Guid employeeId)
        {
            var employee = service.EmployeeService.GetEmployee(companyId, employeeId, trackChanges: false);
            return Ok(employee);
        }
    }

}

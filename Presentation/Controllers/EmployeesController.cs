using Microsoft.AspNetCore.JsonPatch;
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

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var employeeToReturn = service.EmployeeService.CreateEmployee(companyId, employee, false);

            return CreatedAtRoute("GetEmployeeById", new { companyId, employeeId = employeeToReturn.Id }, employeeToReturn);
        }

        [HttpPut("{employeeId:guid}")]
        public IActionResult UpdateEmployee(Guid companyId, Guid employeeId,
            [FromBody] UpdateEmployeeDto employeeForUpdate)
        {
            if (employeeForUpdate is null) return BadRequest("EmployeeForUpdateDto object is null");

            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            service.EmployeeService.UpdateEmployee(companyId, employeeId, employeeForUpdate, compTrackChanges: false, empTrackChanges: true);

            return NoContent();
        }

        [HttpPatch("{employeeId:guid}")]
        public IActionResult PartiallyUpdateEmployeeForCompany(Guid companyId, Guid employeeId,
            [FromBody] JsonPatchDocument<UpdateEmployeeDto> patchDocument)
        {
            if (patchDocument is null) return BadRequest("Patch document object sent from client is null");

            var result = service.EmployeeService.GetEmployeeForPatch(companyId, employeeId, compTrackChanges: false,
                empTrackChanges: true);

            patchDocument.ApplyTo(result.employeeToPatch, ModelState);

            TryValidateModel(result.employeeToPatch);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            service.EmployeeService.SaveChangesForPatch(result.employeeToPatch, result.employeeEntity);

            return NoContent();
        }

        [HttpDelete("{employeeId:guid}")]
        public IActionResult DeleteEmployee(Guid companyId, Guid employeeId)
        {
            service.EmployeeService.DeleteEmployee(companyId, employeeId, false);

            return NoContent();
        }

    }

}

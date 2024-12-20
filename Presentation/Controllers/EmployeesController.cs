﻿using System.Text.Json;
using CompanyEmployees.Presentation.ActionFilters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.Employee;
using Shared.RequestFeatures;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/companies/{companyId}/employees")]
[ApiController]
public class EmployeesController(IServiceManager service) : ControllerBase
{
    [HttpGet]
    [HttpHead]
    public async Task<IActionResult> GetEmployeesForCompany(Guid companyId,
        [FromQuery] EmployeeParameters employeeParameters)
    {
        var pagedResult =
            await service.EmployeeService.GetEmployeesAsync(companyId, employeeParameters, false);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.employees);
    }

    [HttpGet("{employeeId:guid}", Name = "GetEmployeeById")]
    public async Task<IActionResult> GetEmployee(Guid companyId, Guid employeeId,
        [FromQuery] EmployeeParameters employeeParameters)
    {
        var employee =
            await service.EmployeeService.GetEmployeeAsync(companyId, employeeId, false,
                employeeParameters);
        return Ok(employee);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateEmployee(Guid companyId, [FromBody] CreateEmployeeDto employee)
    {
        var employeeToReturn = await service.EmployeeService.CreateEmployeeAsync(companyId, employee, false);

        return CreatedAtRoute("GetEmployeeById", new { companyId, employeeId = employeeToReturn.Id },
            employeeToReturn);
    }

    [HttpPut("{employeeId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateEmployee(Guid companyId, Guid employeeId,
        [FromBody] UpdateEmployeeDto employeeForUpdate)
    {
        await service.EmployeeService.UpdateEmployeeAsync(companyId, employeeId, employeeForUpdate,
            false, true);

        return NoContent();
    }

    [HttpPatch("{employeeId:guid}")]
    public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(Guid companyId, Guid employeeId,
        [FromBody] JsonPatchDocument<UpdateEmployeeDto> patchDocument)
    {
        if (patchDocument is null) return BadRequest("Patch document object sent from client is null");

        var result = await service.EmployeeService.GetEmployeeForPatchAsync(companyId, employeeId,
            false,
            true);

        patchDocument.ApplyTo(result.employeeToPatch, ModelState);

        TryValidateModel(result.employeeToPatch);

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await service.EmployeeService.SaveChangesForPatchAsync(result.employeeToPatch, result.employeeEntity);

        return NoContent();
    }

    [HttpDelete("{employeeId:guid}")]
    public async Task<IActionResult> DeleteEmployee(Guid companyId, Guid employeeId)
    {
        await service.EmployeeService.DeleteEmployeeAsync(companyId, employeeId, false);

        return NoContent();
    }
}
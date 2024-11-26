using CompanyEmployees.Presentation.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.User;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController(IServiceManager service) : ControllerBase
{
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
    {
        var result = await service.AuthenticationService.RegisterUser(userForRegistrationDto);

        if (result.Succeeded) return StatusCode(201);

        foreach (var error in result.Errors) ModelState.TryAddModelError(error.Code, error.Description);

        return BadRequest(ModelState);
    }
}
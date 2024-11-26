using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.RequestFeatures;

namespace CompanyEmployees.Presentation.Controllers;

[ApiVersion("2.0", Deprecated = true)]
[Route("api/companies")]
[ApiController]
public class CompaniesV2Controller(IServiceManager service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCompanies([FromQuery] CompanyParameters companyParameters)
    {
        var pagedResult = await service.CompanyService.GetAllCompaniesAsync(companyParameters, false);

        var companiesV2 = pagedResult.companies.Select(c => $"'{c.Name} V2");

        return Ok(companiesV2);
    }
}
using Shared.DataTransferObjects.Employee;

namespace Shared.DataTransferObjects.Company;

public record UpdateCompanyDto : ManipulateEmployeeDto
{
    public IEnumerable<CreateEmployeeDto>? Employees { get; init; }
}
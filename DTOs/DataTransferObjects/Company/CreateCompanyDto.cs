using Shared.DataTransferObjects.Employee;

namespace Shared.DataTransferObjects.Company
{
    public record CreateCompanyDto : ManipulateCompanyDto
    {
        public IEnumerable<CreateEmployeeDto>? Employees { get; init; }
    }
}

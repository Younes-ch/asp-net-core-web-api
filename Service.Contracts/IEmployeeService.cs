using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Employee;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges);
        EmployeeDto GetEmployee(Guid companyId, Guid employeeId, bool trackChanges);
        EmployeeDto CreateEmployee(Guid companyId, CreateEmployeeDto employee, bool trackChanges);
    }
}

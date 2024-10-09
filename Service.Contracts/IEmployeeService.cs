using Entities.Models;
using Shared.DataTransferObjects.Employee;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChanges);
        Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges);
        Task<EmployeeDto> CreateEmployeeAsync(Guid companyId, CreateEmployeeDto employee, bool trackChanges);
        Task DeleteEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges);
        Task UpdateEmployeeAsync(Guid companyId, Guid employeeId, UpdateEmployeeDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges);
        Task<(UpdateEmployeeDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(
            Guid companyId, Guid employeeId, bool compTrackChanges, bool empTrackChanges);
        Task SaveChangesForPatchAsync(UpdateEmployeeDto employeeToPatch, Employee
            employeeEntity);

    }
}

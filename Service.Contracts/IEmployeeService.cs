using System.Dynamic;
using Entities.Models;
using Shared.DataTransferObjects.Employee;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IEmployeeService
{
    Task<(IEnumerable<ExpandoObject> employees, MetaData metaData)> GetEmployeesAsync(Guid companyId,
        EmployeeParameters employeeParameters, bool trackChanges);

    Task<ExpandoObject> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges,
        EmployeeParameters employeeParameters);

    Task<EmployeeDto> CreateEmployeeAsync(Guid companyId, CreateEmployeeDto employee, bool trackChanges);
    Task DeleteEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges);

    Task UpdateEmployeeAsync(Guid companyId, Guid employeeId, UpdateEmployeeDto employeeForUpdate,
        bool compTrackChanges, bool empTrackChanges);

    Task<(UpdateEmployeeDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(
        Guid companyId, Guid employeeId, bool compTrackChanges, bool empTrackChanges);

    Task SaveChangesForPatchAsync(UpdateEmployeeDto employeeToPatch, Employee
        employeeEntity);
}
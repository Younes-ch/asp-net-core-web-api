using Entities.Models;
using Shared.DataTransferObjects.Employee;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges);
        EmployeeDto GetEmployee(Guid companyId, Guid employeeId, bool trackChanges);
        EmployeeDto CreateEmployee(Guid companyId, CreateEmployeeDto employee, bool trackChanges);
        void DeleteEmployee(Guid companyId, Guid employeeId, bool trackChanges);
        void UpdateEmployee(Guid companyId, Guid employeeId, UpdateEmployeeDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges);
        (UpdateEmployeeDto employeeToPatch, Employee employeeEntity) GetEmployeeForPatch(
            Guid companyId, Guid employeeId, bool compTrackChanges, bool empTrackChanges);
        void SaveChangesForPatch(UpdateEmployeeDto employeeToPatch, Employee
            employeeEntity);

    }
}

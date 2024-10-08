using Contracts;
using Entities.Models;

namespace Repository
{
    public class EmployeeRepository(RepositoryContext repositoryContext)
    : RepositoryBase<Employee>(repositoryContext), IEmployeeRepository
    {
        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges) =>
            FindByCondition(emp => emp.CompanyId.Equals(companyId), trackChanges)
                .OrderBy(emp => emp.Name)
                .ToList();

        public Employee GetEmployee(Guid companyId, Guid employeeId, bool trackChanges) =>
            FindByCondition(emp => emp.CompanyId.Equals(companyId) && emp.Id.Equals(employeeId), trackChanges)
                .SingleOrDefault();

        public void CreateEmployee(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee) => Delete(employee);
    }
}

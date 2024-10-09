using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class EmployeeRepository(RepositoryContext repositoryContext)
    : RepositoryBase<Employee>(repositoryContext), IEmployeeRepository
    {
        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChanges) =>
            await FindByCondition(emp => emp.CompanyId.Equals(companyId), trackChanges)
                .OrderBy(emp => emp.Name)
                .ToListAsync();

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges) =>
            await FindByCondition(emp => emp.CompanyId.Equals(companyId) && emp.Id.Equals(employeeId), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateEmployee(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee) => Delete(employee);
    }
}

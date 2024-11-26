using Contracts;

namespace Repository;

public class RepositoryManager(RepositoryContext repositoryContext) : IRepositoryManager
{
    private readonly Lazy<ICompanyRepository> _companyRepository = new(() => new CompanyRepository(repositoryContext));

    private readonly Lazy<IEmployeeRepository> _employeeRepository =
        new(() => new EmployeeRepository(repositoryContext));


    public ICompanyRepository CompanyRepository => _companyRepository.Value;
    public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

    public async Task SaveAsync()
    {
        await repositoryContext.SaveChangesAsync();
    }
}
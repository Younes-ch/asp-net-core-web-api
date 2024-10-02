using Contracts;
using Entities.Models;

namespace Repository
{
    public class CompanyRepository(RepositoryContext repositoryContext)
        : RepositoryBase<Company>(repositoryContext), ICompanyRepository
    {
        public IEnumerable<Company> GetAllCompanies(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(company => company.Name)
                .ToList();

        public Company GetCompany(Guid companyId, bool trackChanges) => 
            FindByCondition(c => c.Id.Equals(companyId), trackChanges)
                .SingleOrDefault();

        public IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            FindByCondition(c => ids.Contains(c.Id), trackChanges)
                .ToList();

        public void CreateCompany(Company company) => Create(company);
    }
}

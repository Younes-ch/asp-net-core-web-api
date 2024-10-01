using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Company;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
        CompanyDto GetCompany(Guid companyId, bool trackChanges);
        CompanyDto CreateCompany(CreateCompanyDto company);
    }
}

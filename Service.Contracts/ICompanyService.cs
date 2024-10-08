using Shared.DataTransferObjects.Company;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
        CompanyDto GetCompany(Guid companyId, bool trackChanges);
        IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        CompanyDto CreateCompany(CreateCompanyDto company);
        (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(
            IEnumerable<CreateCompanyDto> companyCollection);
        void UpdateCompany(Guid companyId, UpdateCompanyDto companyForUpdate, bool
            trackChanges);
        void DeleteCompany(Guid companyId, bool trackChanges);
    }
}

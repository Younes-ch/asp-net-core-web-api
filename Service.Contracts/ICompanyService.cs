using Shared.DataTransferObjects.Company;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges);
        Task<CompanyDto> GetCompanyAsync(Guid companyId, bool trackChanges);
        Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto company);
        Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync(
            IEnumerable<CreateCompanyDto> companyCollection);
        Task UpdateCompanyAsync(Guid companyId, UpdateCompanyDto companyForUpdate, bool
            trackChanges);
        Task DeleteCompanyAsync(Guid companyId, bool trackChanges);
    }
}

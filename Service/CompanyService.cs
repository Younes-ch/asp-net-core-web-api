using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Exceptions.BadRequestExceptions;
using Entities.Exceptions.NotFoundExceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Company;
using Shared.RequestFeatures;

namespace Service
{
    internal sealed class CompanyService(
        IRepositoryManager repository,
        ILoggerManager logger,
        IMapper mapper) : ICompanyService
    {
        public async Task<(IEnumerable<CompanyDto> companies, MetaData metaData)> GetAllCompaniesAsync(CompanyParameters companyParameters, bool trackChanges)
        {
            var companiesMetaData = await repository.CompanyRepository.GetAllCompaniesAsync(companyParameters, trackChanges);

            var companiesDto = mapper.Map<IEnumerable<CompanyDto>>(companiesMetaData);

            return (companiesDto, companiesMetaData.MetaData);
        }

        public async Task<CompanyDto> GetCompanyAsync(Guid companyId, bool trackChanges)
        {
            var company = await GetCompanyAndCheckIfItExistsAsync(companyId, trackChanges);

            var companyDto = mapper.Map<CompanyDto>(company);

            return companyDto;
        }

        private async Task<Company> GetCompanyAndCheckIfItExistsAsync(Guid companyId, bool trackChanges)
        {
            var company = await repository.CompanyRepository.GetCompanyAsync(companyId, trackChanges);
            if (company is null) throw new CompanyNotFoundException(companyId);
            return company;
        }

        public async Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null) throw new IdParametersBadRequestException();

            var companyEntities = await repository.CompanyRepository.GetByIdsAsync(ids, trackChanges);

            if (ids.Count() != companyEntities.Count()) throw new CollectionByIdsBadRequestException();

            var companiesToReturn = mapper.Map<IEnumerable<CompanyDto>>(companyEntities);

            return companiesToReturn;
        }

        public async Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto company)
        {
            var companyEntity = mapper.Map<Company>(company);

            repository.CompanyRepository.CreateCompany(companyEntity);
            await repository.SaveAsync();

            var companyToReturn = mapper.Map<CompanyDto>(companyEntity);

            return companyToReturn;
        }

        public async Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync(
            IEnumerable<CreateCompanyDto> companyCollection)
        {
            if (companyCollection is null)
                throw new CompanyCollectionBadRequest();

            var companyEntities = mapper.Map<IEnumerable<Company>>(companyCollection);

            foreach (var company in companyEntities)
            {
                repository.CompanyRepository.CreateCompany(company);

            }

            await repository.SaveAsync();

            var companyCollectionToReturn = mapper.Map<IEnumerable<CompanyDto>>(companyEntities);

            var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));

            return (companies: companyCollectionToReturn, ids: ids);
        }

        public async Task UpdateCompanyAsync(Guid companyId, UpdateCompanyDto companyForUpdate, bool trackChanges)
        {
            var companyEntity = await GetCompanyAndCheckIfItExistsAsync(companyId, trackChanges);

            mapper.Map(companyForUpdate, companyEntity);
            await repository.SaveAsync();
        }

        public async Task DeleteCompanyAsync(Guid companyId, bool trackChanges)
        {
            var company = await GetCompanyAndCheckIfItExistsAsync(companyId, trackChanges);

            repository.CompanyRepository.DeleteCompany(company);
            await repository.SaveAsync();
        }
    }
}

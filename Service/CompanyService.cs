using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Exceptions.BadRequestExceptions;
using Entities.Exceptions.NotFoundExceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Company;

namespace Service
{
    internal sealed class CompanyService(
        IRepositoryManager repository,
        ILoggerManager logger,
        IMapper mapper) : ICompanyService
    {
        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
            var companies = repository.CompanyRepository.GetAllCompanies(trackChanges);

            var companiesDto = mapper.Map<IEnumerable<CompanyDto>>(companies);

            return companiesDto;
        }

        public CompanyDto GetCompany(Guid companyId, bool trackChanges)
        {
            var company = repository.CompanyRepository.GetCompany(companyId, trackChanges);
            if (company is null) throw new CompanyNotFoundException(companyId);

            var companyDto = mapper.Map<CompanyDto>(company);

            return companyDto;
        }

        public IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null) throw new IdParametersBadRequestException();

            var companyEntities = repository.CompanyRepository.GetByIds(ids, trackChanges);

            if (ids.Count() != companyEntities.Count()) throw new CollectionByIdsBadRequestException();

            var companiesToReturn = mapper.Map<IEnumerable<CompanyDto>>(companyEntities);

            return companiesToReturn;
        }

        public CompanyDto CreateCompany(CreateCompanyDto company)
        {
            var companyEntity = mapper.Map<Company>(company);

            repository.CompanyRepository.CreateCompany(companyEntity);
            repository.Save();

            var companyToReturn = mapper.Map<CompanyDto>(companyEntity);

            return companyToReturn;
        }
    }
}

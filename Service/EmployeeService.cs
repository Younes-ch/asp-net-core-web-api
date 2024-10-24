using System.Dynamic;
using AutoMapper;
using Contracts;
using Entities.Exceptions.BadRequestExceptions;
using Entities.Exceptions.NotFoundExceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.Employee;
using Shared.RequestFeatures;

namespace Service
{
    internal sealed class EmployeeService(
        IRepositoryManager repository,
        ILoggerManager logger,
        IMapper mapper,
        IDataShaper<EmployeeDto> dataShaper) : IEmployeeService
    {
        public async Task<(IEnumerable<ExpandoObject> employees, MetaData metaData)> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            if (!employeeParameters.ValidAgeRange) throw new MaxAgeRangeBadRequestException();

            await CheckIfCompanyExistsAsync(companyId, trackChanges);

            var employeesWithMetaData = await repository.EmployeeRepository.GetEmployeesAsync(companyId, employeeParameters, trackChanges);
            var employeesDto = mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);
            var shapedData = dataShaper.ShapeData(employeesDto, employeeParameters.Fields);

            return (employees: shapedData, metaData: employeesWithMetaData.MetaData);
        }

        private async Task CheckIfCompanyExistsAsync(Guid companyId, bool trackChanges)
        {
            var company = await repository.CompanyRepository.GetCompanyAsync(companyId, trackChanges);
            if (company is null) throw new CompanyNotFoundException(companyId);
        }

        public async Task<ExpandoObject> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges, EmployeeParameters employeeParameters)
        {
            await CheckIfCompanyExistsAsync(companyId, trackChanges);

            var employee = await GetEmployeeAndCheckIfItExistsAsync(companyId, employeeId, trackChanges);

            var employeeDto = mapper.Map<EmployeeDto>(employee);
            var shapedData = dataShaper.ShapeData(employeeDto, employeeParameters.Fields);
            return shapedData;
        }

        private async Task<Employee> GetEmployeeAndCheckIfItExistsAsync(Guid companyId, Guid employeeId, bool trackChanges)
        {
            var employee = await repository.EmployeeRepository.GetEmployeeAsync(companyId, employeeId, trackChanges);

            if (employee is null) throw new EmployeeNotFoundException(employeeId);
            return employee;
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(Guid companyId, CreateEmployeeDto employee, bool trackChanges)
        {
            await CheckIfCompanyExistsAsync(companyId, trackChanges);

            var employeeEntity = mapper.Map<Employee>(employee);

            repository.EmployeeRepository.CreateEmployee(companyId, employeeEntity);
            await repository.SaveAsync();

            var employeeToReturn = mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn;
        }

        public async Task DeleteEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges)
        {
            await CheckIfCompanyExistsAsync(companyId, trackChanges);

            var employee = await GetEmployeeAndCheckIfItExistsAsync(companyId, employeeId, trackChanges);

            repository.EmployeeRepository.DeleteEmployee(employee);
            await repository.SaveAsync();
        }

        public async Task UpdateEmployeeAsync(Guid companyId, Guid employeeId, UpdateEmployeeDto employeeForUpdate,
            bool compTrackChanges, bool empTrackChanges)
        {
            await CheckIfCompanyExistsAsync(companyId, compTrackChanges);

            var employeeEntity = await GetEmployeeAndCheckIfItExistsAsync(companyId, employeeId, empTrackChanges);

            mapper.Map(employeeForUpdate, employeeEntity);
            await repository.SaveAsync();
        }

        public async Task<(UpdateEmployeeDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync
            (Guid companyId, Guid employeeId, bool compTrackChanges, bool empTrackChanges)
        {
            await CheckIfCompanyExistsAsync(companyId, compTrackChanges);

            var employeeEntity = await GetEmployeeAndCheckIfItExistsAsync(companyId, employeeId, empTrackChanges);

            var employeeToPatch = mapper.Map<UpdateEmployeeDto>(employeeEntity);

            return (employeeToPatch, employeeEntity);
        }

        public async Task SaveChangesForPatchAsync(UpdateEmployeeDto employeeToPatch, Employee employeeEntity)
        {
            mapper.Map(employeeToPatch, employeeEntity);
            await repository.SaveAsync();
        }
    }
}

using AutoMapper;
using Contracts;
using Entities.Exceptions.NotFoundExceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.Employee;

namespace Service
{
    internal sealed class EmployeeService(
        IRepositoryManager repository,
        ILoggerManager logger,
        IMapper mapper) : IEmployeeService
    {
        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChanges)
        {
            var company = await repository.CompanyRepository.GetCompanyAsync(companyId, trackChanges);
            if (company is null) throw new CompanyNotFoundException(companyId);

            var employees = await repository.EmployeeRepository.GetEmployeesAsync(companyId, trackChanges);
            var employeesDto = mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return employeesDto;
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges)
        {
            var company = await repository.CompanyRepository.GetCompanyAsync(companyId, trackChanges);
            if (company is null) throw new CompanyNotFoundException(companyId);

            var employee = await repository.EmployeeRepository.GetEmployeeAsync(companyId, employeeId, trackChanges);

            if (employee is null) throw new EmployeeNotFoundException(employeeId);

            var employeeDto = mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(Guid companyId, CreateEmployeeDto employee, bool trackChanges)
        {
            var company = await repository.CompanyRepository.GetCompanyAsync(companyId, trackChanges);

            if (company is null) throw new CompanyNotFoundException(companyId);

            var employeeEntity = mapper.Map<Employee>(employee);

            repository.EmployeeRepository.CreateEmployee(companyId, employeeEntity);
            await repository.SaveAsync();

            var employeeToReturn = mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn;
        }

        public async Task DeleteEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges)
        {
            var company = await repository.CompanyRepository.GetCompanyAsync(companyId, trackChanges);

            if (company is null) throw new CompanyNotFoundException(companyId);

            var employee = await repository.EmployeeRepository.GetEmployeeAsync(companyId, employeeId, trackChanges);

            if (employee is null) throw new EmployeeNotFoundException(employeeId);

            repository.EmployeeRepository.DeleteEmployee(employee);
            await repository.SaveAsync();
        }

        public async Task UpdateEmployeeAsync(Guid companyId, Guid employeeId, UpdateEmployeeDto employeeForUpdate,
            bool compTrackChanges, bool empTrackChanges)
        {
            var company = await repository.CompanyRepository.GetCompanyAsync(companyId, compTrackChanges);

            if (company is null) throw new CompanyNotFoundException(companyId);

            var employeeEntity = await repository.EmployeeRepository.GetEmployeeAsync(companyId, employeeId, empTrackChanges);

            if (employeeEntity is null) throw new EmployeeNotFoundException(employeeId);

            mapper.Map(employeeForUpdate, employeeEntity);
            await repository.SaveAsync();
        }

        public async Task<(UpdateEmployeeDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync
            (Guid companyId, Guid employeeId, bool compTrackChanges, bool empTrackChanges)
        {
            var company = await repository.CompanyRepository.GetCompanyAsync(companyId, compTrackChanges);

            if (company is null) throw new CompanyNotFoundException(companyId);

            var employeeEntity = await repository.EmployeeRepository.GetEmployeeAsync(companyId, employeeId, empTrackChanges);

            if (employeeEntity is null) throw new EmployeeNotFoundException(employeeId);

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

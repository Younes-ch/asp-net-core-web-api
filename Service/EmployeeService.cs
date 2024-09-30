using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class EmployeeService(
        IRepositoryManager repository,
        ILoggerManager logger,
        IMapper mapper) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
        {
            var company = repository.CompanyRepository.GetCompany(companyId, trackChanges);
            if (company is null) throw new CompanyNotFoundException(companyId);

            var employees = repository.EmployeeRepository.GetEmployees(companyId, trackChanges);
            var employeesDto = mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return employeesDto;
        }

        public EmployeeDto GetEmployee(Guid companyId, Guid employeeId, bool trackChanges)
        {
            var company = repository.CompanyRepository.GetCompany(companyId, trackChanges);
            if (company is null) throw new CompanyNotFoundException(companyId);

            var employee = repository.EmployeeRepository.GetEmployee(companyId, employeeId, trackChanges);

            if (employee is null) throw new EmployeeNotFoundException(employeeId);

            var employeeDto = mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }
    }
}

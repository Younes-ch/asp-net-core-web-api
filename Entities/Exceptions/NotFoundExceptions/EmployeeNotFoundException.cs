namespace Entities.Exceptions.NotFoundExceptions;

public sealed class EmployeeNotFoundException(Guid employeeId) :
    NotFoundException($"Employee with id: {employeeId} doesn't exist in the database.");
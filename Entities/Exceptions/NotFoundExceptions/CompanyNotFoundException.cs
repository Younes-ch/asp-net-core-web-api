namespace Entities.Exceptions.NotFoundExceptions
{
    public sealed class CompanyNotFoundException(Guid companyId)
        : NotFoundException($"The company with id: {companyId} doesn't exist in the database.");
}

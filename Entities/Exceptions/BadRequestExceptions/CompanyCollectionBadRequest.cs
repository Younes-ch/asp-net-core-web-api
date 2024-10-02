namespace Entities.Exceptions.BadRequestExceptions
{
    public sealed class CompanyCollectionBadRequest() : BadRequestException("Company collection sent from a client is null.");
}

namespace Entities.Exceptions.BadRequestExceptions
{
    public sealed class CollectionByIdsBadRequestException()
        : BadRequestException("Collection count mismatch comparing to ids.");
}

namespace Entities.Exceptions.BadRequestExceptions
{
    public sealed class MaxAgeRangeBadRequestException()
        : BadRequestException("Max age can't be less than min age.");
}

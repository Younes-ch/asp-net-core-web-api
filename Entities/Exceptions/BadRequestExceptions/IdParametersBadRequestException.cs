namespace Entities.Exceptions.BadRequestExceptions;

public sealed class IdParametersBadRequestException() : BadRequestException("Parameter ids is null");
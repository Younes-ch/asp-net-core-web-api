namespace Entities.Exceptions.BadRequestExceptions;

public abstract class BadRequestException(string message) : Exception(message);
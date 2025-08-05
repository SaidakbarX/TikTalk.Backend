namespace TikTalk.Backend.Core.Exceptions;

public abstract class BaseException : Exception
{
    protected BaseException(string message) : base(message) { }
    protected BaseException(string message, Exception innerException) : base(message, innerException) { }
}

public class EntityNotFoundException : BaseException
{
    public EntityNotFoundException(string message) : base(message) { }
    public EntityNotFoundException(string entityName, object id) 
        : base($"{entityName} with ID {id} was not found.") { }
}

public class AuthException : BaseException
{
    public AuthException(string message) : base(message) { }
}

public class UnauthorizedException : BaseException
{
    public UnauthorizedException(string message = "Unauthorized access") : base(message) { }
}

public class ForbiddenException : BaseException
{
    public ForbiddenException(string message = "Access forbidden") : base(message) { }
}

public class NotAllowedException : BaseException
{
    public NotAllowedException(string message) : base(message) { }
}

public class ValidationException : BaseException
{
    public ValidationException(string message) : base(message) { }
}

public class DuplicateException : BaseException
{
    public DuplicateException(string message) : base(message) { }
}
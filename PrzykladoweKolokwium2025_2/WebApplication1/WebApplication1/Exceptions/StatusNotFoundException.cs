namespace WebApplication1.Exceptions;

public class StatusNotFoundException : Exception
{
    public StatusNotFoundException()
    {
    }

    public StatusNotFoundException(string? message) : base(message)
    {
    }

    public StatusNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
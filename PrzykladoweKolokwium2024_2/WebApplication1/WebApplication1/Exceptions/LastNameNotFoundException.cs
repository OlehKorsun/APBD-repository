namespace WebApplication1.Exceptions;

public class LastNameNotFoundException : Exception
{
    public LastNameNotFoundException()
    {
    }

    public LastNameNotFoundException(string? message) : base(message)
    {
    }

    public LastNameNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
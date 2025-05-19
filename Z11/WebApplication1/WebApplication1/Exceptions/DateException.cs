namespace WebApplication.Exceptions;

public class DateException : Exception
{
    public DateException()
    {
    }
    
    public DateException(string? message) : base(message)
    {
    }

    public DateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
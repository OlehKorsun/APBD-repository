namespace WebApplication1.Exceptions;

public class ConcertNotFoundException : Exception
{
    public ConcertNotFoundException()
    {
    }

    public ConcertNotFoundException(string? message) : base(message)
    {
    }

    public ConcertNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
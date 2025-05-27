namespace WebApplication1.Exceptions;

public class TripDateException : Exception
{
    public TripDateException()
    {
    }

    public TripDateException(string? message) : base(message)
    {
    }

    public TripDateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
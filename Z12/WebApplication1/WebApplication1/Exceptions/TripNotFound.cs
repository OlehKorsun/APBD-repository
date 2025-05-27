namespace WebApplication1.Exceptions;

public class TripNotFound : Exception
{
    public TripNotFound()
    {
    }

    public TripNotFound(string? message) : base(message)
    {
    }

    public TripNotFound(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
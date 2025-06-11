namespace WebApplication1.Exceptions;

public class RacerNotFoundException : Exception
{
    public RacerNotFoundException()
    {
    }

    public RacerNotFoundException(string? message) : base(message)
    {
    }

    public RacerNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
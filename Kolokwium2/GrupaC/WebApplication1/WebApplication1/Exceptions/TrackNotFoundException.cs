namespace WebApplication1.Exceptions;

public class TrackNotFoundException : Exception
{
    public TrackNotFoundException()
    {
    }

    public TrackNotFoundException(string? message) : base(message)
    {
    }

    public TrackNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
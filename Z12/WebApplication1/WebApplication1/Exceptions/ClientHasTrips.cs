namespace WebApplication1.Exceptions;

public class ClientHasTrips : Exception
{
    public ClientHasTrips()
    {
    }
    
    public ClientHasTrips(string? message) : base(message)
    {
    }

    public ClientHasTrips(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
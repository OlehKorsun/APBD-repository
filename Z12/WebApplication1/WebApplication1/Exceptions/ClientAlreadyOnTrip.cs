namespace WebApplication1.Exceptions;

public class ClientAlreadyOnTrip : Exception
{
    public ClientAlreadyOnTrip()
    {
    }

    public ClientAlreadyOnTrip(string? message) : base(message)
    {
    }

    public ClientAlreadyOnTrip(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
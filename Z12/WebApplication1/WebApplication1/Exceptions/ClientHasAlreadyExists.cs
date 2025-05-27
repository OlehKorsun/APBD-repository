namespace WebApplication1.Exceptions;

public class ClientHasAlreadyExists : Exception
{
    public ClientHasAlreadyExists()
    {
    }

    public ClientHasAlreadyExists(string? message) : base(message)
    {
    }

    public ClientHasAlreadyExists(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
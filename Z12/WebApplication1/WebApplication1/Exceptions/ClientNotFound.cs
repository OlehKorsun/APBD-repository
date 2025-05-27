namespace WebApplication1.Exceptions;

public class ClientNotFound : Exception
{
    public ClientNotFound()
    {
    }
    
    public ClientNotFound(string? message) : base(message)
    {
    }

    public ClientNotFound(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
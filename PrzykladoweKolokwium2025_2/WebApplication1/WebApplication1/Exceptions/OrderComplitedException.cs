namespace WebApplication1.Exceptions;

public class OrderComplitedException : Exception
{
    public OrderComplitedException()
    {
    }

    public OrderComplitedException(string? message) : base(message)
    {
    }

    public OrderComplitedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
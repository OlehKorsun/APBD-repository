namespace WebApplication.Exceptions;

public class MedicamentLimitException : Exception
{
    public MedicamentLimitException()
    {
    }
    
    public MedicamentLimitException(string? message) : base(message)
    {
    }

    public MedicamentLimitException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
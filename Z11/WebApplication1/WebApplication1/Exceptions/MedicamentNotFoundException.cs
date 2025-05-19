namespace WebApplication.Exceptions;

public class MedicamentNotFoundException : Exception
{
    public MedicamentNotFoundException()
    {
    }
    
    public MedicamentNotFoundException(string? message) : base(message)
    {
    }

    public MedicamentNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
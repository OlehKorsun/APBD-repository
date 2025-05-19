namespace WebApplication.Exceptions;

public class DoctorNotFoundException : Exception
{
    public DoctorNotFoundException()
    {
    }
    
    public DoctorNotFoundException(string? message) : base(message)
    {
    }

    public DoctorNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
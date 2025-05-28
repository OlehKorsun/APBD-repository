namespace Poprawka1.Exceptions;

public class RentalAlreadyExistsException : Exception
{
    public RentalAlreadyExistsException()
    {
    }

    public RentalAlreadyExistsException(string? message) : base(message)
    {
    }

    public RentalAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
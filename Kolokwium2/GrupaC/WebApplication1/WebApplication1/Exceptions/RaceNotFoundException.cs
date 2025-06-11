using System.Runtime.Serialization;

namespace WebApplication1.Exceptions;

public class RaceNotFoundException : Exception
{
    public RaceNotFoundException()
    {
    }

    protected RaceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public RaceNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public RaceNotFoundException(string? message) : base(message)
    {
    }
}
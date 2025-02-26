namespace InvoizR.Domain.Exceptions;

public class InvoizRException : Exception
{
    public InvoizRException()
        : base()
    {
    }

    public InvoizRException(string message)
        : base(message)
    {
    }

    public InvoizRException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

namespace InvoizR.Domain.Exceptions;

public class InvalidInvoiceCancellationException : InvoizRException
{
    public InvalidInvoiceCancellationException()
        : base()
    {
    }

    public InvalidInvoiceCancellationException(string message)
        : base(message)
    {
    }

    public InvalidInvoiceCancellationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

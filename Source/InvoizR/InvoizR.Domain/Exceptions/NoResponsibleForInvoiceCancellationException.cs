namespace InvoizR.Domain.Exceptions;

public class NoResponsibleForInvoiceCancellationException : InvoizRException
{
    public NoResponsibleForInvoiceCancellationException()
    : base()
    {
    }

    public NoResponsibleForInvoiceCancellationException(string message)
        : base(message)
    {
    }

    public NoResponsibleForInvoiceCancellationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

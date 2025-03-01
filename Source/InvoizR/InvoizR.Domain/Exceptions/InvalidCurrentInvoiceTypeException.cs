namespace InvoizR.Domain.Exceptions;

public class InvalidCurrentInvoiceTypeException : InvoizRException
{
    public InvalidCurrentInvoiceTypeException()
        : base()
    {
    }

    public InvalidCurrentInvoiceTypeException(string message)
        : base(message)
    {
    }

    public InvalidCurrentInvoiceTypeException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

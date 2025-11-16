namespace InvoizR.Domain.Exceptions;

public class NotSupportedWebhookProtocolException : InvoizRException
{
    public NotSupportedWebhookProtocolException()
        : base()
    {
    }

    public NotSupportedWebhookProtocolException(string message)
        : base(message)
    {
    }

    public NotSupportedWebhookProtocolException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

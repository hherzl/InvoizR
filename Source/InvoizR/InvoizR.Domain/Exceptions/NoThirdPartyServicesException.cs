namespace InvoizR.Domain.Exceptions;

public class NoThirdPartyServicesException : InvoizRException
{
    public NoThirdPartyServicesException()
        : base()
    {
    }

    public NoThirdPartyServicesException(string message)
        : base($"There are no third party services for '{message}' environment")
    {
    }

    public NoThirdPartyServicesException(string message, Exception innerException)
        : base($"There are no third party services for '{message}' environment", innerException)
    {
    }

    public NoThirdPartyServicesException(string company, string environment)
        : base($"There are no third party services for '{company}' in '{environment}' environment")
    {
    }
}

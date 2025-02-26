using System.Net.Mail;
using InvoizR.Clients;
using InvoizR.Clients.Contracts;
using Microsoft.Extensions.Options;

namespace InvoizR.Infrastructure.Clients;

public class SatanCitySmtpClient : ISmtpClient
{
    public SatanCitySmtpClient(IOptions<SmtpClientSettings> options)
    {
        ClientSettings = options.Value;
    }

    public SmtpClientSettings ClientSettings { get; }

    public void Send(MailMessage mailMessage)
    {
        // TODO: Emulate mail sending...
    }
}

using System.Net;
using System.Net.Mail;
using InvoizR.Clients;
using InvoizR.Clients.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace InvoizR.Infrastructure.Clients;

public class SatanCitySmtpClient : ISmtpClient
{
    private readonly ILogger _logger;

    public SatanCitySmtpClient(ILogger<SatanCitySmtpClient> logger, IOptions<SmtpClientSettings> options)
    {
        _logger = logger;
        ClientSettings = options.Value;
    }

    public SmtpClientSettings ClientSettings { get; }

    public void Send(MailMessage mailMessage)
    {
        _logger?.LogInformation($"Sending email to {string.Join(",", mailMessage.To.Select(item => item.Address))}...");

        try
        {
            using var client = new SmtpClient(ClientSettings.Host, ClientSettings.Port)
            {
                EnableSsl = ClientSettings.EnableSsl,
                UseDefaultCredentials = ClientSettings.UseDefaultCredentials,
                Credentials = new NetworkCredential(ClientSettings.UserName, ClientSettings.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            // TODO: uncomment until SMTP is available...
            //client.Send(mailMessage);
        }
        catch (Exception ex)
        {
            _logger?.LogCritical(ex, $"Error sending email on SatanCitySmtpClient");
        }
    }
}

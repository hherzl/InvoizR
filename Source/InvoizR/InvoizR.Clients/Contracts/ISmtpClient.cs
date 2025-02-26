using System.Net.Mail;

namespace InvoizR.Clients.Contracts;

public interface ISmtpClient
{
    SmtpClientSettings ClientSettings { get; }

    void Send(MailMessage mailMessage);
}

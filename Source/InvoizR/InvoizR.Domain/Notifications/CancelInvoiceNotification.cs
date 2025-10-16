using InvoizR.Domain.Entities;
using MediatR;

namespace InvoizR.Domain.Notifications;

public class CancelInvoiceNotification : INotification
{
    public CancelInvoiceNotification(Invoice invoice)
    {
        Id = invoice.Id;
        Payload = invoice.CancellationPayload;
    }

    public CancelInvoiceNotification(long? id, string payload)
    {
        Id = id;
        Payload = payload;
    }

    public long? Id { get; }
    public string Payload { get; }
}

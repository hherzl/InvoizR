using InvoizR.Domain.Entities;
using MediatR;

namespace InvoizR.Domain.Notifications;

public class ExportInvoiceNotification : INotification
{
    public ExportInvoiceNotification(Invoice invoice)
    {
        Id = invoice.Id;
        InvoiceTypeId = invoice.InvoiceTypeId;
    }

    public long? Id { get; }
    public short? InvoiceTypeId { get; }
}

using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

public partial class InvoiceWebhookNotification : AuditableEntity
{
    public InvoiceWebhookNotification()
    {
    }

    public InvoiceWebhookNotification(long? id)
    {
        Id = id;
    }

    public long? Id { get; set; }
    public long? InvoiceId { get; set; }
    public string Protocol { get; set; }
    public string Address { get; set; }
    public string ContentType { get; set; }
    public bool? IsSuccess { get; set; }
    public string Request { get; set; }
    public string Response { get; set; }

    public virtual Invoice Invoice { get; set; }
}

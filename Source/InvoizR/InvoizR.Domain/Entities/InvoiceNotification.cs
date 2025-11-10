using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

public partial class InvoiceNotification : AuditableEntity
{
    public InvoiceNotification()
    {
    }

    public InvoiceNotification(long? id)
    {
        Id = id;
    }

    public long? Id { get; set; }
    public long? InvoiceId { get; set; }
    public string Email { get; set; }
    public bool? Bcc { get; set; }
    public short? Files { get; set; }
    public bool? Successful { get; set; }
}

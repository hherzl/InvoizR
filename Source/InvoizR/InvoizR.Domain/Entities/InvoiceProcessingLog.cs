using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

public partial class InvoiceSyncLog : AuditableEntity
{
    public InvoiceSyncLog()
    {
    }

    public InvoiceSyncLog(long? id)
    {
        Id = id;
    }

    public long? Id { get; set; }
    public long? InvoiceId { get; set; }
    public short? SyncStatusId { get; set; }
    public string LogType { get; set; }
    public string ContentType { get; set; }
    public string Content { get; set; }
}

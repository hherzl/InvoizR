using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

public partial class InvoiceSyncStatusLog : AuditableEntity
{
    public InvoiceSyncStatusLog()
    {
    }

    public InvoiceSyncStatusLog(long? id)
    {
        Id = id;
    }

    public long? Id { get; set; }
    public long? InvoiceId { get; set; }
    public short? SyncStatusId { get; set; }
}

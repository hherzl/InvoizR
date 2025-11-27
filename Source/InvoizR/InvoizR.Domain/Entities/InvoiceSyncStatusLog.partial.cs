namespace InvoizR.Domain.Entities;

public partial class InvoiceSyncStatusLog
{
    public InvoiceSyncStatusLog(long? invoiceId, short? syncStatusId)
    {
        InvoiceId = invoiceId;
        CreatedAt = DateTime.Now;
        SyncStatusId = syncStatusId;
    }
}

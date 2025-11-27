namespace InvoizR.Clients.DataContracts.Invoices;

public record InvoiceSyncLogItemModel
{
    public int? Id { get; set; }
    public long? InvoiceId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public short? SyncStatusId { get; set; }
    public string SyncStatus { get; set; }
    public string LogType { get; set; }
    public string ContentType { get; set; }
    public string Content { get; set; }
}

namespace InvoizR.Clients.DataContracts.Invoices;

public partial record InvoiceSyncStatusLogItemModel
{
    public int? Id { get; set; }
    public long? InvoiceId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public short? SyncStatusId { get; set; }
    public string SyncStatus { get; set; }
}

public partial record InvoiceSyncStatusLogItemModel
{
    public bool IsRequested()
        => SyncStatusId == 1000;

    public bool IsDeclined()
        => SyncStatusId == 2000;
    public bool IsProcessed()
        => SyncStatusId >= 3000;
}

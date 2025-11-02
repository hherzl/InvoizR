namespace InvoizR.Clients.DataContracts.Fallback;

public record class FallbackDetailsModel
{
    public FallbackDetailsModel()
    {
        Files = [];
        ProcessingLogs = [];
        Invoices = [];
    }

    public short? Id { get; set; }
    public short? CompanyId { get; set; }
    public string Company { get; set; }
    public string Name { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public bool? Enable { get; set; }
    public string FallbackGuid { get; set; }
    public short? SyncStatusId { get; set; }
    public string SyncStatus { get; set; }
    public string Payload { get; set; }
    public int? RetryIn { get; set; }
    public int? SyncAttempts { get; set; }
    public DateTime? EmitDateTime { get; set; }
    public string ReceiptStamp { get; set; }

    public List<FallbackInvoiceItemModel> Invoices { get; set; }
    public List<FallbackProcessingLogItemModel> ProcessingLogs { get; set; }
    public List<FallbackFileItemModel> Files { get; set; }
}

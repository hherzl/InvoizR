namespace InvoizR.Clients.DataContracts;

public record InvoiceDetailsModel
{
    public long? Id { get; set; }
    public short? ContingencyId { get; set; }
    public short? PosId { get; set; }
    public string Pos { get; set; }

    public string CustomerId { get; set; }
    public string CustomerWtId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerCountryId { get; set; }
    public short? CustomerCountryLevelId { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerEmail { get; set; }
    public DateTime? CustomerLastUpdated { get; set; }
    public DateTime? CreatedAt { get; set; }

    public short? InvoiceTypeId { get; set; }
    public long? InvoiceNumber { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public decimal? InvoiceTotal { get; set; }
    public int? Lines { get; set; }

    public string SchemaType { get; set; }
    public short? SchemaVersion { get; set; }
    public string GenerationCode { get; set; }
    public string ControlNumber { get; set; }
    public string Serialization { get; set; }
    public short? ProcessingStatusId { get; set; }
    public string ProcessingStatus { get; set; }
    public int? RetryIn { get; set; }
    public int? SyncAttempts { get; set; }
    public DateTime? ProcessingDateTime { get; set; }
    public string ReceiptStamp { get; set; }
    public string ExternalUrl { get; set; }
    public string Notes { get; set; }

    public bool Processed { get; set; }

    public List<InvoiceProcessingStatusLogItemModel> ProcessingStatusLogs { get; set; }
    public List<InvoiceProcessingLogItemModel> ProcessingLogs { get; set; }
    public List<InvoiceFileItemModel> Files { get; set; }
    public List<InvoiceNotificationItemModel> Notifications { get; set; }
}

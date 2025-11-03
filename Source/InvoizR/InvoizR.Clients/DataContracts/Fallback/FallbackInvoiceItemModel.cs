namespace InvoizR.Clients.DataContracts.Fallback;

public record FallbackInvoiceItemModel
{
    public long? Id { get; set; }
    public short? InvoiceTypeId { get; set; }
    public string InvoiceType { get; set; }
    public string InvoiceGuid { get; set; }
    public string AuditNumber { get; set; }
}

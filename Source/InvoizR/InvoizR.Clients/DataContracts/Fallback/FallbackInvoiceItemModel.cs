namespace InvoizR.Clients.DataContracts.Fallback;

public record FallbackInvoiceItemModel
{
    public long? Id { get; set; }
    public short? InvoiceTypeId { get; set; }
    public string InvoiceType { get; set; }
    public string GenerationCode { get; set; }
    public string ControlNumber { get; set; }
}

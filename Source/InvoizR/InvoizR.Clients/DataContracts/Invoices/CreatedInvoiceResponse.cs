using InvoizR.Clients.DataContracts.Common;

namespace InvoizR.Clients.DataContracts.Invoices;

public record CreatedInvoiceResponse : CreatedResponse<long?>
{
    public CreatedInvoiceResponse()
    {
    }

    public short? InvoiceTypeId { get; set; }
    public string SchemaType { get; set; }
    public short? SchemaVersion { get; set; }
    public string InvoiceGuid { get; set; }
    public string AuditNumber { get; set; }
    public string ReceiptStamp { get; set; }
}

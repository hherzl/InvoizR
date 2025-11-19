using InvoizR.Clients.DataContracts.Common;

namespace InvoizR.Clients.DataContracts.Invoices;

public record CreatedInvoiceResponse : CreatedResponse<long?>
{
    public CreatedInvoiceResponse()
    {
    }

    public CreatedInvoiceResponse(long? id, short? invoiceTypeId, string schemaType, short? schemaVersion, string invoideGuid, string auditNumber)
    {
        Id = id;
        InvoiceTypeId = invoiceTypeId;
        SchemaType = schemaType;
        SchemaVersion = schemaVersion;
        InvoiceGuid = invoideGuid;
        AuditNumber = auditNumber;
    }

    public short? InvoiceTypeId { get; set; }
    public string SchemaType { get; set; }
    public short? SchemaVersion { get; set; }
    public string InvoiceGuid { get; set; }
    public string AuditNumber { get; set; }
}

using System.Text.Json;
using InvoizR.SharedKernel;

namespace InvoizR.Application.Services.Models;

public record WebhookInvoiceNodeModel
{
    public long? InvoiceId { get; set; }
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }
    public short? InvoiceTypeId { get; set; }
    public long? InvoiceNumber { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public decimal? InvoiceTotal { get; set; }
    public string InvoiceGuid { get; set; }
    public string AuditNumber { get; set; }
    public string ReceiptStamp { get; set; }
    public string ExternalUrl { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this, AbstractModel.DefaultJsonSerializerOpts);
}

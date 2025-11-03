namespace InvoizR.Application.Reports.Templates.Common;

public record DteTemplateModel<TDte>
{
    public DteTemplateModel()
    {
        Title = "Documento Tributario Electrónico";
    }

    public string Qr { get; set; }

    public string Title { get; set; }
    public short InvoiceTypeId { get; set; }
    public string InvoiceType { get; set; }

    public DteEmitterModel Emitter { get; set; }
    public DteReceiverModel Receiver { get; set; }

    public string SchemaType { get; set; }
    public short? SchemaVersion { get; set; }

    public string InvoiceGuid { get; set; }
    public string AuditNumber { get; set; }
    public DateTime? EmitDateTime { get; set; }
    public string ReceiptStamp { get; set; }
    public string ExternalUrl { get; set; }

    public TDte Dte { get; set; }
    public string ErrorMessage { get; set; }
}

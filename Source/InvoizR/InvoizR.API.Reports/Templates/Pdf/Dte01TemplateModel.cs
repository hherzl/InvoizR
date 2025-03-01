using InvoizR.SharedKernel.Mh;

namespace InvoizR.API.Reports.Templates.Pdf;

public record Dte01TemplateModel
{
    public string Qr { get; set; }

    public DteEmitterModel Emitter { get; set; }
    public DteReceiverModel Receiver { get; set; }

    public string SchemaType { get; set; }
    public short? SchemaVersion { get; set; }

    public string GenerationCode { get; set; }
    public string ControlNumber { get; set; }
    public DateTime? ProcessingDateTime { get; set; }
    public string ReceiptStamp { get; set; }
    public string ExternalUrl { get; set; }

    public FeFcv1 Dte { get; set; }

    public string ErrorMessage { get; set; }
}

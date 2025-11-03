namespace InvoizR.Clients.DataContracts;

public record InvoiceQrModel
{
    public byte[] Qr { get; set; }
    public string ContentType { get; set; }
    public string AuditNumber { get; set; }
}

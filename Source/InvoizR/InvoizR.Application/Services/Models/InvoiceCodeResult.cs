namespace InvoizR.Application.Services.Models;

public record InvoiceCodeResult
{
    public InvoiceCodeResult()
    {
    }

    public InvoiceCodeResult(short version, string type, Guid invoiceGuid, string auditNumber)
    {
        Version = version;
        Type = type;
        InvoiceGuid = invoiceGuid.ToString().ToUpper();
        AuditNumber = auditNumber;
    }

    public short Version { get; }
    public string Type { get; }
    public string InvoiceGuid { get; }
    public string AuditNumber { get; }

    public bool HasAuditNumber
        => !string.IsNullOrEmpty(AuditNumber);

    public static InvoiceCodeResult Empty()
        => new();
}

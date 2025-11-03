namespace InvoizR.Domain.Entities;

public partial class InvoiceFile
{
    public InvoiceFile(Invoice invoice, byte[] fileBytes, string mimeType, string fileType)
    {
        InvoiceId = invoice.Id;
        Size = fileBytes.Length;
        MimeType = mimeType;
        FileType = fileType;
        FileName = $"{invoice.AuditNumber}.{fileType.ToLower()}";
        CreatedAt = DateTime.Now;
        File = fileBytes;
    }
}

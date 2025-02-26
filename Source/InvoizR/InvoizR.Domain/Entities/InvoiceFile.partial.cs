namespace InvoizR.Domain.Entities;

public partial class InvoiceFile
{
    public static InvoiceFile Create(Invoice invoice, byte[] fileBytes, string mimeType, string fileType)
    {
        return new()
        {
            InvoiceId = invoice.Id,
            Size = fileBytes.Length,
            MimeType = mimeType,
            FileType = fileType,
            FileName = $"{invoice.ControlNumber}.{fileType.ToLower()}",
            CreatedAt = DateTime.Now,
            File = fileBytes
        };
    }
}

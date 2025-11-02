namespace InvoizR.Domain.Entities;

public partial class FallbackFile
{
    public FallbackFile(Fallback fallback, byte[] fileBytes, string mimeType, string fileType)
    {
        FallbackId = fallback.Id;
        Size = fileBytes.Length;
        MimeType = mimeType;
        FileType = fileType;
        FileName = $"{fallback.FallbackGuid}.{fileType.ToLower()}";
        CreatedAt = DateTime.Now;
        File = fileBytes;
    }
}

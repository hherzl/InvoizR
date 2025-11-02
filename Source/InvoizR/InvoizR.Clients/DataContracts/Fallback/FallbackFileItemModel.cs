using System.Text;

namespace InvoizR.Clients.DataContracts.Fallback;

public record FallbackFileItemModel
{
    public long? Size { get; set; }
    public string MimeType { get; set; }
    public string FileType { get; set; }
    public string FileName { get; set; }
    public string ExternalUrl { get; set; }
    public DateTime? CreatedAt { get; set; }
    public byte[] File { get; set; }

    public bool IsJson
        => string.Compare(FileType, "json", true) == 0;

    public string GetJson()
        => Encoding.Default.GetString(File);

    public bool IsPdf
        => string.Compare(FileType, "pdf", true) == 0;

    public string GetPdf()
        => Convert.ToBase64String(File);
}

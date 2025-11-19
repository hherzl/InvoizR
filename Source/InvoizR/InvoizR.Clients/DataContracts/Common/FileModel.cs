using System.Text;
using InvoizR.SharedKernel;

namespace InvoizR.Clients.DataContracts.Common;

public record FileModel
{
    public long? Size { get; set; }
    public string MimeType { get; set; }
    public string FileName { get; set; }
    public string ExternalUrl { get; set; }
    public DateTime? CreatedAt { get; set; }

    public string FileType { get; set; }

    public bool IsJson
        => string.Compare(FileType, Tokens.Json, true) == 0;

    public bool IsPdf
        => string.Compare(FileType, Tokens.Pdf, true) == 0;

    public byte[] File { get; set; }

    public string GetJson()
        => Encoding.Default.GetString(File);

    public string GetPdf()
        => Convert.ToBase64String(File);
}

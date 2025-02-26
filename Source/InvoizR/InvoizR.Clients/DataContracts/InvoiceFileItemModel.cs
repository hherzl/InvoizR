namespace InvoizR.Clients.DataContracts;

public record InvoiceFileItemModel
{
    public InvoiceFileItemModel()
    {
    }

    public long? Size { get; set; }
    public string MimeType { get; set; }
    public string FileType { get; set; }
    public string FileName { get; set; }
    public string ExternalUrl { get; set; }
    public DateTime? CreatedAt { get; set; }
    public byte[] File { get; set; }
}

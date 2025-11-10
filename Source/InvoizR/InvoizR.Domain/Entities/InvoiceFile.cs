using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

public partial class InvoiceFile : AuditableEntity
{
    public InvoiceFile()
    {
    }

    public InvoiceFile(long? id)
    {
        Id = id;
    }

    public long? Id { get; set; }
    public long? InvoiceId { get; set; }
    public long? Size { get; set; }
    public string MimeType { get; set; }
    public string FileType { get; set; }
    public string FileName { get; set; }
    public string ExternalUrl { get; set; }
    public byte[] File { get; set; }
}

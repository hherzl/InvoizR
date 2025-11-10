using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

public partial class FallbackFile : AuditableEntity
{
    public FallbackFile()
    {
    }

    public FallbackFile(long? id)
    {
        Id = id;
    }

    public long? Id { get; set; }
    public short? FallbackId { get; set; }
    public long? Size { get; set; }
    public string MimeType { get; set; }
    public string FileType { get; set; }
    public string FileName { get; set; }
    public string ExternalUrl { get; set; }
    public byte[] File { get; set; }

    public virtual Fallback FallbackFk { get; set; }
}

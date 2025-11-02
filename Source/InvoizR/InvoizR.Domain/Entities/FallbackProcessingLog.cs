using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

public partial class FallbackProcessingLog : Entity
{
    public FallbackProcessingLog()
    {
    }

    public FallbackProcessingLog(int? id)
    {
        Id = id;
    }

    public int? Id { get; set; }
    public short? FallbackId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public short? SyncStatusId { get; set; }
    public string LogType { get; set; }
    public string ContentType { get; set; }
    public string Content { get; set; }

    public virtual Fallback Fallback { get; set; }
}

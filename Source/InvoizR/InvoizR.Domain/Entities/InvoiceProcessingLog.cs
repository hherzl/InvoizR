using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

public partial class InvoiceProcessingLog : Entity
{
    public InvoiceProcessingLog()
    {
    }

    public InvoiceProcessingLog(long? id)
    {
        Id = id;
    }

    public long? Id { get; set; }
    public long? InvoiceId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public short? ProcessingStatusId { get; set; }
    public string LogType { get; set; }
    public string ContentType { get; set; }
    public string Content { get; set; }
}

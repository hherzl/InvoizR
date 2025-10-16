using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

public partial class InvoiceCancellationLog : Entity
{
    public InvoiceCancellationLog()
    {
        CreatedAt = DateTime.Now;
    }

    public InvoiceCancellationLog(long? id)
        : this()
    {
        Id = id;
    }

    public long? Id { get; set; }
    public long? InvoiceId { get; set; }
    public short? ProcessingStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string LogType { get; set; }
    public string ContentType { get; set; }
    public string Payload { get; set; }

    public virtual Invoice Invoice { get; set; }
}

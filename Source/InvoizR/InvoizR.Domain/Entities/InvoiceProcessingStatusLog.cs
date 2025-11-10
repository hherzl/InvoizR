using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

public partial class InvoiceProcessingStatusLog : AuditableEntity
{
    public InvoiceProcessingStatusLog()
    {
    }

    public InvoiceProcessingStatusLog(long? id)
    {
        Id = id;
    }

    public long? Id { get; set; }
    public long? InvoiceId { get; set; }
    public short? ProcessingStatusId { get; set; }
}

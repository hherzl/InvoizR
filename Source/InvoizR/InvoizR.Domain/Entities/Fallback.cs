using System.Collections.ObjectModel;
using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

public partial class Fallback : Entity
{
    public Fallback()
    {
    }

    public Fallback(short? id)
    {
        Id = id;
    }

    public short? Id { get; set; }
    public short? CompanyId { get; set; }
    public string Name { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public bool? Enable { get; set; }
    public string FallbackGuid { get; set; }
    public short? SyncStatusId { get; set; }
    public string Payload { get; set; }
    public int? RetryIn { get; set; }
    public int? SyncAttempts { get; set; }
    public DateTime? EmitDateTime { get; set; }
    public string ReceiptStamp { get; set; }

    public virtual Company Company { get; set; }
    public virtual Collection<FallbackProcessingLog> FallbackProcessingLogs { get; set; }
    public virtual Collection<FallbackFile> FallbackFiles { get; set; }
    public virtual Collection<Invoice> Invoices { get; set; }
}

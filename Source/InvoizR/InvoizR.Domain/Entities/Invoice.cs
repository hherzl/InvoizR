using System.Collections.ObjectModel;
using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

public partial class Invoice : Entity
{
    public Invoice()
    {
        CreatedAt = DateTime.Now;
        RetryIn = 0;
        SyncAttempts = 0;
    }

    public Invoice(long? id)
    {
        Id = id;
    }

    public long? Id { get; set; }
    public short? ContingencyId { get; set; }
    public short? PosId { get; set; }
    public string CustomerId { get; set; }
    public string CustomerDocumentTypeId { get; set; }
    public string CustomerDocumentNumber { get; set; }
    public string CustomerWtId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerCountryId { get; set; }
    public short? CustomerCountryLevelId { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerEmail { get; set; }
    public DateTime? CustomerLastUpdated { get; set; }
    public DateTime? CreatedAt { get; set; }
    public short? InvoiceTypeId { get; set; }
    public long? InvoiceNumber { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public decimal? InvoiceTotal { get; set; }
    public int? Lines { get; set; }
    public string SchemaType { get; set; }
    public short? SchemaVersion { get; set; }
    public string GenerationCode { get; set; }
    public string ControlNumber { get; set; }
    public string Payload { get; set; }
    public short? ProcessingTypeId { get; set; }
    public short? ProcessingStatusId { get; set; }
    public int? RetryIn { get; set; }
    public int? SyncAttempts { get; set; }
    public DateTime? ProcessingDateTime { get; set; }
    public string ReceiptStamp { get; set; }
    public string ExternalUrl { get; set; }
    public string Notes { get; set; }

    public virtual Pos Pos { get; set; }
    public virtual Collection<InvoiceProcessingStatusLog> InvoiceSyncStatusLogs { get; set; }
    public virtual Collection<InvoiceProcessingLog> InvoiceProcessingLogs { get; set; }
    public virtual Collection<InvoiceFile> InvoiceFiles { get; set; }
    public virtual Collection<InvoiceNotification> InvoiceNotifications { get; set; }
}

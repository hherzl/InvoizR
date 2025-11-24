using InvoizR.SharedKernel.Mh.FeCcf;
using InvoizR.SharedKernel.Mh.FeFc;
using InvoizR.SharedKernel.Mh.FeNr;

namespace InvoizR.Clients.DataContracts.Invoices;

public record InvoiceDetailsModel
{
    public long? Id { get; set; }
    public short? FallbackId { get; set; }
    public short? PosId { get; set; }
    public string Pos { get; set; }

    public string CustomerId { get; set; }
    public string CustomerWtId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerCountryId { get; set; }
    public short? CustomerCountryLevelId { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerEmail { get; set; }
    public DateTime? CustomerLastUpdated { get; set; }
    public short? InvoiceTypeId { get; set; }
    public long? InvoiceNumber { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public decimal? InvoiceTotal { get; set; }
    public int? Lines { get; set; }
    public string SchemaType { get; set; }
    public short? SchemaVersion { get; set; }
    public string InvoiceGuid { get; set; }
    public string AuditNumber { get; set; }
    public string Payload { get; set; }
    public short? ProcessingStatusId { get; set; }
    public string ProcessingStatus { get; set; }
    public int? RetryIn { get; set; }
    public int? SyncAttempts { get; set; }
    public DateTime? EmitDateTime { get; set; }
    public string ReceiptStamp { get; set; }
    public string ExternalUrl { get; set; }
    public string Notes { get; set; }
    public DateTime? CreatedAt { get; set; }
    public bool Processed { get; set; }

    public List<InvoiceProcessingStatusLogItemModel> ProcessingStatusLogs { get; set; }
    public List<InvoiceProcessingLogItemModel> ProcessingLogs { get; set; }
    public List<InvoiceFileItemModel> Files { get; set; }
    public List<InvoiceNotificationItemModel> Notifications { get; set; }
}

public static class InvoiceDetailsExtensions
{
    public static bool IsDte01(this InvoiceDetailsModel detailsModel)
        => detailsModel.InvoiceTypeId == FeFcv1.TypeId;

    public static bool IsDte03(this InvoiceDetailsModel detailsModel)
        => detailsModel.InvoiceTypeId == FeCcfv3.TypeId;

    public static bool IsDte04(this InvoiceDetailsModel detailsModel)
        => detailsModel.InvoiceTypeId == FeNrv3.TypeId;
}

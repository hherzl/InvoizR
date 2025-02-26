using InvoizR.Domain.Enums;

namespace InvoizR.Domain.Entities;

public partial class InvoiceProcessingLog
{
    public static InvoiceProcessingLog CreateRequest(long? invoiceId, InvoiceProcessingStatus processingStatus, string content)
        => new()
        {
            InvoiceId = invoiceId,
            CreatedAt = DateTime.Now,
            ProcessingStatusId = (short)processingStatus,
            LogType = "REQUEST",
            ContentType = "application/json",
            Content = content
        };

    public static InvoiceProcessingLog CreateResponse(long? invoiceId, InvoiceProcessingStatus processingSyncStatus, string content)
        => new()
        {
            InvoiceId = invoiceId,
            CreatedAt = DateTime.Now,
            ProcessingStatusId = (short)processingSyncStatus,
            LogType = "RESPONSE",
            ContentType = "application/json",
            Content = content
        };
}

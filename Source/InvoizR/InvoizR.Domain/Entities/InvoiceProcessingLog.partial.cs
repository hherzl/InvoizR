using InvoizR.Domain.Enums;
using InvoizR.SharedKernel;
namespace InvoizR.Domain.Entities;

public partial class InvoiceProcessingLog
{
    public static InvoiceProcessingLog CreateRequest(long? invoiceId, InvoiceProcessingStatus processingStatus, string content)
        => new()
        {
            InvoiceId = invoiceId,
            CreatedAt = DateTime.Now,
            ProcessingStatusId = (short)processingStatus,
            LogType = Tokens.Request,
            ContentType = Tokens.ApplicationJson,
            Content = content
        };

    public static InvoiceProcessingLog CreateResponse(long? invoiceId, InvoiceProcessingStatus processingSyncStatus, string content)
        => new()
        {
            InvoiceId = invoiceId,
            CreatedAt = DateTime.Now,
            ProcessingStatusId = (short)processingSyncStatus,
            LogType = Tokens.Response,
            ContentType = Tokens.ApplicationJson,
            Content = content
        };
}

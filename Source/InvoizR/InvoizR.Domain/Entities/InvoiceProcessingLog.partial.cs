using InvoizR.Domain.Enums;
using InvoizR.SharedKernel;

namespace InvoizR.Domain.Entities;

public partial class InvoiceSyncLog
{
    public static InvoiceSyncLog CreateRequest(long? invoiceId, SyncStatus processingStatus, string content)
        => new()
        {
            InvoiceId = invoiceId,
            CreatedAt = DateTime.Now,
            SyncStatusId = (short)processingStatus,
            LogType = Tokens.Request,
            ContentType = Tokens.ApplicationJson,
            Content = content
        };

    public static InvoiceSyncLog CreateResponse(long? invoiceId, SyncStatus processingSyncStatus, string content)
        => new()
        {
            InvoiceId = invoiceId,
            CreatedAt = DateTime.Now,
            SyncStatusId = (short)processingSyncStatus,
            LogType = Tokens.Response,
            ContentType = Tokens.ApplicationJson,
            Content = content
        };
}

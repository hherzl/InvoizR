using InvoizR.Domain.Enums;
using InvoizR.Domain.Notifications;
using InvoizR.SharedKernel;

namespace InvoizR.Domain.Entities;

public partial class FallbackProcessingLog
{
    public static FallbackProcessingLog CreateRequest(short? fallbackId, InvoiceProcessingStatus syncStatus, string content)
        => new()
        {
            FallbackId = fallbackId,
            CreatedAt = DateTime.Now,
            SyncStatusId = (short)syncStatus,
            LogType = Tokens.Request,
            ContentType = Tokens.ApplicationJson,
            Content = content
        };

    public static FallbackProcessingLog CreateResponse(short? fallbackId, InvoiceProcessingStatus syncStatus, string content)
    {
        var entity = new FallbackProcessingLog
        {
            FallbackId = fallbackId,
            CreatedAt = DateTime.Now,
            SyncStatusId = (short)syncStatus,
            LogType = Tokens.Response,
            ContentType = Tokens.ApplicationJson,
            Content = content,
        };

        if (syncStatus == InvoiceProcessingStatus.Processed)
            entity.AddNotification(new ExportFallbackNotification(fallbackId));

        return entity;
    }
}

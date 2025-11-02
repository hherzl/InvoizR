using InvoizR.Domain.Enums;
using InvoizR.Domain.Notifications;

namespace InvoizR.Domain.Entities;

public partial class FallbackProcessingLog
{
    public static FallbackProcessingLog CreateRequest(short? fallbackId, InvoiceProcessingStatus syncStatus, string content)
        => new()
        {
            FallbackId = fallbackId,
            CreatedAt = DateTime.Now,
            SyncStatusId = (short)syncStatus,
            LogType = "REQUEST",
            ContentType = "application/json",
            Content = content
        };

    public static FallbackProcessingLog CreateResponse(short? fallbackId, InvoiceProcessingStatus syncStatus, string content)
    {
        var entity = new FallbackProcessingLog
        {
            FallbackId = fallbackId,
            CreatedAt = DateTime.Now,
            SyncStatusId = (short)syncStatus,
            LogType = "RESPONSE",
            ContentType = "application/json",
            Content = content,
        };

        if (syncStatus == InvoiceProcessingStatus.Processed)
            entity.AddNotification(new ExportFallbackNotification(fallbackId));

        return entity;
    }
}

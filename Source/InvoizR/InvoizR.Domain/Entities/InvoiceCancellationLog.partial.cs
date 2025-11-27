using InvoizR.Domain.Enums;
using InvoizR.Domain.Notifications;
using InvoizR.SharedKernel;

namespace InvoizR.Domain.Entities;

public partial class InvoiceCancellationLog
{
    public static InvoiceCancellationLog CreateRequest(long? invoiceId, string payload)
        => new()
        {
            InvoiceId = invoiceId,
            ProcessingStatusId = (short)SyncStatus.Requested,
            LogType = Tokens.Request,
            ContentType = Tokens.ApplicationJson,
            Payload = payload
        };

    public static InvoiceCancellationLog CreateResponse(long? invoiceId, string payload, SyncStatus syncStatus)
    {
        var entity = new InvoiceCancellationLog
        {
            InvoiceId = invoiceId,
            ProcessingStatusId = (short)syncStatus,
            LogType = Tokens.Response,
            ContentType = Tokens.ApplicationJson,
            Payload = payload
        };

        if (syncStatus == SyncStatus.Processed)
            entity.Notifications.Add(new CancelInvoiceNotification(invoiceId, payload));

        return entity;
    }
}

using MediatR;

namespace InvoizR.Domain.Notifications;

public class ExportFallbackNotification : INotification
{
    public ExportFallbackNotification(short? fallbackId)
    {
        Id = fallbackId;
    }

    public short? Id { get; }
}

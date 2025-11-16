using InvoizR.Application.Services.Models;
using InvoizR.Domain.Exceptions;

namespace InvoizR.Application.Services;

public sealed class WebhookNotificationHandler(WebhookNotificationHttpHandler httpHandler)
{
    public async Task HandleAsync(WebhookNotificationModel model, CancellationToken ct = default)
    {
        if (model.IsHttp())
            await httpHandler.HandleAsync(model, ct);
        else
            throw new NotSupportedWebhookProtocolException($"There is no implementation for '{model.WebhookNotificationProtocol}' protocol");
    }
}

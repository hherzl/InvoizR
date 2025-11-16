using System.Text;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Services.Models;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Exceptions;
using InvoizR.SharedKernel;

namespace InvoizR.Application.Services;

public sealed class WebhookNotificationHttpHandler(IInvoizRDbContext dbContext)
{
    public async Task HandleAsync(WebhookNotificationModel model, CancellationToken ct = default)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Accept", Tokens.ApplicationJson);

        HttpResponseMessage response = null;
        if (model.IsHttpPost())
            response = await client.PostAsync(model.WebhookNotificationAddress, GetStringContent(model.Invoice.ToJson()), ct);
        else if (model.IsHttpPut())
            response = await client.PutAsync(model.WebhookNotificationAddress, GetStringContent(model.Invoice.ToJson()), ct);
        else if (model.IsHttpPatch())
            response = await client.PatchAsync(model.WebhookNotificationAddress, GetStringContent(model.Invoice.ToJson()), ct);
        else
            throw new NotSupportedWebhookProtocolException($"There is no implementation for '{model.WebhookNotificationMisc1}' verb");

        var flag = response.IsSuccessStatusCode;
        var content = await response.Content.ReadAsStringAsync(ct);

        dbContext.InvoiceWebhookNotification.Add(new InvoiceWebhookNotification
        {
            InvoiceId = model.Invoice.InvoiceId,
            Protocol = model.WebhookNotificationProtocol,
            Address = model.WebhookNotificationAddress,
            ContentType = Tokens.ApplicationJson,
            IsSuccess = response.IsSuccessStatusCode,
            Request = model.Invoice.ToJson(),
            Response = content
        });

        await dbContext.SaveChangesAsync(ct);
    }

    private StringContent GetStringContent(string content)
        => new(content, Encoding.Default, Tokens.ApplicationJson);
}

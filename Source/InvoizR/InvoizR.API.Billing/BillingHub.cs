using Microsoft.AspNetCore.SignalR;

namespace InvoizR.API.Billing;

public class BillingHub : Hub
{
    [HubMethodName(HubMethods.SendInvoice)]
    public async Task SendInvoiceAsync(short invoiceTypeId, long invoiceNumber, decimal invoiceTotal)
    {
        await Clients.All.SendAsync(HubMethods.ReceiveInvoice, invoiceTypeId, invoiceNumber, invoiceTotal);
    }
}

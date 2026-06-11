using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts.Invoices;
using MediatR;

namespace InvoizR.Application.Features.Invoices;

public sealed class GetInvoiceQrQueryHandler(IInvoizRDbContext dbContext, IQrCodeGenerator qrCodeGenerator) : IRequestHandler<GetInvoiceQrQuery, InvoiceQrModel>
{
    public async Task<InvoiceQrModel> Handle(GetInvoiceQrQuery request, CancellationToken ct)
    {
        var entity = await dbContext.GetInvoiceAsync(request.Id, ct: ct);
        if (entity == null)
            return null;

        return new()
        {
            Qr = qrCodeGenerator.GetBytes(entity.ExternalUrl ?? "noexternalurl"),
            ContentType = "image/png",
            AuditNumber = entity.AuditNumber
        };
    }
}

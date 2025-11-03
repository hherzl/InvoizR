using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using MediatR;

namespace InvoizR.Application.Features.Invoices.Queries;

public class GetInvoiceQrQueryHandler : IRequestHandler<GetInvoiceQrQuery, InvoiceQrModel>
{
    private readonly IInvoizRDbContext _dbContext;
    private readonly IQrCodeGenerator _qrCodeGenerator;

    public GetInvoiceQrQueryHandler(IInvoizRDbContext dbContext, IQrCodeGenerator qrCodeGenerator)
    {
        _dbContext = dbContext;
        _qrCodeGenerator = qrCodeGenerator;
    }

    public async Task<InvoiceQrModel> Handle(GetInvoiceQrQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.GetInvoiceAsync(request.Id, ct: cancellationToken);
        if (entity == null)
            return null;

        return new()
        {
            Qr = _qrCodeGenerator.GetBytes(entity.ExternalUrl ?? "noexternalurl"),
            ContentType = "image/png",
            AuditNumber = entity.AuditNumber
        };
    }
}

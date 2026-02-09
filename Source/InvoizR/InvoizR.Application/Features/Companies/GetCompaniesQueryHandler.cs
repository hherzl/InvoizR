using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Companies;

public sealed class GetCompaniesQueryHandler(IInvoizRDbContext dbContext) : IRequestHandler<GetCompaniesQuery, ListResponse<CompanyItemModel>>
{
    public async Task<ListResponse<CompanyItemModel>> Handle(GetCompaniesQuery request, CancellationToken ct)
    {
        var query =
            from company in dbContext.Companies
            orderby company.Name
            select new CompanyItemModel
            {
                Id = company.Id,
                Environment = company.Environment,
                Name = company.Name,
                Code = company.Code,
                BusinessName = company.BusinessName,
                TaxIdNumber = company.TaxIdNumber,
                TaxpayerRegistrationNumber = company.TaxpayerRegistrationNumber
            };

        return new(await query.ToListAsync(ct));
    }
}

using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Cancellation;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.DataContracts.Dte01;
using InvoizR.Clients.DataContracts.Dte03;
using InvoizR.Clients.DataContracts.Dte04;
using InvoizR.Clients.DataContracts.Dte05;
using InvoizR.Clients.DataContracts.Dte06;
using InvoizR.Clients.DataContracts.Dte14;
using InvoizR.Clients.DataContracts.Fallback;
using InvoizR.Clients.DataContracts.ThirdPartyServices;

namespace InvoizR.Clients.Contracts;

public interface IInvoizRClient
{
    InvoizRClientSettings ClientSettings { get; }

    Task<ListResponse<ThirdPartyServiceItemModel>> GetThirdPartyServicesAsync(GetThirdPartyServicesQuery request);
    Task<SingleResponse<ThirdPartyServiceDetailsModel>> GetThirdPartyServiceAsync(short? id);

    Task<ListResponse<CompanyItemModel>> GetCompaniesAsync(GetCompaniesQuery request);
    Task<SingleResponse<CompanyDetailsModel>> GetCompanyAsync(short? id);
    Task<CreatedResponse<short?>> CreateCompanyAsync(CreateCompanyCommand request);

    Task<SingleResponse<BranchDetailsModel>> GetBranchAsync(short id);
    Task<CreatedResponse<short?>> CreateBranchAsync(CreateBranchCommand request);
    Task<CreatedResponse<short?>> AddNotificationToBranchAsync(AddNotificationToBranchCommand request);

    Task<ListResponse<ResponsibleItemModel>> GetResponsiblesAsync(GetResponsiblesQuery request);
    Task<CreatedResponse<short?>> CreateResponsibleAsync(CreateResponsibleCommand request);

    Task<PosDetailsModel> GetPosAsync(short id);
    Task<CreatedResponse<short?>> CreatePosAsync(CreatePosCommand request);

    Task<ListResponse<InvoiceTypeItemModel>> GetInvoiceTypesAsync(GetInvoiceTypesQuery request);
    Task<Response> SetInvoiceTypeAsCurrentAsync(SetInvoiceTypeAsCurrentCommand request);

    Task<GetInvoicesViewBagResponse> GetInvoicesViewBagAsync();
    Task<PagedResponse<InvoiceItemModel>> GetInvoicesAsync(GetInvoicesQuery request);
    Task<InvoiceDetailsModel> GetInvoiceAsync(long? id);
    Task<Response> ChangeProcessingStatusAsync(ChangeProcessingStatusCommand request);

    Task<CreatedResponse<long?>> CreateDte01InvoiceOWAsync(CreateDte01OWCommand request);
    Task<CreatedResponse<long?>> CreateDte01InvoiceRTAsync(CreateDte01RTCommand request);

    Task<CreatedResponse<long?>> CreateDte03InvoiceOWAsync(CreateDte03OWCommand request);
    Task<CreatedResponse<long?>> CreateDte03InvoiceRTAsync(CreateDte03RTCommand request);

    Task<CreatedResponse<long?>> CreateDte04OWAsync(CreateDte04OWCommand request);
    Task<CreatedResponse<long?>> CreateDte04RTAsync(CreateDte04RTCommand request);

    Task<CreatedResponse<long?>> CreateDte05OWAsync(CreateDte05OWCommand request);
    Task<CreatedResponse<long?>> CreateDte05RTAsync(CreateDte05RTCommand request);

    Task<CreatedResponse<long?>> CreateDte06OWAsync(CreateDte06OWCommand request);
    Task<CreatedResponse<long?>> CreateDte06RTAsync(CreateDte06RTCommand request);

    Task<CreatedResponse<long?>> CreateDte14OWAsync(CreateDte14OWCommand request);
    Task<CreatedResponse<long?>> CreateDte14RTAsync(CreateDte14RTCommand request);

    Task<Response> DteCancellationAsync(DteCancellationCommand request);

    Task<PagedResponse<FallbackItemModel>> GetFallbacksAsync(GetFallbacksQuery request);
    Task<SingleResponse<FallbackDetailsModel>> GetFallbackAsync(short? id);
    Task<CreatedResponse<short?>> CreateFallbackAsync(CreateFallbackCommand request);
    Task<Response> ProcessFallbackAsync(short id);
}

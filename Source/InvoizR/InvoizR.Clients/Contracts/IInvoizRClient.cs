﻿using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;

namespace InvoizR.Clients.Contracts;

public interface IInvoizRClient
{
    InvoizRClientSettings ClientSettings { get; }

    Task<ListResponse<CompanyItemModel>> GetCompaniesAsync(GetCompaniesQuery request);
    Task<CompanyDetailsModel> GetCompanyAsync(short? id);
    Task<CreatedResponse<short?>> CreateCompanyAsync(CreateCompanyCommand request);

    Task<BranchDetailsModel> GetBranchAsync(short id);
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

    Task<CreatedResponse<long?>> CreateDte01InvoiceOWAsync(CreateDte01InvoiceOWCommand request);
    Task<CreatedResponse<long?>> CreateDte01InvoiceRTAsync(CreateDte01InvoiceRTCommand request);

    Task<CreatedResponse<long?>> CreateDte03InvoiceOWAsync(CreateDte03InvoiceOWCommand request);
    Task<CreatedResponse<long?>> CreateDte03InvoiceRTAsync(CreateDte03InvoiceRTCommand request);
}

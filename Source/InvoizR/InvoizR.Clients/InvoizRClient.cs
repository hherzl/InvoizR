using System.Net;
using System.Text.Json;
using System.Web;
using InvoizR.Clients.Contracts;
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
using Microsoft.Extensions.Options;

namespace InvoizR.Clients;

public class InvoizRClient : Client, IInvoizRClient
{
    public InvoizRClient(IOptions<InvoizRClientSettings> options)
        : base()
    {
        ClientSettings = options.Value;

        InitClient(ClientSettings.Endpoint, "InvoizR HttpClient");
    }

    public InvoizRClientSettings ClientSettings { get; }

    public async Task<ListResponse<ThirdPartyServiceItemModel>> GetThirdPartyServicesAsync(GetThirdPartyServicesQuery request)
    {
        var queryString = HttpUtility.ParseQueryString(string.Empty);

        var response = await _httpClient.GetAsync($"third-party-service?{queryString}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ListResponse<ThirdPartyServiceItemModel>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<SingleResponse<ThirdPartyServiceDetailsModel>> GetThirdPartyServiceAsync(short? id)
    {
        var response = await _httpClient.GetAsync($"third-party-service/{id}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SingleResponse<ThirdPartyServiceDetailsModel>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<ListResponse<CompanyItemModel>> GetCompaniesAsync(GetCompaniesQuery request)
    {
        var queryString = HttpUtility.ParseQueryString(string.Empty);

        var response = await _httpClient.GetAsync($"company?{queryString}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ListResponse<CompanyItemModel>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<SingleResponse<CompanyDetailsModel>> GetCompanyAsync(short? id)
    {
        var response = await _httpClient.GetAsync($"company/{id}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SingleResponse<CompanyDetailsModel>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<short?>> CreateCompanyAsync(CreateCompanyCommand request)
    {
        var response = await _httpClient.PostAsync($"company", ContentHelper.Create(request.ToJson()));
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CreatedResponse<short?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<SingleResponse<BranchDetailsModel>> GetBranchAsync(short id)
    {
        var response = await _httpClient.GetAsync($"branch/{id}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SingleResponse<BranchDetailsModel>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<short?>> CreateBranchAsync(CreateBranchCommand request)
    {
        var response = await _httpClient.PostAsync($"branch", ContentHelper.Create(request.ToJson()));
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CreatedResponse<short?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<short?>> AddNotificationToBranchAsync(AddNotificationToBranchCommand request)
    {
        var response = await _httpClient.PostAsync($"branch/add-notification", ContentHelper.Create(request.ToJson()));
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CreatedResponse<short?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<ListResponse<ResponsibleItemModel>> GetResponsiblesAsync(GetResponsiblesQuery request)
    {
        var queryString = HttpUtility.ParseQueryString(string.Empty);

        var response = await _httpClient.GetAsync($"responsible?{queryString.ToString()}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ListResponse<ResponsibleItemModel>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<short?>> CreateResponsibleAsync(CreateResponsibleCommand request)
    {
        var queryString = HttpUtility.ParseQueryString(string.Empty);

        if (request.CompanyId != null)
            queryString.Add("posId", request.CompanyId.ToString());

        var response = await _httpClient.PostAsync($"responsible", ContentHelper.Create(request.ToJson()));
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CreatedResponse<short?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<PosDetailsModel> GetPosAsync(short id)
    {
        var response = await _httpClient.GetAsync($"pos/{id}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PosDetailsModel>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<short?>> CreatePosAsync(CreatePosCommand request)
    {
        var response = await _httpClient.PostAsync($"pos", ContentHelper.Create(request.ToJson()));
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CreatedResponse<short?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<ListResponse<InvoiceTypeItemModel>> GetInvoiceTypesAsync(GetInvoiceTypesQuery request)
    {
        var response = await _httpClient.GetAsync($"invoice-type");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ListResponse<InvoiceTypeItemModel>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<Response> SetInvoiceTypeAsCurrentAsync(SetInvoiceTypeAsCurrentCommand request)
    {
        var response = await _httpClient.PostAsync($"invoice-type/{request.Id}/set-as-current", ContentHelper.CreateEmpty());
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Response>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<GetInvoicesViewBagResponse> GetInvoicesViewBagAsync()
    {
        var response = await _httpClient.GetAsync($"invoice-viewbag");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<GetInvoicesViewBagResponse>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<PagedResponse<InvoiceItemModel>> GetInvoicesAsync(GetInvoicesQuery request)
    {
        var queryString = HttpUtility.ParseQueryString(string.Empty);

        queryString.Add("pageSize", request.PageSize.ToString());
        queryString.Add("pageNumber", request.PageNumber.ToString());

        if (request.PosId != null)
            queryString.Add("posId", request.PosId.ToString());

        if (request.InvoiceTypeId != null)
            queryString.Add("invoiceTypeId", request.InvoiceTypeId.ToString());

        if (request.ProcessingTypeId != null)
            queryString.Add("processingTypeId", request.ProcessingTypeId.ToString());

        if (request.SyncStatusId != null)
            queryString.Add("syncStatusId", request.SyncStatusId.ToString());

        var response = await _httpClient.GetAsync($"invoice?{queryString.ToString()}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PagedResponse<InvoiceItemModel>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<InvoiceDetailsModel> GetInvoiceAsync(long? id)
    {
        var response = await _httpClient.GetAsync($"invoice/{id}");
        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<InvoiceDetailsModel>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<Response> ChangeProcessingStatusAsync(ChangeProcessingStatusCommand request)
    {
        var response = await _httpClient.PostAsync($"invoice/change-processing-status", ContentHelper.Create(request.ToJson()));
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Response>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<long?>> CreateDte01InvoiceOWAsync(CreateDte01OWCommand request)
    {
        var response = await _httpClient.PostAsync($"dte01-ow", ContentHelper.Create(request.ToJson()));
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CreatedResponse<long?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<long?>> CreateDte01InvoiceRTAsync(CreateDte01RTCommand request)
    {
        var response = await _httpClient.PostAsync($"dte01-rt", ContentHelper.Create(request.ToJson()));
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CreatedResponse<long?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<long?>> CreateDte03InvoiceOWAsync(CreateDte03OWCommand request)
    {
        var response = await _httpClient.PostAsync($"dte03-ow", ContentHelper.Create(request.ToJson()));
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CreatedResponse<long?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<long?>> CreateDte03InvoiceRTAsync(CreateDte03RTCommand request)
    {
        var response = await _httpClient.PostAsync($"dte03-rt", ContentHelper.Create(request.ToJson()));
        var responseContent = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<CreatedResponse<long?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<long?>> CreateDte04OWAsync(CreateDte04OWCommand request)
    {
        var response = await _httpClient.PostAsync($"dte04-ow", ContentHelper.Create(request.ToJson()));
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CreatedResponse<long?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<long?>> CreateDte04RTAsync(CreateDte04RTCommand request)
    {
        var response = await _httpClient.PostAsync($"dte04-rt", ContentHelper.Create(request.ToJson()));
        var responseContent = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<CreatedResponse<long?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<long?>> CreateDte05RTAsync(CreateDte05RTCommand request)
    {
        var response = await _httpClient.PostAsync($"dte05-rt", ContentHelper.Create(request.ToJson()));
        var responseContent = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<CreatedResponse<long?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<long?>> CreateDte05OWAsync(CreateDte05OWCommand request)
    {
        var response = await _httpClient.PostAsync($"dte05-ow", ContentHelper.Create(request.ToJson()));
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CreatedResponse<long?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<long?>> CreateDte06RTAsync(CreateDte06RTCommand request)
    {
        var response = await _httpClient.PostAsync($"dte06-rt", ContentHelper.Create(request.ToJson()));
        var responseContent = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<CreatedResponse<long?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<long?>> CreateDte06OWAsync(CreateDte06OWCommand request)
    {
        var response = await _httpClient.PostAsync($"dte06-ow", ContentHelper.Create(request.ToJson()));
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CreatedResponse<long?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<long?>> CreateDte14OWAsync(CreateDte14OWCommand request)
    {
        var response = await _httpClient.PostAsync($"dte14-ow", ContentHelper.Create(request.ToJson()));
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CreatedResponse<long?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<long?>> CreateDte14RTAsync(CreateDte14RTCommand request)
    {
        var response = await _httpClient.PostAsync($"dte14-rt", ContentHelper.Create(request.ToJson()));
        var responseContent = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<CreatedResponse<long?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<Response> DteCancellationAsync(DteCancellationCommand request)
    {
        var response = await _httpClient.PostAsync($"dte-cancellation", ContentHelper.Create(request.ToJson()));
        var responseContent = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<Response>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<PagedResponse<FallbackItemModel>> GetFallbacksAsync(GetFallbacksQuery request)
    {
        var queryString = HttpUtility.ParseQueryString(string.Empty);

        queryString.Add("pageSize", request.PageSize.ToString());
        queryString.Add("pageNumber", request.PageNumber.ToString());

        var response = await _httpClient.GetAsync($"fallback?{queryString.ToString()}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PagedResponse<FallbackItemModel>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<SingleResponse<FallbackDetailsModel>> GetFallbackAsync(short? id)
    {
        var response = await _httpClient.GetAsync($"fallback/{id}");
        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SingleResponse<FallbackDetailsModel>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<short?>> CreateFallbackAsync(CreateFallbackCommand request)
    {
        var response = await _httpClient.PostAsync($"fallback", ContentHelper.Create(request.ToJson()));
        var responseContent = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<CreatedResponse<short?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<Response> ProcessFallbackAsync(short id)
    {
        var response = await _httpClient.PutAsync($"fallback/{id}/process", ContentHelper.CreateEmpty());
        var responseContent = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<CreatedResponse<short?>>(responseContent, DefaultJsonSerializerOpts);
    }
}

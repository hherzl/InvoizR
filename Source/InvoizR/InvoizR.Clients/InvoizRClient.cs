using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;
using InvoizR.Clients.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using Microsoft.Extensions.Options;

namespace InvoizR.Clients;

public class InvoizRClient : IInvoizRClient
{
    private const string APPLICATION_JSON = "application/JSON";
    private const string USER_AGENT = "User-Agent";

    private readonly HttpClient _httpClient;

    public InvoizRClient(IOptions<InvoizRClientSettings> options)
    {
        ClientSettings = options.Value;

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(ClientSettings.Endpoint),
            DefaultRequestHeaders =
            {
                Accept =
                {
                    new MediaTypeWithQualityHeaderValue(APPLICATION_JSON)
                }
            }
        };

        _httpClient.DefaultRequestHeaders.Add(USER_AGENT, "InvoizR HttpClient");
    }

    protected JsonSerializerOptions DefaultJsonSerializerOpts
        => new() { PropertyNameCaseInsensitive = true, WriteIndented = true };

    public InvoizRClientSettings ClientSettings { get; }

    public async Task<ListResponse<CompanyItemModel>> GetCompaniesAsync(GetCompaniesQuery request)
    {
        var qs = HttpUtility.ParseQueryString(string.Empty);

        var response = await _httpClient.GetAsync($"company?{qs.ToString()}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ListResponse<CompanyItemModel>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CompanyDetailsModel> GetCompanyAsync(short? id)
    {
        var response = await _httpClient.GetAsync($"company/{id}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<CompanyDetailsModel>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<short?>> CreateCompanyAsync(CreateCompanyCommand request)
    {
        var response = await _httpClient.PostAsync($"company", ContentHelper.Create(request.ToJson()));
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CreatedResponse<short?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<BranchDetailsModel> GetBranchAsync(short id)
    {
        var response = await _httpClient.GetAsync($"branch/{id}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<BranchDetailsModel>(responseContent, DefaultJsonSerializerOpts);
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
        var qs = HttpUtility.ParseQueryString(string.Empty);

        var response = await _httpClient.GetAsync($"responsible?{qs.ToString()}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ListResponse<ResponsibleItemModel>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<short?>> CreateResponsibleAsync(CreateResponsibleCommand request)
    {
        var qs = HttpUtility.ParseQueryString(string.Empty);

        if (request.CompanyId != null)
            qs.Add("posId", request.CompanyId.ToString());

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
        var qs = HttpUtility.ParseQueryString(string.Empty);

        qs.Add("pageSize", request.PageSize.ToString());
        qs.Add("pageNumber", request.PageNumber.ToString());

        if (request.PosId != null)
            qs.Add("posId", request.PosId.ToString());

        if (request.ProcessingStatusId != null)
            qs.Add("processingStatusId", request.ProcessingStatusId.ToString());

        var response = await _httpClient.GetAsync($"invoice?{qs.ToString()}");
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

    public async Task<CreatedResponse<long?>> CreateDte01InvoiceOWAsync(CreateDte01InvoiceOWCommand request)
    {
        var response = await _httpClient.PostAsync($"dte01-ow", ContentHelper.Create(request.ToJson()));
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CreatedResponse<long?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<long?>> CreateDte01InvoiceRTAsync(CreateDte01InvoiceRTCommand request)
    {
        var response = await _httpClient.PostAsync($"dte01-rt", ContentHelper.Create(request.ToJson()));
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CreatedResponse<long?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<long?>> CreateDte03InvoiceOWAsync(CreateDte03InvoiceOWCommand request)
    {
        var response = await _httpClient.PostAsync($"dte03-ow", ContentHelper.Create(request.ToJson()));
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CreatedResponse<long?>>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<CreatedResponse<long?>> CreateDte03InvoiceRTAsync(CreateDte03InvoiceRTCommand request)
    {
        var response = await _httpClient.PostAsync($"dte03-rt", ContentHelper.Create(request.ToJson()));
        var responseContent = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<CreatedResponse<long?>>(responseContent, DefaultJsonSerializerOpts);
    }
}

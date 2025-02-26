namespace InvoizR.Clients.DataContracts.Common;

public record DetailsResponse<TModel> : Response
{
    public TModel Model { get; set; }
}

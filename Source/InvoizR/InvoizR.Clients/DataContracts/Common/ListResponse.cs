namespace InvoizR.Clients.DataContracts.Common;

public record ListResponse<TModel> : Response
{
    public ListResponse()
    {
    }

    public ListResponse(IEnumerable<TModel> model)
    {
        Model = model;
    }

    public IEnumerable<TModel> Model { get; set; }
}

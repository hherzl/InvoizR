namespace InvoizR.Clients.DataContracts.Common;

public record CreatedResponse<TKey> : Response
{
    public CreatedResponse()
    {
    }

    public CreatedResponse(TKey id)
    {
        Id = id;
    }

    public TKey Id { get; set; }
}

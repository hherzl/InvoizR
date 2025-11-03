namespace InvoizR.Clients.DataContracts.ThirdPartyServices;

public record ThirdPartyServiceItemModel
{
    public short? Id { get; set; }
    public string EnvironmentId { get; set; }
    public string Name { get; set; }
}

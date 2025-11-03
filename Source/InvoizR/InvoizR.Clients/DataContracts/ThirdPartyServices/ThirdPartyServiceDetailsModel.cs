namespace InvoizR.Clients.DataContracts.ThirdPartyServices;

public record ThirdPartyServiceDetailsModel
{
    public ThirdPartyServiceDetailsModel()
    {
        Parameters = [];
    }

    public short? Id { get; set; }
    public string EnvironmentId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public List<ThirdPartyServiceParameterItemModel> Parameters { get; set; }
}

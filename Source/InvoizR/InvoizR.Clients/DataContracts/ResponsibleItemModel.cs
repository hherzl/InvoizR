using InvoizR.SharedKernel.Mh;

namespace InvoizR.Clients.DataContracts;

public record ResponsibleItemModel
{
    public short? Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string IdType { get; set; }
    public string IdNumber { get; set; }
    public bool? AuthorizeCancellation { get; set; }
    public bool? AuthorizeFallback { get; set; }

    public string IdTypeDesc
        => MhCatalog.Cat022.Desc(IdType);
}

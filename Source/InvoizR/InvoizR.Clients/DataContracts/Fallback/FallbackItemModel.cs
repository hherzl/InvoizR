namespace InvoizR.Clients.DataContracts.Fallback;

public record FallbackItemModel
{
    public short? Id { get; set; }
    public short? CompanyId { get; set; }
    public string Company { get; set; }
    public string Name { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public bool? Enable { get; set; }
    public string FallbackGuid { get; set; }
    public short? SyncStatusId { get; set; }
}

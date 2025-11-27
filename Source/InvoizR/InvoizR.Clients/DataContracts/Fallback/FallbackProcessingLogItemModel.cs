namespace InvoizR.Clients.DataContracts.Fallback;

public record FallbackProcessingLogItemModel
{
    public DateTime? CreatedAt { get; set; }
    public short? SyncStatusId { get; set; }
    public string SyncStatus { get; set; }
    public string LogType { get; set; }
    public string ContentType { get; set; }
    public string Content { get; set; }
}

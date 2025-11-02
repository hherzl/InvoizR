namespace InvoizR.Clients.DataContracts.Fallback;

public record FallbackProcessingLogItemModel
{
    public DateTime? CreatedAt { get; set; }
    public short? ProcessingStatusId { get; set; }
    public string ProcessingStatus { get; set; }
    public string LogType { get; set; }
    public string ContentType { get; set; }
    public string Content { get; set; }
}

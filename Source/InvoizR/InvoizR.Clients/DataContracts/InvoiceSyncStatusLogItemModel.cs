﻿namespace InvoizR.Clients.DataContracts;

public record InvoiceProcessingStatusLogItemModel
{
    public int? Id { get; set; }
    public long? InvoiceId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public short? ProcessingStatusId { get; set; }
    public string ProcessingStatus { get; set; }
}

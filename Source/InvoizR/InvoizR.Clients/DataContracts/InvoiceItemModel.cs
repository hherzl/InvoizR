namespace InvoizR.Clients.DataContracts;

public record InvoiceItemModel
{
    public long? Id { get; set; }
    public short? BranchId { get; set; }
    public string Branch { get; set; }
    public short? PosId { get; set; }
    public string Pos { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public short? InvoiceTypeId { get; set; }
    public string InvoiceType { get; set; }
    public long? InvoiceNumber { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public decimal? InvoiceTotal { get; set; }
    public string ControlNumber { get; set; }
    public short? ProcessingStatusId { get; set; }
    public string ProcessingStatus { get; set; }
    public string Serialization { get; set; }
}

namespace InvoizR.Application.Reports.Templates.Common;

public record ReferencedDocumentModel
{
    public string Type { get; set; }
    public string GenerationType { get; set; }
    public string DocumentNumber { get; set; }
    public DateTime? EmitDate { get; set; }
}

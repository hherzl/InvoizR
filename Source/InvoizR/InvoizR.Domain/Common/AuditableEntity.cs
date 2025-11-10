namespace InvoizR.Domain.Common;

public abstract class AuditableEntity : Entity
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public string LastModifiedBy { get; set; }

    public byte[] RowVersion { get; set; }
}

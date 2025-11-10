using System.Collections.ObjectModel;
using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

public partial class Pos : AuditableEntity
{
    public Pos()
    {
    }

    public Pos(short? id)
    {
        Id = id;
    }

    public short? Id { get; set; }
    public short? BranchId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string TaxAuthId { get; set; }
    public string Description { get; set; }

    public virtual Branch Branch { get; set; }
    public virtual Collection<Invoice> Invoices { get; set; }
}

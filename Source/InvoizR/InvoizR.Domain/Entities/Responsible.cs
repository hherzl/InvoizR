using System.Collections.ObjectModel;

namespace InvoizR.Domain.Entities;

public partial class Responsible
{
    public Responsible()
    {
    }

    public Responsible(short? id)
    {
        Id = id;
    }

    public short? Id { get; set; }
    public short? CompanyId { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string IdType { get; set; }
    public string IdNumber { get; set; }
    public bool? AuthorizeCancellation { get; set; }
    public bool? AuthorizeFallback { get; set; }

    public virtual Collection<Branch> Branches { get; set; }
}

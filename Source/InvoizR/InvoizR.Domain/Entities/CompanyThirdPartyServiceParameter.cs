using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

public partial class CompanyThirdPartyServiceParameter : AuditableEntity
{
    public CompanyThirdPartyServiceParameter()
    {
    }

    public CompanyThirdPartyServiceParameter(short? id)
    {
        Id = id;
    }

    public short? Id { get; set; }
    public short? CompanyId { get; set; }
    public short? ThirdPartyServiceId { get; set; }
    public string EnvironmentId { get; set; }
    public string Category { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }

    public virtual Company Company { get; set; }
    public virtual ThirdPartyService ThirdPartyService { get; set; }
}

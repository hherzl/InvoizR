using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts;

public record CreateBranchCommand : IRequest<CreatedResponse<short?>>, IValidatableObject
{
    public short? CompanyId { get; set; }
    public string Name { get; set; }
    public string EstablishmentPrefix { get; set; }
    public string TaxAuthId { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Logo { get; set; }
    public int? Headquarters { get; set; }
    public short? ResponsibleId { get; set; }

    public bool HasLogo
        => !string.IsNullOrEmpty(Logo);

    public string ToJson()
        => JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (CompanyId == null)
            yield return new("Company is required", [nameof(CompanyId)]);

        if (string.IsNullOrEmpty(Name))
            yield return new("Name is required", [nameof(Name)]);

        if (string.IsNullOrEmpty(EstablishmentPrefix))
            yield return new("EstablishmentPrefix is required", [nameof(EstablishmentPrefix)]);

        if (string.IsNullOrEmpty(TaxAuthId))
            yield return new("TaxAuthId is required", [nameof(TaxAuthId)]);
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts;

public record CreatePosCommand : IRequest<CreatedResponse<short?>>, IValidatableObject
{
    public short? BranchId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string TaxAuthId { get; set; }
    public string Description { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (BranchId == null)
            yield return new("Branch is required", [nameof(BranchId)]);

        if (string.IsNullOrEmpty(Name))
            yield return new("Name is required", [nameof(Name)]);

        if (string.IsNullOrEmpty(Code))
            yield return new("Code is required", [nameof(Code)]);

        if (string.IsNullOrEmpty(TaxAuthId))
            yield return new("Code is required", [nameof(TaxAuthId)]);
    }
}

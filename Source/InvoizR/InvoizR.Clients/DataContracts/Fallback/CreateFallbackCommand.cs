using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.SharedKernel.Mh.Contingencia;
using MediatR;

namespace InvoizR.Clients.DataContracts.Fallback;

public record CreateFallbackCommand : Request, IRequest<CreatedResponse<short?>>, IValidatableObject
{
    public short? CompanyId { get; set; }
    public string Name { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }

    public Contingenciav3 Contingencia { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (CompanyId == null)
            yield return new ValidationResult("CompanyId is required", [nameof(CompanyId)]);

        if (string.IsNullOrEmpty(Name))
            yield return new ValidationResult("Name is required", [nameof(Name)]);

        if (StartDateTime == null)
            yield return new ValidationResult("StartDateTime is required", [nameof(StartDateTime)]);

        if (Contingencia == null)
            yield return new ValidationResult("Contingencia is required", [nameof(Contingencia)]);
    }
}

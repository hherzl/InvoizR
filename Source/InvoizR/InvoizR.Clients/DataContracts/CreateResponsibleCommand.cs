using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts;

public record CreateResponsibleCommand : BaseCommand, IRequest<CreatedResponse<short?>>
{
    public short? CompanyId { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string IdType { get; set; }
    public string IdNumber { get; set; }
    public bool? AuthorizeCancellation { get; set; }
    public bool? AuthorizeContingency { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (CompanyId == null)
            yield return new ValidationResult("CompanyId is required", [nameof(CompanyId)]);

        if (string.IsNullOrEmpty(Name))
            yield return new ValidationResult("Name is required", [nameof(Name)]);

        if (string.IsNullOrEmpty(Phone))
            yield return new ValidationResult("Phone is required", [nameof(Phone)]);

        if (string.IsNullOrEmpty(Email))
            yield return new ValidationResult("Email is required", [nameof(Email)]);

        if (string.IsNullOrEmpty(IdType))
            yield return new ValidationResult("IdType is required", [nameof(IdType)]);

        if (string.IsNullOrEmpty(IdNumber))
            yield return new ValidationResult("IdNumber is required", [nameof(IdNumber)]);

        if (AuthorizeCancellation == null)
            yield return new ValidationResult("AuthorizeCancellation is required", [nameof(AuthorizeCancellation)]);

        if (AuthorizeContingency == null)
            yield return new ValidationResult("AuthorizeContingency is required", [nameof(AuthorizeContingency)]);
    }
}

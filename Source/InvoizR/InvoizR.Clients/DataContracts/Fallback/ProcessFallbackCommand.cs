using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts.Fallback;

public record ProcessFallbackCommand : Request, IRequest<Response>, IValidatableObject
{
    public ProcessFallbackCommand(short id)
    {
        Id = id;
    }

    public short? Id { get; }

    public string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Id == null)
            yield return new ValidationResult("Id is required", [nameof(Id)]);
    }
}

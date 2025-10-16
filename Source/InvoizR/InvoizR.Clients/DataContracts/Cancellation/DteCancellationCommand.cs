using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts.Cancellation;

public record DteCancellationCommand : Request, IRequest<Response>, IValidatableObject
{
    public long? InvoiceId { get; set; }
    public string Anulacion { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (InvoiceId == null)
            yield return new("Invoice ID is required", [nameof(InvoiceId)]);

        if (string.IsNullOrEmpty(Anulacion))
            yield return new("Anulacion (JSON) is required", [nameof(Anulacion)]);
    }
}

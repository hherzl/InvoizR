using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts;

public record ChangeProcessingStatusCommand : IRequest<Response>, IValidatableObject
{
    public long? InvoiceId { get; set; }
    public short? ProcessingStatusId { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (InvoiceId == null)
            yield return new("Invoice is required", [nameof(InvoiceId)]);

        if (ProcessingStatusId == null)
            yield return new("Processing Status is required", [nameof(ProcessingStatusId)]);
    }
}

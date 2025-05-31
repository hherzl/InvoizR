using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.SharedKernel.Mh.FeCcf;
using MediatR;

namespace InvoizR.Clients.DataContracts;

public record CreateDte03InvoiceCommand : Request, IRequest<CreatedResponse<long?>>, IValidatableObject
{
    public CreateDte03InvoiceCommand()
    {
        Customer = new();
    }

    public short? PosId { get; set; }

    public CustomerNodeModel Customer { get; set; }

    public short? InvoiceTypeId { get; set; }
    public long? InvoiceNumber { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public decimal? InvoiceTotal { get; set; }
    public int? Lines { get; set; }

    public FeCcfv3 Dte { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (InvoiceTypeId == null)
            yield return new ValidationResult("InvoiceNumber is required", [nameof(InvoiceTypeId)]);

        if (InvoiceNumber == null)
            yield return new ValidationResult("InvoiceNumber is required", [nameof(InvoiceNumber)]);

        if (InvoiceDate == null)
            yield return new ValidationResult("InvoiceDate is required", [nameof(InvoiceDate)]);

        if (InvoiceTotal == null)
            yield return new ValidationResult("InvoiceTotal is required", [nameof(InvoiceTotal)]);
    }
}

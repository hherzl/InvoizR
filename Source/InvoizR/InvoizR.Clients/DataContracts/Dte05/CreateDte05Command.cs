using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.DataContracts.Invoices;
using InvoizR.SharedKernel.Mh.FeNc;
using MediatR;

namespace InvoizR.Clients.DataContracts.Dte05;

public record CreateDte05Command : Request, IRequest<CreatedInvoiceResponse>, IValidatableObject
{
    public CreateDte05Command()
    {
        Customer = new();
    }

    public short? PosId { get; set; }

    public CustomerNodeModel Customer { get; set; }

    public long? InvoiceNumber { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public decimal? InvoiceTotal { get; set; }
    public int? Lines { get; set; }

    public FeNcv3 Dte { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Customer == null)
        {
            yield return new ValidationResult("Customer is required", [nameof(Customer)]);
        }
        else
        {
            if (string.IsNullOrEmpty(Customer.Id))
                yield return new ValidationResult("Customer ID is required", [nameof(Customer.Id)]);

            if (string.IsNullOrEmpty(Customer.WtId))
                yield return new ValidationResult("Customer Wt ID is required", [nameof(Customer.WtId)]);

            if (string.IsNullOrEmpty(Customer.Name))
                yield return new ValidationResult("Customer name is required", [nameof(Customer.Name)]);

            if (string.IsNullOrEmpty(Customer.CountryId))
                yield return new ValidationResult("Customer country is required", [nameof(Customer.CountryId)]);

            if (Customer.CountryLevelId == null)
                yield return new ValidationResult("Customer country level is required", [nameof(Customer.CountryLevelId)]);

            if (Customer.Address == null)
                yield return new ValidationResult("Customer address is required", [nameof(Customer.Address)]);

            if (Customer.Phone == null)
                yield return new ValidationResult("Customer phone is required", [nameof(Customer.Phone)]);

            if (Customer.Email == null)
                yield return new ValidationResult("Customer email is required", [nameof(Customer.Email)]);
        }

        if (InvoiceNumber == null)
            yield return new ValidationResult("InvoiceNumber is required", [nameof(InvoiceNumber)]);

        if (InvoiceDate == null)
            yield return new ValidationResult("InvoiceDate is required", [nameof(InvoiceDate)]);

        if (InvoiceTotal == null)
            yield return new ValidationResult("InvoiceTotal is required", [nameof(InvoiceTotal)]);

        if (Dte == null)
            yield return new ValidationResult("DTE is required", [nameof(Dte)]);
    }
}

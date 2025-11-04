using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts;

public record CreateCompanyCommand : IRequest<CreatedResponse<short?>>, IValidatableObject
{
    public CreateCompanyCommand()
    {
    }

    public string Environment { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string BusinessName { get; set; }
    public string TaxIdNumber { get; set; }
    public string TaxpayerRegistrationNumber { get; set; }
    public string EconomicActivityId { get; set; }
    public string EconomicActivity { get; set; }
    public short? CountryLevelId { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Logo { get; set; }
    public int? Headquarters { get; set; }
    public string NonCustomerEmail { get; set; }

    public bool HasLogo
        => !string.IsNullOrEmpty(Logo);

    public string ToJson()
        => JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Name))
            yield return new("Name is required", [nameof(Name)]);

        if (string.IsNullOrEmpty(Code))
            yield return new("Code is required", [nameof(Code)]);

        if (string.IsNullOrEmpty(BusinessName))
            yield return new("Name is required", [nameof(BusinessName)]);

        if (string.IsNullOrEmpty(TaxIdNumber))
            yield return new("TaxIdNumber is required", [nameof(TaxIdNumber)]);

        if (string.IsNullOrEmpty(TaxpayerRegistrationNumber))
            yield return new("TaxpayerRegistrationNumber is required", [nameof(TaxpayerRegistrationNumber)]);
    }
}

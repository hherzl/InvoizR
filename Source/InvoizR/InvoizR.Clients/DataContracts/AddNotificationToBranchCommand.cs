using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts;

public record AddNotificationToBranchCommand : IRequest<CreatedResponse<short?>>, IValidatableObject
{
    public AddNotificationToBranchCommand()
    {
    }

    public AddNotificationToBranchCommand(short? branchId, short? invoiceTypeId, string email, bool bcc)
    {
        BranchId = branchId;
        InvoiceTypeId = invoiceTypeId;
        Email = email;
        Bcc = bcc;
    }

    public short? BranchId { get; set; }
    public short? InvoiceTypeId { get; set; }
    public string Email { get; set; }
    public bool? Bcc { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (BranchId == null)
            yield return new("Branch is required", [nameof(BranchId)]);

        if (InvoiceTypeId == null)
            yield return new("InvoiceType is required", [nameof(InvoiceTypeId)]);

        if (string.IsNullOrEmpty(Email))
            yield return new("Email is required", [nameof(Email)]);
    }
}

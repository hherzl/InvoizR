namespace InvoizR.Domain.Entities;

public partial class Invoice
{
    public bool HasCustomerEmail
        => !string.IsNullOrEmpty(CustomerEmail);
}

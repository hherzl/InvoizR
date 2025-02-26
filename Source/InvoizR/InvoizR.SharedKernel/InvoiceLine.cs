namespace InvoizR.SharedKernel;

public record InvoiceLine
{
    public InvoiceLine(string code, string description, double price, double quantity)
    {
        Code = code;
        Description = description;
        Price = price;
        Quantity = quantity;

        Total = Price * Quantity;
    }

    public string Code;
    public string Description;
    public double Price;
    public double Quantity;

    public double Total { get; }

    public double VatSum { get; set; }
}

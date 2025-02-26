namespace InvoizR.Client.CapsuleCorp.Data;

public partial class Product
{
    public Product()
    {
    }

    public Product(string code, string description, decimal price, DateTime releaseDate, string category)
    {
        Code = code;
        Description = description;
        Price = price;
        ReleaseDate = releaseDate;
        Category = category;
    }

    public string Code { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Category { get; set; }
}

public partial class Product
{
    public static Product Clone(Product source)
    {
        return new()
        {
            Code = source.Code,
            Description = source.Description,
            Price = source.Price,
            ReleaseDate = source.ReleaseDate,
            Category = source.Category
        };
    }
}

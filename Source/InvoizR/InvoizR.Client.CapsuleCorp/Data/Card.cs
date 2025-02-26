namespace InvoizR.Client.CapsuleCorp.Data;

public partial class Card
{
    public Card()
    {
    }

    public Card(string id, short? cardTypeId, string wtId, string name, string countryId, short? countryLevelId, string address, string phone, string email)
    {
        Id = id;
        CardTypeId = cardTypeId;
        WtId = wtId;
        Name = name;
        CountryId = countryId;
        CountryLevelId = countryLevelId;
        Address = address;
        Phone = phone;
        Email = email;
    }

    public string Id { get; set; }
    public short? CardTypeId { get; set; }
    public string DocumentTypeId { get; set; }
    public string DocumentNumber { get; set; }
    public string TaxId { get; set; }
    public string WtId { get; set; }
    public string Name { get; set; }
    public string CountryId { get; set; }
    public short? CountryLevelId { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public bool HasWt
        => !string.IsNullOrEmpty(WtId);
}

public partial class Card
{
    public static Card CreatePerson
    (
        string id,
        string documentTypeId,
        string documentNumber,
        string name,
        string countryId,
        short? countryLevelId,
        string address,
        string phone,
        string email,
        string taxId = null,
        string wtId = null
    )
    {
        return new()
        {
            CardTypeId = (short)CardType.Person,
            Id = id,
            DocumentTypeId = documentTypeId,
            DocumentNumber = documentNumber,
            Name = name,
            CountryId = countryId,
            CountryLevelId = countryLevelId,
            Address = address,
            Phone = phone,
            Email = email,
            TaxId = taxId,
            WtId = wtId
        };
    }

    public static Card CreateCompany
    (
        string id,
        string documentTypeId,
        string documentNumber,
        string name,
        string countryId,
        short? countryLevelId,
        string address,
        string phone,
        string email,
        string taxId = null,
        string wtId = null
    )
    {
        return new()
        {
            CardTypeId = (short)CardType.Company,
            Id = id,
            DocumentTypeId = documentTypeId,
            DocumentNumber = documentNumber,
            Name = name,
            CountryId = countryId,
            CountryLevelId = countryLevelId,
            Address = address,
            Phone = phone,
            Email = email,
            TaxId = taxId,
            WtId = wtId
        };
    }

    public static Card CreateWalkIn(string id, string name, string countryId, short? countryLevelId, string address, string phone, string email)
    {
        return new()
        {
            CardTypeId = (short)CardType.WalkIn,
            Id = id,
            Name = name,
            CountryId = countryId,
            CountryLevelId = countryLevelId,
            Address = address,
            Phone = phone,
            Email = email,
        };
    }
}

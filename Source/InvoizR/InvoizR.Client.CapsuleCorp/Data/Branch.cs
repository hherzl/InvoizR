namespace InvoizR.Client.CapsuleCorp.Data;

public partial class Branch
{
    public Branch()
    {
    }

    public Branch(string name, string establishmentPrefix, string taxAuthId, string address, string phone, string email)
    {
        Name = name;
        EstablishmentPrefix = establishmentPrefix;
        TaxAuthId = taxAuthId;
        Address = address;
        Phone = phone;
        Email = email;
    }

    public string Name { get; set; }
    public string EstablishmentPrefix { get; set; }
    public string TaxAuthId { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}

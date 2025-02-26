namespace InvoizR.Domain.Entities;

public partial class Branch
{
    public Branch(string name, short? companyId, string establishmentPrefix, string taxAuthId, string address, string phone, string email, byte[] logo)
    {
        Name = name;
        CompanyId = companyId;
        EstablishmentPrefix = establishmentPrefix;
        TaxAuthId = taxAuthId;
        Address = address;
        Phone = phone;
        Email = email;
        Logo = logo;
    }
}

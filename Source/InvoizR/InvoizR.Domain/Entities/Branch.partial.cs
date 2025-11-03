namespace InvoizR.Domain.Entities;

public partial class Branch
{
    public Branch(string name, short? companyId, string taxAuthId, string address, string phone, string email, byte[] logo)
    {
        Name = name;
        CompanyId = companyId;
        TaxAuthId = taxAuthId;
        Address = address;
        Phone = phone;
        Email = email;
        Logo = logo;
    }
}

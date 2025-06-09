namespace InvoizR.Client.CapsuleCorp.Data;

public partial class Responsible
{
    public Responsible()
    {
    }

    public Responsible(string name, string phone, string email, string idType, string idNumber)
    {
        Name = name;
        Phone = phone;
        Email = email;
        IdType = idType;
        IdNumber = idNumber;
        AuthorizeCancellation = true;
        AuthorizeContingency = true;
    }

    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string IdType { get; set; }
    public string IdNumber { get; set; }
    public bool? AuthorizeCancellation { get; set; }
    public bool? AuthorizeContingency { get; set; }
}

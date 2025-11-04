namespace InvoizR.Domain.Entities;

public partial class Company
{
    public Company(string name, string code, string businessName, string taxIdNumber, string taxpayerRegistrationNumber)
    {
        Name = name;
        Code = code;
        BusinessName = businessName;
        TaxIdNumber = taxIdNumber;
        TaxpayerRegistrationNumber = taxpayerRegistrationNumber;
        EconomicActivityId = "1111";
        EconomicActivity = "Misc 1";
    }

    public void SetInformation(short? countryLevelId, string address, string email, string phone, byte[] logo)
    {
        CountryLevelId = countryLevelId;
        Address = address;
        Email = email;
        Phone = phone;
        Logo = logo;
    }
}

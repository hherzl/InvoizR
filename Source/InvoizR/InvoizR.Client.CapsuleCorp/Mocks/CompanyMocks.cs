using InvoizR.Clients.DataContracts;
using InvoizR.SharedKernel.Mh;

namespace InvoizR.Client.CapsuleCorp.Mocks;

public static class CompanyMocks
{
    public static CreateCompanyCommand GetCapsuleCorp()
        => new()
        {
            Environment = MhCatalog.Cat001.Prueba,
            Name = "Capsule Corp.",
            Code = "CC",
            BusinessName = "Capsule Corp.",
            TaxIdNumber = "0614-010121-123-4",
            TaxRegistrationNumber = "225-5",
            EconomicActivityId = "26101",
            EconomicActivity = "Fabricación de componentes y tableros electrónicos",
            CountryLevelId = 1,
            Address = "1 Capsule Corp Drive, West City",
            Phone = "+81-555-0000",
            Email = "contact@capsulecorp.com",
            Logo = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Assets", "logo.png"))),
            NonCustomerEmail = "dte@capsule-corp.com"
        };
}

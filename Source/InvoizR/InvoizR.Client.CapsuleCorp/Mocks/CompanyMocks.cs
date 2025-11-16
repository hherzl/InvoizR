using InvoizR.Clients.DataContracts;

namespace InvoizR.Client.CapsuleCorp.Mocks;

public static class CompanyMocks
{
    public static CreateCompanyCommand GetCapsuleCorp()
        => new()
        {
            Environment = "LC",
            Name = "Capsule Corp.",
            Code = "CC",
            BusinessName = "Capsule Corp.",
            TaxIdNumber = "0614-010121-123-4",
            TaxpayerRegistrationNumber = "225-5",
            EconomicActivityId = "26101",
            EconomicActivity = "Fabricación de componentes y tableros electrónicos",
            CountryLevelId = 1,
            Address = "1 Capsule Corp Drive, West City",
            Phone = "+81-555-0000",
            Email = "contact@capsulecorp.com",
            Logo = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Assets", "logo.png"))),
            NonCustomerEmail = "dte@capsule-corp.com",
            WebhookNotificationProtocol = "HTTP",
            WebhookNotificationAddress = "http://localhost:5000/dte",
            WebhookNotificationMisc1 = "PUT"
        };
}

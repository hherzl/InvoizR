namespace InvoizR.Domain.Entities;

public static class ThirdPartyServiceExtensions
{
    public static ThirdPartyService GetByName(this IEnumerable<ThirdPartyService> thirdPartyServices, string name)
        => thirdPartyServices.FirstOrDefault(item => item.Name == name);
}

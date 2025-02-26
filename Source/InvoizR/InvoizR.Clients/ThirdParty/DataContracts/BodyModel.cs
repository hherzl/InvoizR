namespace InvoizR.Clients.ThirdParty.DataContracts;

public record BodyModel
{
    public string User { get; set; }
    public string Token { get; set; }
    public RolModel Rol { get; set; }
    public string[] Roles { get; set; }
    public string TokenType { get; set; }
}

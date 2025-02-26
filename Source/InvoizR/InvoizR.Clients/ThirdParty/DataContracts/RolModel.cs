namespace InvoizR.Clients.ThirdParty.DataContracts;

public record RolModel
{
    public string Nombre { get; set; }
    public string Codigo { get; set; }
    public string Descripcion { get; set; }
    public object RolSuperior { get; set; }
    public string Nivel { get; set; }
    public bool? Activo { get; set; }
    public string[] Permisos { get; set; }
}

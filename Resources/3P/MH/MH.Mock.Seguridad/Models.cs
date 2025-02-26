using MH.SharedKernel;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace MH.Mock.Seguridad;

public class AuthRequest
{
    public AuthRequest()
    {
    }

    public AuthRequest(string user, string pwd)
    {
        User = user;
        Pwd = pwd;
    }

    [JsonPropertyName("user")]
    [Required]
    [StringLength(14)]
    public string User { get; set; }

    [JsonPropertyName("pwd")]
    [Required]
    [StringLength(maximumLength: 25, MinimumLength = 6)]
    public string Pwd { get; set; }
}

public record BodyModel
{
    public string User { get; set; }
    public string Token { get; set; }
    public RolModel Rol { get; set; }
    public string[] Roles { get; set; }
    public string TokenType { get; set; }
}

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

public record AuthResponse : BaseModel
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("body")]
    public BodyModel Body { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);
}

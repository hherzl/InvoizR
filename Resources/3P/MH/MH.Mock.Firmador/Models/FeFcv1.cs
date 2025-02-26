﻿using MH.SharedKernel;

namespace MH.Mock.Firmador.Models;

#pragma warning disable // Disable all warnings

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public partial class FeFcv1 : Dte
{
    /// <summary>
    /// Identificación
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("identificacion")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    public Identificacion Identificacion { get; set; } = new Identificacion();

    /// <summary>
    /// Documentos Relacionados
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("documentoRelacionado")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.MinLength(1)]
    [System.ComponentModel.DataAnnotations.MaxLength(10)]
    public System.Collections.Generic.ICollection<DocumentoRelacionado> DocumentoRelacionado { get; set; }

    /// <summary>
    /// Emisor
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("emisor")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    public Emisor Emisor { get; set; } = new Emisor();

    /// <summary>
    /// Receptor
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("receptor")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    public Receptor Receptor { get; set; }

    /// <summary>
    /// Documentos Asociados
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("otrosDocumentos")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.MinLength(1)]
    [System.ComponentModel.DataAnnotations.MaxLength(10)]
    public System.Collections.Generic.ICollection<OtrosDocumentos> OtrosDocumentos { get; set; }

    /// <summary>
    /// Ventas por cuenta de terceros
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("ventaTercero")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    public VentaTercero VentaTercero { get; set; }

    /// <summary>
    /// Cuerpo del Documento
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("cuerpoDocumento")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.MinLength(1)]
    [System.ComponentModel.DataAnnotations.MaxLength(2000)]
    public System.Collections.Generic.ICollection<CuerpoDocumento> CuerpoDocumento { get; set; } = new System.Collections.ObjectModel.Collection<CuerpoDocumento>();

    /// <summary>
    /// Resumen
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("resumen")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    public Resumen Resumen { get; set; } = new Resumen();

    /// <summary>
    /// Extensión
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("extension")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    public Extension Extension { get; set; }

    /// <summary>
    /// Apéndice
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("apendice")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.MinLength(1)]
    [System.ComponentModel.DataAnnotations.MaxLength(10)]
    public System.Collections.Generic.ICollection<Apendice> Apendice { get; set; }


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public partial class Identificacion : object
{
    /// <summary>
    /// Versión
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("version")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    public int Version { get; set; }

    /// <summary>
    /// Ambiente de destino
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("ambiente")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    //[System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    //public IdentificacionAmbiente Ambiente { get; set; }
    public string Ambiente { get; set; }

    /// <summary>
    /// Tipo de Documento
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("tipoDte")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    public string TipoDte { get; set; }

    /// <summary>
    /// Número de Control
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("numeroControl")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.StringLength(31, MinimumLength = 31)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^DTE-01-[A-Z0-9]{8}-[0-9]{15}$")]
    public string NumeroControl { get; set; }

    /// <summary>
    /// Código de Generación
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("codigoGeneracion")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.StringLength(36, MinimumLength = 36)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^[A-F0-9]{8}-[A-F0-9]{4}-[A-F0-9]{4}-[A-F0-9]{4}-[A-F0-9]{12}$")]
    public string CodigoGeneracion { get; set; }

    /// <summary>
    /// Modelo de Facturación
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("tipoModelo")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    public double TipoModelo { get; set; }

    /// <summary>
    /// Tipo de Transmisión
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("tipoOperacion")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    public IdentificacionTipoOperacion TipoOperacion { get; set; }

    /// <summary>
    /// Tipo de Contingencia
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("tipoContingencia")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    public IdentificacionTipoContingencia? TipoContingencia { get; set; }

    /// <summary>
    /// Motivo de Contingencia
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("motivoContin")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(150, MinimumLength = 5)]
    public string MotivoContin { get; set; }

    /// <summary>
    /// Fecha de Generación
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("fecEmi")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.Text.Json.Serialization.JsonConverter(typeof(DateFormatConverter))]
    public System.DateTimeOffset FecEmi { get; set; }

    /// <summary>
    /// Hora de Generación
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("horEmi")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]?$")]
    public string HorEmi { get; set; }

    /// <summary>
    /// Tipo de Moneda
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("tipoMoneda")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    public IdentificacionTipoMoneda TipoMoneda { get; set; }


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public partial class DocumentoRelacionado : object
{
    /// <summary>
    /// Tipo de Documento Tributario Relacionado
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("tipoDocumento")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    public DocumentoRelacionadoTipoDocumento TipoDocumento { get; set; }

    /// <summary>
    /// Tipo de Generación del Documento Tributario relacionado
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("tipoGeneracion")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    public DocumentoRelacionadoTipoGeneracion TipoGeneracion { get; set; }

    /// <summary>
    /// Número de documento relacionado
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("numeroDocumento")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.StringLength(36, MinimumLength = 1)]
    public string NumeroDocumento { get; set; }

    /// <summary>
    /// Fecha de Generación del Documento Relacionado
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("fechaEmision")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.Text.Json.Serialization.JsonConverter(typeof(DateFormatConverter))]
    public System.DateTimeOffset FechaEmision { get; set; }


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public partial class Emisor
{
    /// <summary>
    /// NIT (Emisor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("nit")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.StringLength(14)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^([0-9]{14}|[0-9]{9})$")]
    public string Nit { get; set; }

    /// <summary>
    /// NRC (Emisor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("nrc")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.StringLength(8, MinimumLength = 2)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^[0-9]{1,8}$")]
    public string Nrc { get; set; }

    /// <summary>
    /// Nombre, denominación o razón social del contribuyente (Emisor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("nombre")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.StringLength(250, MinimumLength = 1)]
    public string Nombre { get; set; }

    /// <summary>
    /// Código de Actividad Económica (Emisor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("codActividad")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.StringLength(6, MinimumLength = 5)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^[0-9]{2,6}$")]
    public string CodActividad { get; set; }

    /// <summary>
    /// Actividad Económica (Emisor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("descActividad")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.StringLength(150, MinimumLength = 5)]
    public string DescActividad { get; set; }

    /// <summary>
    /// Nombre Comercial (Emisor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("nombreComercial")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(150, MinimumLength = 5)]
    public string NombreComercial { get; set; }

    /// <summary>
    /// Tipo de establecimiento (Emisor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("tipoEstablecimiento")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    //[System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    public string TipoEstablecimiento { get; set; }

    /// <summary>
    /// Dirección (Emisor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("direccion")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    public Direccion Direccion { get; set; } = new Direccion();

    /// <summary>
    /// Teléfono (Emisor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("telefono")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.StringLength(30, MinimumLength = 8)]
    public string Telefono { get; set; }

    /// <summary>
    /// Correo electrónico (Emisor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("correo")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.StringLength(100, MinimumLength = 3)]
    public string Correo { get; set; }

    /// <summary>
    /// Código del establecimiento asignado por el MH
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("codEstableMH")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(4, MinimumLength = 4)]
    public string CodEstableMH { get; set; }

    /// <summary>
    /// Código del establecimiento asignado por el contribuyente
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("codEstable")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(4, MinimumLength = 4)]
    public string CodEstable { get; set; }

    /// <summary>
    /// Código del Punto de Venta (Emisor) asignado por el MH
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("codPuntoVentaMH")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(4, MinimumLength = 4)]
    public string CodPuntoVentaMH { get; set; }

    /// <summary>
    /// Código del Punto de Venta (Emisor) asignado por el contribuyente
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("codPuntoVenta")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(15, MinimumLength = 1)]
    public string CodPuntoVenta { get; set; }


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public partial class Receptor : object
{
    /// <summary>
    /// Tipo de documento de identificación (Receptor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("tipoDocumento")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    //[System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    public string TipoDocumento { get; set; }

    /// <summary>
    /// Número de documento de Identificación (Receptor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("numDocumento")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(20, MinimumLength = 3)]
    public string NumDocumento { get; set; }

    /// <summary>
    /// NRC (Receptor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("nrc")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(8, MinimumLength = 2)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^[0-9]{1,8}$")]
    public string Nrc { get; set; }

    /// <summary>
    /// Nombre, denominación o razón social del contribuyente (Receptor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("nombre")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(250, MinimumLength = 1)]
    public string Nombre { get; set; }

    /// <summary>
    /// Código de Actividad Económica (Receptor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("codActividad")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(6, MinimumLength = 5)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^[0-9]{2,6}$")]
    public string CodActividad { get; set; }

    /// <summary>
    /// Actividad Económica (Receptor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("descActividad")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(150, MinimumLength = 5)]
    public string DescActividad { get; set; }

    /// <summary>
    /// Dirección (Receptor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("direccion")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    public Direccion2 Direccion { get; set; }

    /// <summary>
    /// Teléfono (Receptor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("telefono")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(30, MinimumLength = 8)]
    public string Telefono { get; set; }

    /// <summary>
    /// Correo electrónico (Receptor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("correo")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(100)]
    public string Correo { get; set; }


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public partial class OtrosDocumentos : object
{
    /// <summary>
    /// Documento asociado
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("codDocAsociado")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(1, 4)]
    public int CodDocAsociado { get; set; }

    /// <summary>
    /// Identificación del documento asociado
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("descDocumento")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(100)]
    public string DescDocumento { get; set; }

    /// <summary>
    /// Descripción de documento asociado
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("detalleDocumento")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(300)]
    public string DetalleDocumento { get; set; }

    /// <summary>
    /// Médico
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("medico")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    public Medico Medico { get; set; }


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public partial class VentaTercero
{
    /// <summary>
    /// NIT por cuenta de Terceros
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("nit")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^([0-9]{14}|[0-9]{9})$")]
    public string Nit { get; set; }

    /// <summary>
    /// Nombre, denominación o razón social del Tercero
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("nombre")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.StringLength(250, MinimumLength = 1)]
    public string Nombre { get; set; }


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public partial class CuerpoDocumento : object
{
    /// <summary>
    /// N° de ítem
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("numItem")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(1, 2000)]
    public int NumItem { get; set; }

    /// <summary>
    /// Tipo de ítem
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("tipoItem")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    public CuerpoDocumentoTipoItem TipoItem { get; set; }

    /// <summary>
    /// Número de documento relacionado
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("numeroDocumento")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(36, MinimumLength = 1)]
    public string NumeroDocumento { get; set; }

    /// <summary>
    /// Cantidad
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("cantidad")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    public double Cantidad { get; set; }

    /// <summary>
    /// Código
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("codigo")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(25, MinimumLength = 1)]
    public string Codigo { get; set; }

    /// <summary>
    /// Tributo sujeto a cálculo de IVA
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("codTributo")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(2, MinimumLength = 2)]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    public CuerpoDocumentoCodTributo? CodTributo { get; set; }

    /// <summary>
    /// Unidad de Medida
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("uniMedida")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(1, 99)]
    public int UniMedida { get; set; }

    /// <summary>
    /// Descripción
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("descripcion")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.StringLength(1000)]
    public string Descripcion { get; set; }

    /// <summary>
    /// Precio Unitario
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("precioUni")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    public double PrecioUni { get; set; }

    /// <summary>
    /// Descuento, Bonificación, Rebajas por ítem
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("montoDescu")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double MontoDescu { get; set; }

    /// <summary>
    /// Ventas no Sujetas
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("ventaNoSuj")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double VentaNoSuj { get; set; }

    /// <summary>
    /// Ventas Exentas
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("ventaExenta")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double VentaExenta { get; set; }

    /// <summary>
    /// Ventas Gravadas
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("ventaGravada")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double VentaGravada { get; set; }

    /// <summary>
    /// Código del Tributo
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("tributos")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.MinLength(1)]
    public System.Collections.Generic.ICollection<string> Tributos { get; set; }

    /// <summary>
    /// Precio sugerido de venta
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("psv")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double Psv { get; set; }

    /// <summary>
    /// Cargos/Abonos que no afectan la base imponible
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("noGravado")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    public double NoGravado { get; set; }

    /// <summary>
    /// IVA por ítem
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("ivaItem")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double IvaItem { get; set; }


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public partial class Resumen : object
{
    /// <summary>
    /// Total de Operaciones no sujetas
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("totalNoSuj")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double TotalNoSuj { get; set; }

    /// <summary>
    /// Total de Operaciones exentas
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("totalExenta")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double TotalExenta { get; set; }

    /// <summary>
    /// Total de Operaciones Gravadas
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("totalGravada")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double TotalGravada { get; set; }

    /// <summary>
    /// Suma de operaciones sin impuestos
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("subTotalVentas")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double SubTotalVentas { get; set; }

    /// <summary>
    /// Monto global de Descuento, Bonificación, Rebajas y otros a ventas no sujetas
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("descuNoSuj")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double DescuNoSuj { get; set; }

    /// <summary>
    /// Monto global de Descuento, Bonificación, Rebajas y otros a ventas exentas
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("descuExenta")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double DescuExenta { get; set; }

    /// <summary>
    /// Monto global de Descuento, Bonificación, Rebajas y otros a ventas gravadas
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("descuGravada")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double DescuGravada { get; set; }

    /// <summary>
    /// Porcentaje del monto global de Descuento, Bonificación, Rebajas y otros
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("porcentajeDescuento")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, 100D)]
    public double PorcentajeDescuento { get; set; }

    /// <summary>
    /// Total del monto de Descuento, Bonificación, Rebajas
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("totalDescu")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double TotalDescu { get; set; }

    /// <summary>
    /// Resumen de Tributos
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("tributos")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    public System.Collections.Generic.ICollection<Tributos> Tributos { get; set; }

    /// <summary>
    /// Sub-Total
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("subTotal")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double SubTotal { get; set; }

    /// <summary>
    /// IVA Retenido
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("ivaRete1")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double IvaRete1 { get; set; }

    /// <summary>
    /// Retención Renta
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("reteRenta")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double ReteRenta { get; set; }

    /// <summary>
    /// Monto Total de la Operación
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("montoTotalOperacion")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double MontoTotalOperacion { get; set; }

    /// <summary>
    /// Total Cargos/Abonos que no afectan la base imponible
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("totalNoGravado")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    public double TotalNoGravado { get; set; }

    /// <summary>
    /// Total a Pagar
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("totalPagar")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double TotalPagar { get; set; }

    /// <summary>
    /// Valor en Letras
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("totalLetras")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.StringLength(200)]
    public string TotalLetras { get; set; }

    /// <summary>
    /// IVA 13%
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("totalIva")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double TotalIva { get; set; }

    /// <summary>
    /// Saldo a Favor
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("saldoFavor")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(double.MinValue, 0D)]
    public double SaldoFavor { get; set; }

    /// <summary>
    /// Condición de la Operación
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("condicionOperacion")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    public double CondicionOperacion { get; set; }

    /// <summary>
    /// Pagos
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("pagos")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.MinLength(1)]
    public System.Collections.Generic.ICollection<Pagos> Pagos { get; set; }

    /// <summary>
    /// Número de pago Electrónico
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("numPagoElectronico")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(100)]
    public string NumPagoElectronico { get; set; }


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public partial class Extension
{
    /// <summary>
    /// Nombre del responsable que Genera el DTE
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("nombEntrega")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(100, MinimumLength = 5)]
    public string NombEntrega { get; set; }

    /// <summary>
    /// Documento de identificación de quien genera el DTE
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("docuEntrega")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(25, MinimumLength = 5)]
    public string DocuEntrega { get; set; }

    /// <summary>
    /// Nombre del responsable de la operación por parte del receptor
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("nombRecibe")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(100, MinimumLength = 5)]
    public string NombRecibe { get; set; }

    /// <summary>
    /// Documento de identificación del responsable de la operación por parte del receptor
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("docuRecibe")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(25, MinimumLength = 5)]
    public string DocuRecibe { get; set; }

    /// <summary>
    /// Observaciones
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("observaciones")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(3000)]
    public string Observaciones { get; set; }

    /// <summary>
    /// Placa de vehículo
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("placaVehiculo")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(10)]
    public string PlacaVehiculo { get; set; }


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public partial class Apendice
{
    /// <summary>
    /// Nombre del campo
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("campo")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.StringLength(25)]
    public string Campo { get; set; }

    /// <summary>
    /// Descripción
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("etiqueta")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.StringLength(50)]
    public string Etiqueta { get; set; }

    /// <summary>
    /// Valor/Dato
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("valor")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.StringLength(150)]
    public string Valor { get; set; }


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public enum IdentificacionAmbiente
{

    [System.Runtime.Serialization.EnumMember(Value = @"00")]
    _00 = 0,


    [System.Runtime.Serialization.EnumMember(Value = @"01")]
    _01 = 1,


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public enum IdentificacionTipoOperacion
{

    _1 = 1,


    _2 = 2,


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public enum IdentificacionTipoContingencia
{

    [System.Runtime.Serialization.EnumMember(Value = @"1")]
    _1 = 1,


    [System.Runtime.Serialization.EnumMember(Value = @"2")]
    _2 = 2,


    [System.Runtime.Serialization.EnumMember(Value = @"3")]
    _3 = 3,


    [System.Runtime.Serialization.EnumMember(Value = @"4")]
    _4 = 4,


    [System.Runtime.Serialization.EnumMember(Value = @"5")]
    _5 = 5,


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public enum IdentificacionTipoMoneda
{

    [System.Runtime.Serialization.EnumMember(Value = @"USD")]
    USD = 0,


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public enum DocumentoRelacionadoTipoDocumento
{

    [System.Runtime.Serialization.EnumMember(Value = @"04")]
    _04 = 0,


    [System.Runtime.Serialization.EnumMember(Value = @"09")]
    _09 = 1,


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public enum DocumentoRelacionadoTipoGeneracion
{

    _1 = 1,


    _2 = 2,


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public enum EmisorTipoEstablecimiento
{

    [System.Runtime.Serialization.EnumMember(Value = @"01")]
    _01 = 0,


    [System.Runtime.Serialization.EnumMember(Value = @"02")]
    _02 = 1,


    [System.Runtime.Serialization.EnumMember(Value = @"04")]
    _04 = 2,


    [System.Runtime.Serialization.EnumMember(Value = @"07")]
    _07 = 3,


    [System.Runtime.Serialization.EnumMember(Value = @"20")]
    _20 = 4,


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public partial class Direccion : object
{
    /// <summary>
    /// Dirección Departamento (Emisor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("departamento")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^0[1-9]|1[0-4]$")]
    public string Departamento { get; set; }

    /// <summary>
    /// Dirección Municipio (Emisor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("municipio")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^[0-9]{2}$")]
    public string Municipio { get; set; }

    /// <summary>
    /// Dirección complemento (Emisor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("complemento")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.StringLength(200, MinimumLength = 1)]
    public string Complemento { get; set; }


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public enum ReceptorTipoDocumento
{

    [System.Runtime.Serialization.EnumMember(Value = @"36")]
    _36 = 1,


    [System.Runtime.Serialization.EnumMember(Value = @"13")]
    _13 = 2,


    [System.Runtime.Serialization.EnumMember(Value = @"02")]
    _02 = 3,


    [System.Runtime.Serialization.EnumMember(Value = @"03")]
    _03 = 4,


    [System.Runtime.Serialization.EnumMember(Value = @"37")]
    _37 = 5,


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public partial class Direccion2 : object
{
    /// <summary>
    /// Dirección: Departamento (Receptor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("departamento")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^0[1-9]|1[0-4]$")]
    public string Departamento { get; set; }

    /// <summary>
    /// Dirección: Municipio (Receptor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("municipio")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^[0-9]{2}$")]
    public string Municipio { get; set; }

    /// <summary>
    /// Dirección: complemento (Receptor)
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("complemento")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.StringLength(200, MinimumLength = 5)]
    public string Complemento { get; set; }


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public partial class Medico : object
{
    /// <summary>
    /// Nombre de médico que presta el Servicio
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("nombre")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.StringLength(100)]
    public string Nombre { get; set; }

    /// <summary>
    /// NIT de médico que presta el Servicio
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("nit")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^([0-9]{14}|[0-9]{9})$")]
    public string Nit { get; set; }

    /// <summary>
    /// Documento de identificación de médico no domiciliados
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("docIdentificacion")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(25, MinimumLength = 2)]
    public string DocIdentificacion { get; set; }

    /// <summary>
    /// Código del Servicio realizado
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("tipoServicio")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(1D, 6D)]
    public double TipoServicio { get; set; }


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public enum CuerpoDocumentoTipoItem
{

    _1 = 1,


    _2 = 2,


    _3 = 3,


    _4 = 4,


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public enum CuerpoDocumentoCodTributo
{

    [System.Runtime.Serialization.EnumMember(Value = @"A8")]
    A8 = 1,


    [System.Runtime.Serialization.EnumMember(Value = @"57")]
    _57 = 2,


    [System.Runtime.Serialization.EnumMember(Value = @"90")]
    _90 = 3,


    [System.Runtime.Serialization.EnumMember(Value = @"D4")]
    D4 = 4,


    [System.Runtime.Serialization.EnumMember(Value = @"D5")]
    D5 = 5,


    [System.Runtime.Serialization.EnumMember(Value = @"25")]
    _25 = 6,


    [System.Runtime.Serialization.EnumMember(Value = @"A6")]
    A6 = 7,


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public partial class Tributos
{
    /// <summary>
    /// Resumen Código de Tributo
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("codigo")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.StringLength(2, MinimumLength = 2)]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    public TributosCodigo Codigo { get; set; }

    /// <summary>
    /// Nombre del Tributo
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("descripcion")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.StringLength(150, MinimumLength = 2)]
    public string Descripcion { get; set; }

    /// <summary>
    /// Valor del Tributo
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("valor")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double Valor { get; set; }


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public partial class Pagos
{
    /// <summary>
    /// Código de forma de pago
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("codigo")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.StringLength(2)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^(0[1-9]||1[0-4]||99)$")]
    public string Codigo { get; set; }

    /// <summary>
    /// Monto por forma de pago
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("montoPago")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Range(0D, double.MaxValue)]
    public double MontoPago { get; set; }

    /// <summary>
    /// Referencia de modalidad de pago
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("referencia")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.StringLength(50)]
    public string Referencia { get; set; }

    /// <summary>
    /// Plazo
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("plazo")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^0[1-3]$")]
    public string Plazo { get; set; }

    /// <summary>
    /// Período de plazo
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("periodo")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    public double? Periodo { get; set; }


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
public enum TributosCodigo
{

    [System.Runtime.Serialization.EnumMember(Value = @"C3")]
    C3 = 0,


    [System.Runtime.Serialization.EnumMember(Value = @"59")]
    _59 = 1,


    [System.Runtime.Serialization.EnumMember(Value = @"71")]
    _71 = 2,


    [System.Runtime.Serialization.EnumMember(Value = @"D1")]
    D1 = 3,


    [System.Runtime.Serialization.EnumMember(Value = @"C8")]
    C8 = 4,


    [System.Runtime.Serialization.EnumMember(Value = @"C5")]
    C5 = 5,


    [System.Runtime.Serialization.EnumMember(Value = @"C6")]
    C6 = 6,


    [System.Runtime.Serialization.EnumMember(Value = @"C7")]
    C7 = 7,


    [System.Runtime.Serialization.EnumMember(Value = @"D5")]
    D5 = 8,


    [System.Runtime.Serialization.EnumMember(Value = @"19")]
    _19 = 9,


    [System.Runtime.Serialization.EnumMember(Value = @"28")]
    _28 = 10,


    [System.Runtime.Serialization.EnumMember(Value = @"31")]
    _31 = 11,


    [System.Runtime.Serialization.EnumMember(Value = @"32")]
    _32 = 12,


    [System.Runtime.Serialization.EnumMember(Value = @"33")]
    _33 = 13,


    [System.Runtime.Serialization.EnumMember(Value = @"34")]
    _34 = 14,


    [System.Runtime.Serialization.EnumMember(Value = @"35")]
    _35 = 15,


    [System.Runtime.Serialization.EnumMember(Value = @"36")]
    _36 = 16,


    [System.Runtime.Serialization.EnumMember(Value = @"37")]
    _37 = 17,


    [System.Runtime.Serialization.EnumMember(Value = @"38")]
    _38 = 18,


    [System.Runtime.Serialization.EnumMember(Value = @"39")]
    _39 = 19,


    [System.Runtime.Serialization.EnumMember(Value = @"42")]
    _42 = 20,


    [System.Runtime.Serialization.EnumMember(Value = @"43")]
    _43 = 21,


    [System.Runtime.Serialization.EnumMember(Value = @"44")]
    _44 = 22,


    [System.Runtime.Serialization.EnumMember(Value = @"50")]
    _50 = 23,


    [System.Runtime.Serialization.EnumMember(Value = @"51")]
    _51 = 24,


    [System.Runtime.Serialization.EnumMember(Value = @"52")]
    _52 = 25,


    [System.Runtime.Serialization.EnumMember(Value = @"53")]
    _53 = 26,


    [System.Runtime.Serialization.EnumMember(Value = @"54")]
    _54 = 27,


    [System.Runtime.Serialization.EnumMember(Value = @"55")]
    _55 = 28,


    [System.Runtime.Serialization.EnumMember(Value = @"58")]
    _58 = 29,


    [System.Runtime.Serialization.EnumMember(Value = @"77")]
    _77 = 30,


    [System.Runtime.Serialization.EnumMember(Value = @"78")]
    _78 = 31,


    [System.Runtime.Serialization.EnumMember(Value = @"79")]
    _79 = 32,


    [System.Runtime.Serialization.EnumMember(Value = @"85")]
    _85 = 33,


    [System.Runtime.Serialization.EnumMember(Value = @"86")]
    _86 = 34,


    [System.Runtime.Serialization.EnumMember(Value = @"91")]
    _91 = 35,


    [System.Runtime.Serialization.EnumMember(Value = @"92")]
    _92 = 36,


    [System.Runtime.Serialization.EnumMember(Value = @"A1")]
    A1 = 37,


    [System.Runtime.Serialization.EnumMember(Value = @"A5")]
    A5 = 38,


    [System.Runtime.Serialization.EnumMember(Value = @"A7")]
    A7 = 39,


    [System.Runtime.Serialization.EnumMember(Value = @"A9")]
    A9 = 40,


}

[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.8.0.0 (Newtonsoft.Json v13.0.3.0)")]
internal class DateFormatConverter : System.Text.Json.Serialization.JsonConverter<System.DateTimeOffset>
{
    public override System.DateTimeOffset Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
    {
        var dateTime = reader.GetString();
        if (dateTime == null)
        {
            throw new System.Text.Json.JsonException("Unexpected JsonTokenType.Null");
        }

        return System.DateTimeOffset.Parse(dateTime);
    }

    public override void Write(System.Text.Json.Utf8JsonWriter writer, System.DateTimeOffset value, System.Text.Json.JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
    }
}

using System.Text;
using InvoizR.Application.Reports.Templates.Common;
using InvoizR.SharedKernel.Mh;

namespace InvoizR.Application.Reports.Templates;

public class FallbackTemplatev1 : DteTemplatev1<FallbackTemplateModel>
{
    public FallbackTemplatev1(FallbackTemplateModel model)
        : base(model)
    {
    }

    public override string ToString()
    {
        var output = new StringBuilder();

        output.AppendLine("<html>");

        output.AppendLine(" <head>");
        output.AppendLine(" </head>");

        output.AppendLine(" <body>");

        output.AppendLine($" <h1>{Model.Title}</h1>");

        output.AppendLine($" <h2>{Model.InvoiceType}</h2>");

        output.AppendLine(" <table class='header-table'>");
        output.AppendLine("  <tbody>");
        output.AppendLine("   <tr>");
        output.AppendLine($"    <td><img alt='Logo' src='data:image/png;base64, {Model.Emitter.Logo}' class='logo'></td>");

        output.AppendLine($"    <td>");

        output.AppendLine($"     <ul>");
        output.AppendLine($"      <li>Código de generación: {AsStrong(Model.InvoiceGuid)}</li>");
        output.AppendLine($"      <li>Sello de recepción: {AsStrong(Model.ReceiptStamp)}</li>");
        output.AppendLine($"      <li>Fecha y hora de generación: {AsDateTime(Model.EmitDateTime)}</li>");
        output.AppendLine($"      <li>Versión de JSON: {AsStrong(Model.SchemaVersion.ToString())}</li>");
        output.AppendLine($"     </ul>");

        output.AppendLine($"    </td>");

        output.AppendLine("   </tr>");
        output.AppendLine("  </tbody>");
        output.AppendLine(" </table>");

        output.AppendLine(" <br>");

        output.AppendLine(" <table class='emitter-table'>");
        output.AppendLine("  <caption>EMISOR</caption>");
        output.AppendLine("  <tbody>");

        output.Append($"   <tr><td>Nombre o razón social: {AsStrong(Model.Emitter.BusinessName)}, ");
        output.AppendLine($"   NIT: {AsStrong(Model.Emitter.TaxIdNumber)}, NRC: {AsStrong(Model.Emitter.TaxRegistrationNumber)}</td></tr>");

        output.AppendLine($"   <tr><td>Actividad económica: {AsStrong(Model.Emitter.EconomicActivity)}</td></tr>");
        output.AppendLine($"   <tr><td>Dirección: {AsStrong(Model.Emitter.Address)}, Teléfono: {AsStrong(Model.Emitter.Phone)}</td></tr>");
        output.AppendLine($"   <tr><td>Correo electrónico: {AsStrong(Model.Emitter.Email)}</td></tr>");

        output.AppendLine("  </tbody>");
        output.AppendLine(" </table>");

        output.AppendLine(" <br>");

        output.AppendLine(" <hr>");

        output.AppendLine(" <br>");

        output.AppendLine(" <table class='invoices-table'>");
        output.AppendLine("  <caption>Detalle de Documentos Tributarios Electrónicos</caption>");
        output.AppendLine("  <thead>");
        output.AppendLine("   <tr>");
        output.AppendLine("    <th>N°</th>");
        output.AppendLine("    <th>Código de Generación</th>");
        output.AppendLine("    <th>Tipo DTE</th>");
        output.AppendLine("   </tr>");
        output.AppendLine("  </thead>");

        output.AppendLine("  <tbody>");

        var lines = Model.Dte.DetalleDTE;

        var i = 0;

        foreach (var line in lines)
        {
            if (i % 2 == 0)
                output.AppendLine("  <tr class='invoices-row'>");
            else
                output.AppendLine("  <tr class='invoices-row invoices-alt-row'>");

            output.AppendLine($"   <td>{line.NoItem}</td>");
            output.AppendLine($"   <td>{line.CodigoGeneracion}</td>");
            output.AppendLine($"   <td>{MhCatalog.Cat002.Desc(line.TipoDoc)}</td>");

            output.AppendLine("  </tr>");

            i++;
        }

        output.AppendLine("  </tbody>");

        output.AppendLine(" </table>");

        output.AppendLine($" <h2>Motivo</h2>");

        output.AppendLine(" <table class='reason-table'>");

        output.AppendLine("  <tbody>");

        output.AppendLine("   <tr>");
        output.AppendLine($"    <td>Fecha de inicio</td><td>{AsStrong(AsDate(Model.Dte.Motivo.FInicio))}</td>");
        output.AppendLine("   </tr>");

        output.AppendLine("   <tr>");
        output.AppendLine($"    <td>Hora de inicio</td><td>{AsItalic(Model.Dte.Motivo.HInicio)}</td>");
        output.AppendLine("   </tr>");

        output.AppendLine("   <tr>");
        output.AppendLine($"    <td>Fecha de fin</td><td>{AsStrong(AsDate(Model.Dte.Motivo.FFin))}</td>");
        output.AppendLine("   </tr>");

        output.AppendLine("   <tr>");
        output.AppendLine($"    <td>Hora de fin</td><td>{AsItalic(Model.Dte.Motivo.HFin)}</td>");
        output.AppendLine("   </tr>");

        output.AppendLine("   <tr>");
        output.AppendLine($"    <td>Tipo de contingencia:</td><td>{AsStrong(MhCatalog.Cat005.Desc(Model.Dte.Motivo.TipoContingencia))}</td>");
        output.AppendLine("   </tr>");

        if (!string.IsNullOrEmpty(Model.Dte.Motivo.MotivoContingencia))
        {
            output.AppendLine("   <tr>");
            output.AppendLine($"    <td>Descripción:</td><td>{Model.Dte.Motivo.MotivoContingencia}</td>");
            output.AppendLine("   </tr>");
        }

        output.AppendLine("  </tbody>");

        output.AppendLine(" </body>");

        output.AppendLine("</html>");

        return output.ToString();
    }
}

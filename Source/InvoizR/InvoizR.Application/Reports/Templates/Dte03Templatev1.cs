using System.Text;
using InvoizR.Application.Helpers;
using InvoizR.Application.Reports.Templates.Common;
using InvoizR.SharedKernel.Mh;

namespace InvoizR.Application.Reports.Templates;

public class Dte03Templatev1 : DteTemplatev1<Dte03TemplateModel>
{
    public Dte03Templatev1(Dte03TemplateModel model)
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
        output.AppendLine($"    <td><img alt='Código QR Consulta Pública' src='data:image/png;base64, {Model.Emitter.Logo}' class='logo'></td>");

        output.AppendLine($"    <td>");

        output.AppendLine($"     <ul>");
        output.AppendLine($"      <li>Código de generación: {AsStrong(Model.GenerationCode)}</li>");
        output.AppendLine($"      <li>Número de control: {AsStrong(Model.ControlNumber)}</li>");
        output.AppendLine($"      <li>Sello de recepción: {AsStrong(Model.ReceiptStamp)}</li>");

        output.AppendLine($"      <li>Modelo de facturación: {MhCatalog.Cat003.Desc(Model.Dte.Identificacion.TipoModelo)}</li>");
        output.AppendLine($"      <li>Tipo de transmisión: {MhCatalog.Cat004.Desc((int)Model.Dte.Identificacion.TipoOperacion)}</li>");

        output.AppendLine($"      <li>Fecha y hora de generación: {Model.ProcessingDateTime:yyyy-MM-dd hh:mm:ss}</li>");
        output.AppendLine($"      <li>Versión de JSON: {AsStrong(Model.SchemaVersion.ToString())}</li>");
        output.AppendLine($"     </ul>");

        output.AppendLine($"    </td>");

        output.AppendLine($"    <td><img alt='Código QR' src='data:image/png;base64, {Model.Qr}' class='qr'></td>");
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

        output.AppendLine(" <table class='receiver-table'>");
        output.AppendLine("  <caption>RECEPTOR</caption>");
        output.AppendLine("  <tbody>");

        output.AppendLine($"   <tr><td>Nombre o razón social: {AsStrong(Model.Receiver.Name)}");

        if (Model.Receiver.DocumentTypeId == MhCatalog.Cat022.Dui)
            output.Append($", Documento Único de Identidad: {AsStrong(Model.Receiver.DocumentNumber)}");
        else
            output.Append($", Otro: {AsStrong(Model.Receiver.DocumentNumber)}");

        output.AppendLine($"   </td></tr>");

        output.AppendLine($"   <tr><td>Dirección: {AsStrong(Model.Receiver.Address)}, Teléfono: {AsStrong(Model.Receiver.Phone)}</td></tr>");
        output.AppendLine($"   <tr><td>Correo electrónico: {AsStrong(Model.Receiver.Email)}</td></tr>");

        output.AppendLine("  </tbody>");
        output.AppendLine(" </table>");

        output.AppendLine(" <br>");

        output.AppendLine(" <table class='lines-table'>");
        output.AppendLine("  <caption>Líneas</caption>");
        output.AppendLine("  <thead>");
        output.AppendLine("   <tr>");
        output.AppendLine("    <th>N°</th>");
        output.AppendLine("    <th>Cantidad</th>");
        output.AppendLine("    <th>Unidad</th>");
        output.AppendLine("    <th>Descripción</th>");
        output.AppendLine("    <th>Precio<br>Unitario</th>");
        output.AppendLine("    <th>Ventas<br>No Sujetas</th>");
        output.AppendLine("    <th>Ventas<br>Exentas</th>");
        output.AppendLine("    <th>Ventas<br>Gravadas</th>");
        output.AppendLine("   </tr>");
        output.AppendLine("  </thead>");

        output.AppendLine("  <tbody>");

        var lines = Model.Dte.CuerpoDocumento;

        var i = 0;

        foreach (var line in lines)
        {
            if (i % 2 == 0)
                output.AppendLine("  <tr class='lines-row'>");
            else
                output.AppendLine("  <tr class='lines-row lines-alt-row'>");

            output.AppendLine($"   <td class='line-value'>{line.NumItem}</td>");
            output.AppendLine($"   <td class='line-value'>{line.Cantidad}</td>");

            if (line.UniMedida == MhCatalog.Cat014.Otra)
                output.AppendLine($"   <td class='line-value'>Otra</td>");
            else
                output.AppendLine($"   <td class='line-value'></td>");

            output.AppendLine($"   <td>{line.Descripcion}</td>");
            output.AppendLine($"   <td class='lines-amount'>{AsAmount(line.PrecioUni)}</td>");
            output.AppendLine($"   <td class='lines-amount'>{AsAmount(line.VentaNoSuj)}</td>");
            output.AppendLine($"   <td class='lines-amount'>{AsAmount(line.VentaExenta)}</td>");
            output.AppendLine($"   <td class='lines-amount'>{AsAmount(line.VentaGravada)}</td>");
            output.AppendLine("  </tr>");

            i++;
        }

        output.AppendLine("  </tbody>");

        output.AppendLine("  <tfoot>");

        var totals = new
        {
            VentaNoSuj = lines.Sum(item => item.VentaNoSuj),
            VentaExenta = lines.Sum(item => item.VentaExenta),
            VentaGravada = lines.Sum(item => item.VentaGravada),
            Total = lines.Sum(item => item.VentaNoSuj + item.VentaExenta + item.VentaGravada)
        };

        output.AppendLine("   <tr class='lines-totals-row'>");
        output.AppendLine($"    <td colspan='5' class='summary-row'>{AsStrong("Suma de ventas:")}</td>");
        output.AppendLine($"    <td class='lines-amount'>{AsAmount(totals.VentaNoSuj)}</td>");
        output.AppendLine($"    <td class='lines-amount'>{AsAmount(totals.VentaExenta)}</td>");
        output.AppendLine($"    <td class='lines-amount'>{AsStrong(AsAmount(totals.VentaGravada))}</td>");
        output.AppendLine("   </tr>");

        output.AppendLine("   <tr>");
        output.AppendLine("    <td colspan='8'>&nbsp;</td>");
        output.AppendLine("   </tr>");

        output.AppendLine("   <tr class='lines-summary-row'>");
        output.AppendLine($"    <td colspan='7' class='summary-row'>Sumatoria de ventas:</td>");
        output.AppendLine($"    <td class='lines-amount'>{AsAmount(totals.Total)}</td>");
        output.AppendLine("   </tr>");

        output.AppendLine("   <tr class='lines-summary-row'>");
        output.AppendLine($"    <td colspan='7' class='summary-row'>Sub-total:</td>");
        output.AppendLine($"    <td class='lines-amount'>{AsAmount(totals.Total)}</td>");
        output.AppendLine("   </tr>");

        output.AppendLine("   <tr class='lines-summary-row'>");
        output.AppendLine($"    <td colspan='7' class='summary-row'>Monto Total de la operación:</td>");
        output.AppendLine($"    <td class='lines-amount'>{AsAmount(totals.Total)}</td>");
        output.AppendLine("   </tr>");

        output.AppendLine("   <tr>");
        output.AppendLine("    <td colspan='8'>&nbsp;</td>");
        output.AppendLine("   </tr>");

        output.AppendLine("   <tr>");
        output.AppendLine($"    <td colspan='8'>Total en letras: {AsStrong(MoneyToWordsConverter.SpellingNumber(totals.Total))}</td>");
        output.AppendLine("   </tr>");

        output.AppendLine("   <tr>");
        output.AppendLine($"    <td colspan='8'>Condición de la operación: {MhCatalog.Cat016.Desc(Model.Dte.Resumen.CondicionOperacion)}</td>");
        output.AppendLine("   </tr>");

        output.AppendLine("   <tr>");
        output.AppendLine("    <td colspan='8'>&nbsp;</td>");
        output.AppendLine("   </tr>");

        output.AppendLine("   <tr>");
        output.AppendLine($"    <td colspan='8'>{AsStrong("Pagos")}</td>");
        output.AppendLine("   </tr>");

        foreach (var item in Model.Dte.Resumen.Pagos)
        {
            output.AppendLine("   <tr>");
            output.AppendLine($"    <td colspan='7'>{MhCatalog.Cat017.Desc(item.Codigo)}</td>");
            output.AppendLine($"    <td class='lines-amount'>{AsAmount(item.MontoPago)}</td>");
            output.AppendLine("   </tr>");
        }

        output.AppendLine("   <tr>");
        output.AppendLine("    <td colspan='8'>&nbsp;</td>");
        output.AppendLine("   </tr>");

        output.AppendLine("   <tr class='lines-summary-row'>");
        output.AppendLine($"    <td colspan='7' class='summary-row'>Total a pagar:</td>");
        output.AppendLine($"    <td class='lines-amount'>{AsStrong(AsAmount(totals.Total))}</td>");
        output.AppendLine("   </tr>");

        output.AppendLine("  </tfoot>");

        output.AppendLine(" </table>");

        output.AppendLine(" </body>");

        output.AppendLine("</html>");

        return output.ToString();
    }
}

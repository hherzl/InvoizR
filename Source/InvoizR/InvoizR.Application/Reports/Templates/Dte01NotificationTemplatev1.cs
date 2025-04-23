using System.Net.Mail;
using System.Text;

namespace InvoizR.Application.Reports.Templates;

public class Dte01NotificationTemplatev1
{
    public Dte01NotificationTemplatev1(Dte01NotificationTemplateModel model)
    {
        Model = model;
    }

    public Dte01NotificationTemplateModel Model { get; }

    public string Build()
    {
        var output = new StringBuilder();

        string AsStrong(string value)
            => $"<strong>{value}</strong>";

        string AsDate(DateTime? date)
            => $"{date.Value.ToShortDateString()}";

        string AsCurrency(decimal? amount)
            => $"{amount:C2}";

        output.AppendLine($"<p>Estimado cliente {AsStrong(Model.CustomerName)}, adjunto a este correo electrónico encontrará los archivos relacionados a la factura electrónica ");
        output.AppendLine($"de su reciente compra en sucursal {AsStrong(Model.Branch)}, gracias por su preferencia.</p>");

        output.AppendLine($"<p>Detalle del Documento Electrónico:</p>");

        output.AppendLine($"<ul>");
        output.AppendLine($" <li>Tipo: {AsStrong(Model.InvoiceType)}</li>");
        output.AppendLine($" <li>Fecha: {AsStrong(AsDate(Model.InvoiceDate))}</li>");
        output.AppendLine($" <li>Total: {AsCurrency(Model.InvoiceTotal)}</li>");
        output.AppendLine($" <li>Número de control: {AsStrong(Model.ControlNumber)}</li>");
        output.AppendLine($"</ul>");

        output.AppendLine($"<br>");

        output.AppendLine($"<p><u>Nota: no responder este mensaje auto generado, por cualquier duda comunicarse con la sucursal: <i>{Model.BranchPhone}</i></u>.</p>");

        return output.ToString();
    }

    public MailMessage ToMailMessage()
    {
        var message = new MailMessage(Model.SourceEmail, Model.CustomerEmail)
        {
            Subject = Model.Title,
            Body = Build(),
            IsBodyHtml = true
        };

        return message;
    }

    public override string ToString()
        => Build();
}

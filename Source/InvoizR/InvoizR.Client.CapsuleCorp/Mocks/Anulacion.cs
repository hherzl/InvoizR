using InvoizR.Clients.DataContracts.Cancellation;
using InvoizR.SharedKernel.Mh;
using InvoizR.SharedKernel.Mh.Anulacion;

namespace InvoizR.Client.CapsuleCorp.Mocks;

static class Anulacion
{
    public static DteCancellationCommand MockAnulacion(long? invoiceId)
    {
        var dte = new Anulacionv2();

        dte.Identificacion.Version = 2;
        dte.Identificacion.Ambiente = MhCatalog.Cat001.Prueba;
        dte.Identificacion.CodigoGeneracion = Guid.NewGuid().ToString().ToUpper();
        dte.Identificacion.FecAnula = new DateTimeOffset(DateTime.Now);
        dte.Identificacion.HorAnula = DateTime.Now.ToString("HH:mm:ss");

        //dte.Emisor.Nit = request.TaxInformation.TaxIdNumber;
        //dte.Emisor.NomEstablecimiento = request.TaxInformation.BusinessName;
        //dte.Emisor.Nombre = request.TaxInformation.BusinessName;
        //dte.Emisor.TipoEstablecimiento = TipoEstablecimiento.SucursalAgencia;
        //dte.Emisor.Telefono = request.Document.Branch.Phone;
        //dte.Emisor.Correo = request.Document.Branch.Email;

        //dte.Documento.TipoDte = request.ReferenceDocument.SchemaType;
        //dte.Documento.NumeroControl = request.ReferenceDocument.AuditNumber;
        //dte.Documento.CodigoGeneracion = request.ReferenceDocument.InvoiceGuid;
        //dte.Documento.SelloRecibido = request.ReferenceDocument.ReceiptStamp;

        //dte.Documento.FecEmi = new DateTimeOffset(request.ReferenceDocument.DocDate.Value);

        //dte.Documento.MontoIva = 0;

        //if (!string.IsNullOrEmpty(request.Customer.DocumentNumber1))
        //{
        //    dte.Documento.TipoDocumento = TipoDocumentoIdentificacionReceptor.Dui;
        //    dte.Documento.NumDocumento = request.Customer.DocumentNumber1;
        //}
        //else if (!string.IsNullOrEmpty(request.Customer.DocumentNumber2))
        //{
        //    dte.Documento.TipoDocumento = TipoDocumentoIdentificacionReceptor.Nit;
        //    dte.Documento.NumDocumento = request.Customer.DocumentNumber2;
        //}

        //dte.Documento.Nombre = request.Customer.Name;
        //dte.Documento.Telefono = request.Customer.Phone1;
        //dte.Documento.Correo = request.Customer.Email;

        //dte.Motivo.TipoAnulacion = TipoInvalidacion.RecindirOperacion;
        ////eDocument.Motivo.MotivoAnulacion = "";

        //dte.Motivo.NombreResponsable = "Ricardo Antonio Guzman Mejía";
        //dte.Motivo.TipDocResponsable = "13";
        //dte.Motivo.NumDocResponsable = "02649748-7";

        //dte.Motivo.NombreSolicita = request.Document.Branch.ResponsibleName;
        //dte.Motivo.TipDocSolicita = request.Document.Branch.ResponsibleDocumentTypeId;
        //dte.Motivo.NumDocSolicita = request.Document.Branch.ResponsibleDocumentNumber;

        return new DteCancellationCommand
        {
            InvoiceId = invoiceId,
            Anulacion = dte.ToJson()
        };
    }
}

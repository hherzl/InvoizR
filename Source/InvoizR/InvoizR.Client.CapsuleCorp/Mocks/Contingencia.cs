using InvoizR.SharedKernel.Mh;
using InvoizR.SharedKernel.Mh.Contingencia;

namespace InvoizR.Client.CapsuleCorp.Mocks;

static class Contingencia
{
    public static Contingenciav3 MockContingencia()
    {
        var contingencia = new Contingenciav3();

        contingencia.Identificacion.Version = 3;
        contingencia.Identificacion.Ambiente = MhCatalog.Cat001.Prueba;
        contingencia.Identificacion.CodigoGeneracion = Guid.NewGuid().ToString().ToUpper();
        contingencia.Identificacion.FTransmision = new DateTimeOffset(DateTime.Now);
        contingencia.Identificacion.HTransmision = DateTime.Now.ToShortTimeString();

        var company = CompanyMocks.GetCapsuleCorp();
        contingencia.Emisor.Nit = company.TaxIdNumber;
        contingencia.Emisor.Nombre = company.Name;

        // TODO: complete this properties
        //contingencia.Emisor.NombreResponsable = "";
        //contingencia.Emisor.TipoDocResponsable = "";
        //contingencia.Emisor.NumeroDocResponsable = "";

        contingencia.Emisor.Correo = company.Email;

        contingencia.Motivo.FInicio = new DateTimeOffset(DateTime.Now);
        contingencia.Motivo.HInicio = DateTime.Now.ToShortTimeString();
        contingencia.Motivo.TipoContingencia = MhCatalog.Cat005.Otro;
        contingencia.Motivo.MotivoContingencia = $"Contingencia de prueba ({DateTime.Now})";

        return contingencia;
    }
}

# InvoizR

La solución **InvoizR** proporciona un "toolkit" para facilitar la integración con la facturación electrónica según el Ministerio de Hacienda.

Hace días escribí un resumen sobre mi experiencia, [Integrando Facturación Electrónica con SAP Business One (SAP B1)](https://devshivaschool4devs.blogspot.com/2024/12/integrando-facturacion-electronica-con.html), esta solución pretende proporcionar un recurso útil que reduzca el tiempo de desarrollo.

La temática es `Capsule Corp.`, la idea es proporcionar un modelo como ejemplo para el procesamiento de facturas electrónicas, cada implementación deberá sincronizar la información de la empresa emisora de DTE.

Esta solución ha sido diseñada para alto volumen de procesamiento, servicios en segundo plano preparan y sincronizan las facturas electrónicas con el Ministerio de Hacienda.

El código proporcionado en este repositorio es de uso libre, para más detalles lee la licencia presente en el mismo, este es un recurso de programadores para programadores.

## Tecnologías

### Prerrequisitos

1. [.NET SDK](https://dotnet.microsoft.com/en-us/download)
1. [Visual Studio](https://visualstudio.microsoft.com)
1. [Visual Studio Code](https://code.visualstudio.com)
1. [SQL Server 2022 Express](https://www.microsoft.com/en-us/download/details.aspx?id=104781&lc=1033)

### Stack

Las siguientes tecnologías son utilizadas para esta solución:

- Entity Framework Core
- ASP.NET Core
- Blazor
- SQL Server

### Librerías

Las siguientes librerías son usadas por la solución:

- [ClosedXML](https://github.com/ClosedXML/ClosedXML)
- [DinkToPdf](https://github.com/rdvojmoc/DinkToPdf)
- [MediatR](https://github.com/jbogard/MediatR)
- [MudBlazor](https://mudblazor.com)
- [QRCoder](https://github.com/codebude/QRCoder)
- [Serilog](https://serilog.net)
- [xUnit.net](https://xunit.net)

Sirven como complementos para generar el código QR, PDF o EXCEL; estas librerías podrían ser reemplazadas pero será trabajo adicional producir la misma salida.

### Estructura

La solución `InvoizR` contiene los siguientes proyectos:

|Proyecto|Tipo|Versión .NET|Descripción|
|--------|----|------------|-----------|
|InvoizR.SharedKernel|Class library|.NET 8.0|Contiene definiciones usadas por todos los proyectos|
|InvoizR.Clients|Class library|.NET 8.0|Contiene el cliente HTTP usado por el GUI y el Capsule Corp.|
|InvoizR.Domain|Class library|.NET 9.0|Contiene el dominio; entidades, enumeraciones y excepciones|
|InvoizR.Application|Class library|.NET 9.0|Contiene los casos de uso implementados con CQRS (MediatR)|
|InvoizR.Infrastructure|Class library|.NET 9.0|Implementa la conexión a recursos externos: SQL, SMTP y clientes de terceros|
|InvoizR.API.Billing|Web API|.NET 8.0|Expone la API para facturación|
|InvoizR.API.Reports|Web API|.NET 8.0|Expone la API para reportes|
|InvoizR.GUI.InvoiceManager|Blazor|.NET 8.0|Interfaz gráfica para administrar las facturas electrónicas (MudBlazor)|

## Arquitectura

Para esta solución se ha optado por [Clean Architecture](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture)
 y [Domain Driven Design](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice) como esquema para el desarrollo de esta solución.

 Debido a los cambios de la última versión de .NET, se ha optado por hacer uso de [Minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/overview?view=aspnetcore-9.0).

## Ejecución

Esta solución ha sido desarrollada en Windows, por temas de auditoría se ha creado el directorio `C:\InvoizR`, el cual debe contener los siguientes subdirectorios:

    Dte: Guarda los archivos JSON y PDF de las facturas electrónicas
    Logs: Crea un directorio por cada factura donde se guarda el JSON del JWT, la firma y el DTE a procesar
    Notifications: Guarda el HTML del correo electrónico de notificación de la factura electrónica

*Nota: estos directorios pueden cambiarse en el archivo appsettings.json de cada API.*

### Recursos

Dentro de la solución hay un directorio con nombre `Resources`, el contenido es el siguiente:

    3P: Servicios Mock que emulan el comportamiento de las APIs del MH
    Database: Contiene los scripts para la creación de la base de datos
    MH: Contiene los esquemas JSON y el API para firmar documentos

#### Mock para Servicios del Ministerio de Hacienda

El Ministerio de Hacienda tiene un proceso engorroso para aprobar a una empresa como emisor de DTE, previniendo eso se han desarrollado APIs que emulan el comportamiento de los servicios del MH:

    Seguridad
    Firmador
    FE SV

### Scripts BAT

Para facilitar la ejecución de la solución, hay scripts BAT para poder ejecutar los servicios y mocks sin necesidad de abrir el editor.

|Archivo|Descripción|
|-------|-----------|
|00-Create-Database.bat|Crea la base de datos en la instancia SQL Server|
|10-Seed-Billing.bat|Inicializa los registros para la base de datos|
|20-Run-Billing.bat|Ejecuta la API para facturación|
|21-Run-Reports.bat|Ejecuta la API para reportes|
|30-Run-Mh-Seguridad.bat|Ejecuta el API Mock de seguridad del MH|
|31-Run-Mh-Firmador.bat|Ejecuta el API Mock de firma para DTE|
|32-Run-Mh-FeSv.bat|Ejecuta el API Mock de Fe SV del MH|
|40-Seed-CapsuleCorp.bat|Inicializa los registros de `Capsule Corp.`|

|41-Mock-CC-DTE01-OW-1.bat|Crea 1 factura de consumidor final en procesamiento OW (útil para debug)|
|41-Mock-CC-DTE01-OW-25.bat|Crea 25 facturas de consumidor final en procesamiento OW|
|41-Mock-CC-DTE01-RT-1.bat|Crea 1 factura de consumidor final en procesamiento RT (útil para debug)|
|41-Mock-CC-DTE01-RT-25.bat|Crea 25 facturas de consumidor final en procesamiento RT|
|43-Mock-CC-DTE03-OW-1.bat|Crea 1 factura de comprobrante de crédito fiscal en procesamiento OW (útil para debug)|
|43-Mock-CC-DTE03-OW-25.bat|Crea 25 facturas de comprobrante de crédito fiscal en procesamiento OW|
|43-Mock-CC-DTE03-RT-1.bat|Crea 1 factura de comprobrante de crédito fiscal en procesamiento RT (útil para debug)|
|43-Mock-CC-DTE03-RT-25.bat|Crea 25 facturas de comprobrante de crédito fiscal en procesamiento RT|
|44-Mock-CC-DTE04-OW-1.bat|Crea 1 factura de nota de remisión en procesamiento OW (útil para debug)|
|44-Mock-CC-DTE04-OW-25.bat|Crea 25 facturas de nota de remisión en procesamiento OW|
|44-Mock-CC-DTE04-RT-1.bat|Crea 1 factura de nota de remisión en procesamiento RT (útil para debug)|
|44-Mock-CC-DTE04-RT-25.bat|Crea 25 facturas de nota de remisión en procesamiento RT|
|45-Mock-CC-DTE05-OW-1.bat|Crea 1 factura de nota de crédito en procesamiento OW (útil para debug)|
|45-Mock-CC-DTE05-OW-25.bat|Crea 25 facturas de nota de crédito en procesamiento OW|
|45-Mock-CC-DTE05-RT-1.bat|Crea 1 factura de nota de crédito en procesamiento RT (útil para debug)|
|45-Mock-CC-DTE05-RT-25.bat|Crea 25 facturas de nota de crédito en procesamiento RT|
|54-Mock-CC-DTE14-OW-1.bat|Crea 1 factura de sujeto excluido en procesamiento OW (útil para debug)|
|54-Mock-CC-DTE14-OW-25.bat|Crea 25 facturas de sujeto excluido en procesamiento OW|
|54-Mock-CC-DTE14-RT-1.bat|Crea 1 factura de sujeto excluido en procesamiento RT (útil para debug)|
|54-Mock-CC-DTE14-RT-25.bat|Crea 25 facturas de sujeto excluido en procesamiento RT|
|99-Run-GUI.bat|Ejecuta el GUI para administración|

### Integración

Usaremos `Capsule Corp.` como referencia para la empresa que se integrará con la facturación electrónica.

`Capsule Corp.` debe iniciar el trámite para solicitar autorización como emisor de DTE, una vez complete el trámite con el Ministerio de Hacienda podrá ingresar al 
[Portal para Emisores DTE](https://admin.factura.gob.sv/login), según la actividad económica del emisor así serán los DTE que podrá emitir.

En el ambiente de pruebas hay una cantidad por cada tipo de DTE que se deben transmitir satisfactoriamente adicional se debe transmitir en contingencia; una vez se haya completado el bloque de transmisión en ambiente de pruebas se podrá proceder con el paso a producción, lo cual no lleva más de 15 minutos, una vez se obtiene la autorización no se puede regresar a ambiente de pruebas ni a emitir facturas en físico.

### Limitaciones

La principal limitación del proyecto es que la base de datos es SQL Server, pero pensando bien hay una alternativa la cual sería usar IA para transformar los SCRIPTS y el mapeo a otra base de datos; al no hacer uso de objetos para base de datos como procedimientos almacenados y funciones escalares o de tabla el código dentro del espacio de nombres `Application.Features` no debe sufrir cambios.

## Hoja de Ruta

    2025/02/27: Versión inicial, Factura de consumidor Final
    2025/03/06: Comprobante de crédito fiscal
    2025/03/13: Factura de exportación
    2025/03/20: Factura sujeto excluido
    2025/03/27: Nota de crédito, debito y remisión

Actualmente solo la factura de consumidor final está implementada, si deseas implementar otro tipo de DTE sigue estos pasos:

    Generar Modelo en C# a partir del esquema JSON
    Agregar MapDteXY en InvoizR.API.Billing
    Agregar método CreateDteXYInvoiceAsync en IInvoizRClient
    Agregar CreateDteXYInvoiceCommand y CreateDteXYInvoiceCommandHandler en IInvoizRClient
    DteXYHostedService en InvoizR.API.Billing

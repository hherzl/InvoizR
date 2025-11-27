using System.Dynamic;
using CatFactory.EntityFrameworkCore;
using CatFactory.ObjectRelationalMapping;
using CatFactory.SqlServer;
using CatFactory.SqlServer.CodeFactory;
using CatFactory.SqlServer.DatabaseObjectModel;
using CatFactory.SqlServer.ObjectRelationalMapping;

var db = SqlServerDatabase.CreateWithDefaults("InvoizR");

var now = DateTime.Now;

db.AddDefaultTypeMapFor(typeof(string), "nvarchar");
db.AddDefaultTypeMapFor(typeof(DateTime), "datetime");
db.AddDefaultTypeMapFor(typeof(byte[]), "varbinary(max)");

var enumDescription = db
    .DefineEntity(new
    {
        Id = 0,
        FullName = "",
        Value = 0,
        Desc = ""
    })
    .SetNaming("EnumDescription")
    .SetColumnFor(e => e.FullName, 200)
    .SetColumnFor(e => e.Desc, 50)
    .SetPrimaryKey(e => e.Id)
    .SetIdentity(e => e.Id)
    .AddUnique(e => new { e.FullName, e.Value })
    ;

var company = db
    .DefineEntity(new
    {
        Id = (short)0,
        Environment = "",
        Name = "",
        Code = "",
        BusinessName = "",
        TaxIdNumber = "",
        TaxpayerRegistrationNumber = "",
        EconomicActivityId = "",
        EconomicActivity = "",
        CountryLevelId = (short)0,
        Address = "",
        Phone = "",
        Email = "",
        Logo = Array.Empty<byte>(),
        Headquarters = 0,
        NonCustomerEmail = "",
        WebhookNotificationProtocol = "",
        WebhookNotificationAddress = "",
        WebhookNotificationMisc1 = "",
        WebhookNotificationMisc2 = ""
    })
    .SetNaming("Company")
    .SetColumnFor(e => e.Environment, 2)
    .SetColumnFor(e => e.Name, 100)
    .SetColumnFor(e => e.Code, 50)
    .SetColumnFor(e => e.BusinessName, 100)
    .SetColumnFor(e => e.TaxIdNumber, 25)
    .SetColumnFor(e => e.TaxpayerRegistrationNumber, 10)
    .SetColumnFor(e => e.EconomicActivityId, 10)
    .SetColumnFor(e => e.EconomicActivity, 50)
    .SetColumnFor(e => e.Address, 100)
    .SetColumnFor(e => e.Phone, 25)
    .SetColumnFor(e => e.Email, 50)
    .SetColumnFor(e => e.Logo, nullable: true)
    .SetColumnFor(e => e.Headquarters, true)
    .SetColumnFor(e => e.NonCustomerEmail, 50, true)
    .SetColumnFor(e => e.WebhookNotificationProtocol, 25, true)
    .SetColumnFor(e => e.WebhookNotificationAddress, 100, true)
    .SetColumnFor(e => e.WebhookNotificationMisc1, 50, true)
    .SetColumnFor(e => e.WebhookNotificationMisc2, 50, true)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => e.Name)
    .AddUnique(e => e.BusinessName)
    .AddUnique(e => e.TaxIdNumber)
    .AddUnique(e => e.TaxpayerRegistrationNumber)
    ;

var thirdPartyService = db
    .DefineEntity(new
    {
        Id = (short)0,
        EnvironmentId = "",
        Name = "",
        Description = ""
    })
    .SetNaming("ThirdPartyService")
    .SetColumnFor(e => e.EnvironmentId, 2)
    .SetColumnFor(e => e.Name, 50)
    .SetColumnFor(e => e.Description, 200)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => new { e.EnvironmentId, e.Name })
    ;

var thirdPartyServiceParameter = db
    .DefineEntity(new
    {
        Id = (short)0,
        ThirdPartyServiceId = (short)0,
        Category = "",
        Name = "",
        DefaultValue = "",
        RequiresEncryption = false
    })
    .SetNaming("ThirdPartyServiceParameter")
    .SetColumnFor(e => e.Category, 25)
    .SetColumnFor(e => e.Name, 25)
    .SetColumnFor(e => e.DefaultValue, 100)
    .SetColumnFor(e => e.RequiresEncryption, true)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => new { e.ThirdPartyServiceId, e.Category, e.Name })
    .AddForeignKey(e => e.ThirdPartyServiceId, thirdPartyService.Table)
    ;

var companyThirdPartyServiceParameter = db
    .DefineEntity(new
    {
        Id = (short)0,
        CompanyId = (short)0,
        ThirdPartyServiceId = (short)0,
        EnvironmentId = "",
        Category = "",
        Name = "",
        Value = "",
    })
    .SetNaming("CompanyThirdPartyServiceParameter")
    .SetColumnFor(e => e.EnvironmentId, 2)
    .SetColumnFor(e => e.Category, 25)
    .SetColumnFor(e => e.Name, 25)
    .SetColumnFor(e => e.Value, 100)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => new { e.CompanyId, e.ThirdPartyServiceId, e.EnvironmentId, e.Category, e.Name })
    .AddForeignKey(e => e.CompanyId, company.Table)
    .AddForeignKey(e => e.ThirdPartyServiceId, thirdPartyService.Table)
    ;

var responsible = db
    .DefineEntity(new
    {
        Id = (short)0,
        CompanyId = (short)0,
        Name = "",
        Phone = "",
        Email = "",
        IdType = "",
        IdNumber = "",
        AuthorizeCancellation = false,
        AuthorizeFallback = false
    })
    .SetNaming("Responsible")
    .SetColumnFor(e => e.Name, 100)
    .SetColumnFor(e => e.Email, 50)
    .SetColumnFor(e => e.IdType, 2)
    .SetColumnFor(e => e.IdNumber, 25)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => new { e.CompanyId, e.Email })
    .AddForeignKey(e => e.CompanyId, company.Table)
    ;

var branch = db
    .DefineEntity(new
    {
        Id = (short)0,
        CompanyId = (short)0,
        Name = "",
        TaxAuthId = "",
        Address = "",
        Phone = "",
        Email = "",
        Logo = Array.Empty<byte>(),
        Headquarters = 0,
        ResponsibleId = (short)0,
        NonCustomerEmail = ""
    })
    .SetNaming("Branch")
    .SetColumnFor(e => e.Name, 100)
    .SetColumnFor(e => e.TaxAuthId, 4, true)
    .SetColumnFor(e => e.Address, 100)
    .SetColumnFor(e => e.Phone, 25)
    .SetColumnFor(e => e.Email, 50)
    .SetColumnFor(e => e.Logo, nullable: true)
    .SetColumnFor(e => e.Headquarters, true)
    .SetColumnFor(e => e.ResponsibleId, true)
    .SetColumnFor(e => e.NonCustomerEmail, 50, true)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => new { e.CompanyId, e.Name })
    .AddForeignKey(e => e.CompanyId, company.Table)
    .AddForeignKey(e => e.ResponsibleId, responsible.Table)
    ;

var pos = db
    .DefineEntity(new
    {
        Id = (short)0,
        BranchId = (short)0,
        Name = "",
        Code = "",
        TaxAuthId = "",
        Description = ""
    })
    .SetNaming("Pos")
    .SetColumnFor(e => e.Name, 25)
    .SetColumnFor(e => e.Code, 5)
    .SetColumnFor(e => e.TaxAuthId, 4, true)
    .SetColumnFor(e => e.Description, nullable: true)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => new { e.BranchId, e.Name })
    .AddUnique(e => new { e.BranchId, e.TaxAuthId })
    .AddForeignKey(e => e.BranchId, branch.Table)
    ;

var invoiceType = db
    .DefineEntity(new
    {
        Id = (short)0,
        Name = "",
        SchemaType = "",
        SchemaVersion = (short)0,
        Current = false,
        CancellationPeriodInDays = (short)0
    })
    .SetNaming("InvoiceType")
    .SetColumnFor(e => e.Name, 100)
    .SetColumnFor(e => e.SchemaType, 2)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => e.Name)
    ;

var branchNotification = db
    .DefineEntity(new
    {
        Id = (short)0,
        BranchId = (short)0,
        InvoiceTypeId = (short)0,
        Email = "",
        Bcc = false
    })
    .SetNaming("BranchNotification")
    .SetColumnFor(e => e.Email, 50)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddForeignKey(e => e.BranchId, branch.Table)
    .AddForeignKey(e => e.InvoiceTypeId, invoiceType.Table)
    .AddUnique(e => new { e.BranchId, e.InvoiceTypeId, e.Email })
    ;

var fallback = db
    .DefineEntity(new
    {
        Id = (short)0,
        CompanyId = (short)0,
        Name = "",
        StartDateTime = now,
        EndDateTime = now,
        Enable = false,
        FallbackGuid = "",
        SyncStatusId = (short)0,
        Payload = "",
        RetryIn = 0,
        SyncAttempts = 0,
        EmitDateTime = now,
        ReceiptStamp = ""
    })
    .SetNaming("Fallback")
    .SetColumnFor(e => e.CompanyId, true)
    .SetColumnFor(e => e.Name, 100)
    .SetColumnFor(e => e.FallbackGuid, 50, true)
    .SetColumnFor(e => e.EndDateTime, true)
    .SetColumnFor(e => e.EmitDateTime, true)
    .SetColumnFor(e => e.ReceiptStamp, 50, true)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => new { e.CompanyId, e.Name })
    .AddForeignKey(e => e.CompanyId, company.Table)
    ;

var fallbackProcessingLog = db
    .DefineEntity(new
    {
        Id = 0,
        FallbackId = (short)0,
        SyncStatusId = (short)0,
        LogType = "",
        ContentType = "",
        Content = ""
    })
    .SetNaming("FallbackProcessingLog")
    .SetColumnFor(e => e.LogType, 25)
    .SetColumnFor(e => e.ContentType, 50)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddForeignKey(e => e.FallbackId, fallback.Table)
    ;

var fallbackFile = db
    .DefineEntity(new
    {
        Id = (long)0,
        FallbackId = (short)0,
        Size = (long)0,
        MimeType = "",
        FileType = "",
        FileName = "",
        ExternalUrl = "",
        File = Array.Empty<byte>()
    })
    .SetNaming("FallbackFile")
    .SetColumnFor(e => e.MimeType, 100)
    .SetColumnFor(e => e.FileType, 100)
    .SetColumnFor(e => e.FileName, 100)
    .SetColumnFor(e => e.ExternalUrl, 200, true)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => new { e.FallbackId, e.FileName })
    .AddForeignKey(e => e.FallbackId, fallback.Table)
    ;

var invoice = db
    .DefineEntity(new
    {
        Id = (long)0,
        FallbackId = (short)0,
        PosId = (short)0,
        CustomerId = "",
        CustomerDocumentTypeId = "",
        CustomerDocumentNumber = "",
        CustomerTaxpayerRegistrationNumber = "",
        CustomerWtId = "",
        CustomerName = "",
        CustomerCountryId = "",
        CustomerCountryLevelId = (short)0,
        CustomerAddress = "",
        CustomerPhone = "",
        CustomerEmail = "",
        CustomerLastUpdated = now,
        InvoiceTypeId = (short)0,
        InvoiceNumber = (long)0,
        InvoiceDate = now,
        InvoiceTotal = 0m,
        Lines = 0,
        SchemaType = "",
        SchemaVersion = (short)0,
        InvoiceGuid = "",
        AuditNumber = "",
        Payload = "",
        ProcessingTypeId = (short)0,
        SyncStatusId = (short)0,
        RetryIn = 0,
        SyncAttempts = 0,
        EmitDateTime = now,
        ReceiptStamp = "",
        CancellationPayload = "",
        CancellationProcessingStatusId = (short)0,
        CancellationDateTime = now,
        ExternalUrl = "",
        Notes = ""
    })
    .SetNaming("Invoice")
    .SetColumnFor(e => e.FallbackId, true)
    .SetColumnFor(e => e.CustomerId, 30, true)
    .SetColumnFor(e => e.CustomerDocumentTypeId, 2, true)
    .SetColumnFor(e => e.CustomerDocumentNumber, 25, true)
    .SetColumnFor(e => e.CustomerTaxpayerRegistrationNumber, 10, true)
    .SetColumnFor(e => e.CustomerWtId, 5, true)
    .SetColumnFor(e => e.CustomerName, 100, true)
    .SetColumnFor(e => e.CustomerCountryId, 3, true)
    .SetColumnFor(e => e.CustomerCountryLevelId, true)
    .SetColumnFor(e => e.CustomerAddress, 100, true)
    .SetColumnFor(e => e.CustomerPhone, 25, true)
    .SetColumnFor(e => e.CustomerEmail, 100, true)
    .SetColumnFor(e => e.CustomerLastUpdated, nullable: true)
    .SetColumnFor(e => e.InvoiceTotal, 14, 6)
    .SetColumnFor(e => e.SchemaType, 2, true)
    .SetColumnFor(e => e.SchemaVersion, true)
    .SetColumnFor(e => e.InvoiceGuid, 50, true)
    .SetColumnFor(e => e.AuditNumber, 50, true)
    .SetColumnFor(e => e.Payload, nullable: true)
    .SetColumnFor(e => e.RetryIn, true)
    .SetColumnFor(e => e.SyncAttempts, true)
    .SetColumnFor(e => e.EmitDateTime, true)
    .SetColumnFor(e => e.ReceiptStamp, 50, true)
    .SetColumnFor(e => e.CancellationPayload, nullable: true)
    .SetColumnFor(e => e.CancellationProcessingStatusId, true)
    .SetColumnFor(e => e.CancellationDateTime, true)
    .SetColumnFor(e => e.ExternalUrl, 125, true)
    .SetColumnFor(e => e.Notes, nullable: true)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => new { e.InvoiceTypeId, e.InvoiceNumber })
    .AddForeignKey(e => e.FallbackId, fallback.Table)
    .AddForeignKey(e => e.PosId, pos.Table)
    .AddForeignKey(e => e.InvoiceTypeId, invoiceType.Table)
    ;

var invoiceValidation = db
    .DefineEntity(new
    {
        Id = (long)0,
        InvoiceId = (long)0,
        Field = "",
        Value = "",
        Message = ""
    })
    .SetNaming("InvoiceValidation")
    .SetColumnFor(e => e.Field, 25)
    .SetColumnFor(e => e.Value, 100, true)
    .SetColumnFor(e => e.Message, 100)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddForeignKey(e => e.InvoiceId, invoice.Table)
    ;

var invoiceSyncStatusLog = db
    .DefineEntity(new
    {
        Id = (long)0,
        InvoiceId = (long)0,
        SyncStatusId = (short)0
    })
    .SetNaming("InvoiceSyncStatusLog")
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddForeignKey(e => e.InvoiceId, invoice.Table)
    ;

var invoiceSyncLog = db
    .DefineEntity(new
    {
        Id = (long)0,
        InvoiceId = (long)0,
        SyncStatusId = (short)0,
        LogType = "",
        ContentType = "",
        Content = ""
    })
    .SetNaming("InvoiceSyncLog")
    .SetColumnFor(e => e.LogType, 25)
    .SetColumnFor(e => e.ContentType, 50)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddForeignKey(e => e.InvoiceId, invoice.Table)
    ;

var invoiceFile = db
    .DefineEntity(new
    {
        Id = (long)0,
        InvoiceId = (long)0,
        Size = (long)0,
        MimeType = "",
        FileType = "",
        FileName = "",
        ExternalUrl = "",
        File = Array.Empty<byte>()
    })
    .SetNaming("InvoiceFile")
    .SetColumnFor(e => e.MimeType, 100)
    .SetColumnFor(e => e.FileType, 100)
    .SetColumnFor(e => e.FileName, 100)
    .SetColumnFor(e => e.ExternalUrl, 200, true)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => new { e.InvoiceId, e.FileName })
    .AddForeignKey(e => e.InvoiceId, invoice.Table)
    ;

var invoiceNotification = db
    .DefineEntity(new
    {
        Id = (long)0,
        InvoiceId = (long)0,
        Email = "",
        Bcc = false,
        Files = (short)0,
        Successful = false
    })
    .SetNaming("InvoiceNotification")
    .SetColumnFor(e => e.Email, 50)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddForeignKey(e => e.InvoiceId, invoice.Table)
    ;

var invoiceWebhookNotification = db
    .DefineEntity(new
    {
        Id = (long)0,
        InvoiceId = (long)0,
        Protocol = "",
        Address = "",
        ContentType = "",
        IsSuccess = false,
        Request = "",
        Response = ""
    })
    .SetNaming("InvoiceWebhookNotification")
    .SetColumnFor(e => e.Protocol, 25)
    .SetColumnFor(e => e.Address, 100)
    .SetColumnFor(e => e.ContentType, 50)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddForeignKey(e => e.InvoiceId, invoice.Table)
    ;

var invoiceCancellationLog = db
    .DefineEntity(new
    {
        Id = (long)0,
        InvoiceId = (long)0,
        ProcessingStatusId = (short)0,
        LogType = "",
        ContentType = "",
        Payload = ""
    })
    .SetNaming("InvoiceCancellationLog")
    .SetColumnFor(e => e.LogType, 25)
    .SetColumnFor(e => e.ContentType, 50)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddForeignKey(e => e.InvoiceId, invoice.Table)
    ;

dynamic importBag = new ExpandoObject();
importBag.ExtendedProperties = new List<ExtendedProperty>();

db.AddColumnForTables(new Column { Name = "CreatedAt", Type = "datetime", ImportBag = importBag }, enumDescription.Table.FullName);
db.AddColumnForTables(new Column { Name = "CreatedBy", Type = "nvarchar", Length = 50, ImportBag = importBag }, enumDescription.Table.FullName);
db.AddColumnForTables(new Column { Name = "LastModifiedAt", Type = "datetime", Nullable = true, ImportBag = importBag }, enumDescription.Table.FullName);
db.AddColumnForTables(new Column { Name = "LastModifiedBy", Type = "nvarchar", Length = 50, Nullable = true, ImportBag = importBag }, enumDescription.Table.FullName);
db.AddColumnForTables(new Column { Name = "RowVersion", Type = "rowversion", Nullable = true, ImportBag = importBag }, enumDescription.Table.FullName);

// Add table for special customers
SqlServerDatabaseScriptCodeBuilder.CreateScript(db, @"C:\Temp\Databases", true, true);

// Create instance of Entity Framework Core project
var efCoreProject = EntityFrameworkCoreProject.CreateForV5x("InvoizR.Domain", db, @"C:\Temp\InvoizR.Domain");

// Apply settings for Entity Framework Core project
efCoreProject.GlobalSelection(settings =>
{
    settings.ForceOverwrite = true;
    settings.DeclareNavigationProperties = true;
    settings.DeclareNavigationPropertiesAsVirtual = true;
    settings.AddConfigurationForForeignKeysInFluentAPI = true;
    settings.DeclareNavigationPropertiesAsVirtual = true;
});

// Build features for project, group all entities by schema into a feature
efCoreProject.BuildFeatures();

// Scaffolding =^^=
efCoreProject.ScaffoldDomain();

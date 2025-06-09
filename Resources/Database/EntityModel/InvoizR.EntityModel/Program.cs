using CatFactory.EntityFrameworkCore;
using CatFactory.SqlServer;
using CatFactory.SqlServer.CodeFactory;
using CatFactory.SqlServer.ObjectRelationalMapping;

var db = SqlServerDatabase.CreateWithDefaults("Invoizr");

var now = DateTime.Now;

db.AddDefaultTypeMapFor(typeof(string), "nvarchar");
db.AddDefaultTypeMapFor(typeof(DateTime), "datetime");
db.AddDefaultTypeMapFor(typeof(byte[]), "varbinary(max)");

var enumDescription = db
    .DefineEntity(new
    {
        Id = 0,
        Desc = "",
        FullName = ""
    })
    .SetNaming("EnumDescription")
    .SetColumnFor(e => e.Desc, 50)
    .SetColumnFor(e => e.FullName, 200)
    .SetPrimaryKey(e => e.Id)
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
        TaxRegistrationNumber = "",
        EconomicActivityId = "",
        EconomicActivity = "",
        CountryLevelId = (short)0,
        Address = "",
        Phone = "",
        Email = "",
        Logo = Array.Empty<byte>(),
        Headquarters = 0
    })
    .SetNaming("Company")
    .SetColumnFor(e => e.Environment, 2)
    .SetColumnFor(e => e.Name, 100)
    .SetColumnFor(e => e.Code, 50)
    .SetColumnFor(e => e.BusinessName, 100)
    .SetColumnFor(e => e.TaxIdNumber, 25)
    .SetColumnFor(e => e.TaxRegistrationNumber, 20)
    .SetColumnFor(e => e.EconomicActivityId, 10)
    .SetColumnFor(e => e.EconomicActivity, 50)
    .SetColumnFor(e => e.Address, 100)
    .SetColumnFor(e => e.Phone, 25)
    .SetColumnFor(e => e.Email, 50)
    .SetColumnFor(e => e.Logo, nullable: true)
    .SetColumnFor(e => e.Headquarters, true)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => e.Name)
    .AddUnique(e => e.BusinessName)
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
        AuthorizeContingency = false
    })
    .SetNaming("Responsible")
    .SetColumnFor(e => e.Name, 100)
    .SetColumnFor(e => e.Email, 25)
    .SetColumnFor(e => e.Email, 50)
    .SetColumnFor(e => e.IdType, 2)
    .SetColumnFor(e => e.IdNumber, 25)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => e.Email)
    .AddForeignKey(e => e.CompanyId, company.Table)
    ;

var branch = db
    .DefineEntity(new
    {
        Id = (short)0,
        CompanyId = (short)0,
        Name = "",
        EstablishmentPrefix = "",
        TaxAuthId = "",
        Address = "",
        Phone = "",
        Email = "",
        Logo = Array.Empty<byte>(),
        Headquarters = 0,
        ResponsibleId = (short)0
    })
    .SetNaming("Branch")
    .SetColumnFor(e => e.Name, 100)
    .SetColumnFor(e => e.EstablishmentPrefix, 5)
    .SetColumnFor(e => e.TaxAuthId, 4, true)
    .SetColumnFor(e => e.Address, 100)
    .SetColumnFor(e => e.Phone, 25)
    .SetColumnFor(e => e.Email, 50)
    .SetColumnFor(e => e.Logo, nullable: true)
    .SetColumnFor(e => e.Headquarters, true)
    .SetColumnFor(e => e.ResponsibleId, true)
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
        TaxAuthPos = "",
        Description = ""
    })
    .SetNaming("Pos")
    .SetColumnFor(e => e.Name, 25)
    .SetColumnFor(e => e.Code, 5)
    .SetColumnFor(e => e.TaxAuthPos, 4, true)
    .SetColumnFor(e => e.Description, nullable: true)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => new { e.BranchId, e.Name })
    .AddForeignKey(e => e.BranchId, branch.Table)
    ;

var invoiceType = db
    .DefineEntity(new
    {
        Id = (short)0,
        Name = "",
        SchemaType = "",
        SchemaVersion = (short)0,
        Current = false
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

var contingencyReason = db
    .DefineEntity(new
    {
        Id = (short)0,
        Name = "",
        RequireDesc = false
    })
    .SetNaming("ContingencyReason")
    .SetColumnFor(e => e.Name, 200)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => e.Name)
    ;

var contingency = db
    .DefineEntity(new
    {
        Id = (short)0,
        BranchId = (short)0,
        Name = "",
        StartDateTime = now,
        EndDateTime = now,
        Enable = false,
        GenerationCode = "",
        SyncStatusId = (short)0,
        TransmisionDateTime = now,
        ContingencyResponsibleId = (short)0,
        ContingencyReasonId = (short)0,
        ContingencyReasonDesc = ""
    })
    .SetNaming("Contingency")
    .SetColumnFor(e => e.BranchId, true)
    .SetColumnFor(e => e.Name, 100)
    .SetColumnFor(e => e.GenerationCode, 50, true)
    .SetColumnFor(e => e.EndDateTime, true)
    .SetColumnFor(e => e.TransmisionDateTime, true)
    .SetColumnFor(e => e.ContingencyReasonDesc, 500, true)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => e.Name)
    .AddForeignKey(e => e.BranchId, branch.Table)
    .AddForeignKey(e => e.ContingencyResponsibleId, responsible.Table)
    .AddForeignKey(e => e.ContingencyReasonId, contingencyReason.Table)
    ;

var contingencyProcessingLog = db
    .DefineEntity(new
    {
        Id = 0,
        ContingencyId = (short)0,
        CreatedAt = now,
        SyncStatusId = (short)0,
        LogType = "",
        ContentType = "",
        Content = ""
    })
    .SetNaming("ContingencyProcessingLog")
    .SetColumnFor(e => e.LogType, 25)
    .SetColumnFor(e => e.ContentType, 100)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddForeignKey(e => e.ContingencyId, contingency.Table)
    ;

var invoice = db
    .DefineEntity(new
    {
        Id = (long)0,
        ContingencyId = (short)0,
        PosId = (short)0,
        CustomerId = "",
        CustomerDocumentTypeId = "",
        CustomerDocumentNumber = "",
        CustomerWtId = "",
        CustomerName = "",
        CustomerCountryId = "",
        CustomerCountryLevelId = (short)0,
        CustomerAddress = "",
        CustomerPhone = "",
        CustomerEmail = "",
        CustomerLastUpdated = now,
        CreatedAt = now,
        InvoiceTypeId = (short)0,
        InvoiceNumber = (long)0,
        InvoiceDate = now,
        InvoiceTotal = 0m,
        Lines = 0,
        SchemaType = "",
        SchemaVersion = (short)0,
        GenerationCode = "",
        ControlNumber = "",
        Serialization = "",
        ProcessingTypeId = (short)0,
        ProcessingStatusId = (short)0,
        RetryIn = 0,
        SyncAttempts = 0,
        ProcessingDateTime = now,
        ReceiptStamp = "",
        ExternalUrl = "",
        Notes = ""
    })
    .SetNaming("Invoice")
    .SetColumnFor(e => e.ContingencyId, true)
    .SetColumnFor(e => e.CustomerId, 30, true)
    .SetColumnFor(e => e.CustomerDocumentTypeId, 2, true)
    .SetColumnFor(e => e.CustomerDocumentNumber, 25, true)
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
    .SetColumnFor(e => e.GenerationCode, 50, true)
    .SetColumnFor(e => e.ControlNumber, 50, true)
    .SetColumnFor(e => e.Serialization, nullable: true)
    .SetColumnFor(e => e.RetryIn, true)
    .SetColumnFor(e => e.SyncAttempts, true)
    .SetColumnFor(e => e.ProcessingDateTime, true)
    .SetColumnFor(e => e.ReceiptStamp, 50, true)
    .SetColumnFor(e => e.ExternalUrl, 125, true)
    .SetColumnFor(e => e.Notes, nullable: true)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => new { e.InvoiceTypeId, e.InvoiceNumber })
    .AddForeignKey(e => e.ContingencyId, contingency.Table)
    .AddForeignKey(e => e.PosId, pos.Table)
    ;

var invoiceValidation = db
    .DefineEntity(new
    {
        Id = (long)0,
        InvoiceId = (long)0,
        Field = "",
        Value = "",
        Message = "",
        CreatedAt = now
    })
    .SetNaming("InvoiceValidation")
    .SetColumnFor(e => e.Field, 25)
    .SetColumnFor(e => e.Value, 100, true)
    .SetColumnFor(e => e.Message, 100)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddForeignKey(e => e.InvoiceId, invoice.Table)
    ;

var invoiceProcessingStatusLog = db
    .DefineEntity(new
    {
        Id = (long)0,
        InvoiceId = (long)0,
        CreatedAt = now,
        ProcessingStatusId = (short)0
    })
    .SetNaming("InvoiceProcessingStatusLog")
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddForeignKey(e => e.InvoiceId, invoice.Table)
    ;

var invoiceProcessingLog = db
    .DefineEntity(new
    {
        Id = (long)0,
        InvoiceId = (long)0,
        CreatedAt = now,
        ProcessingStatusId = (short)0,
        LogType = "",
        ContentType = "",
        Content = ""
    })
    .SetNaming("InvoiceProcessingLog")
    .SetColumnFor(e => e.LogType, 25)
    .SetColumnFor(e => e.ContentType, 100)
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
        CreatedAt = now,
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
        Successful = false,
        CreatedAt = now
    })
    .SetNaming("InvoiceNotification")
    .SetColumnFor(e => e.Email, 50)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddForeignKey(e => e.InvoiceId, invoice.Table)
    ;

// Add table for special customers
SqlServerDatabaseScriptCodeBuilder.CreateScript(db, @"C:\Temp\Databases", true, true);

// Create instance of Entity Framework Core project
var efCoreProject = EntityFrameworkCoreProject
    .CreateForV5x("EB.Domain.Core", db, @"C:\Temp\Invoizr.Domain");

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
efCoreProject
    .ScaffoldDomain()
    ;

IF OBJECT_ID('dbo.InvoiceNotification') IS NOT NULL
	DROP TABLE [dbo].[InvoiceNotification]
GO

IF OBJECT_ID('dbo.InvoiceFile') IS NOT NULL
	DROP TABLE [dbo].[InvoiceFile]
GO

IF OBJECT_ID('dbo.InvoiceProcessingLog') IS NOT NULL
	DROP TABLE [dbo].[InvoiceProcessingLog]
GO

IF OBJECT_ID('dbo.InvoiceProcessingStatusLog') IS NOT NULL
	DROP TABLE [dbo].[InvoiceProcessingStatusLog]
GO

IF OBJECT_ID('dbo.InvoiceValidation') IS NOT NULL
	DROP TABLE [dbo].[InvoiceValidation]
GO

IF OBJECT_ID('dbo.Invoice') IS NOT NULL
	DROP TABLE [dbo].[Invoice]
GO

IF OBJECT_ID('dbo.ContingencyProcessingLog') IS NOT NULL
	DROP TABLE [dbo].[ContingencyProcessingLog]
GO

IF OBJECT_ID('dbo.Contingency') IS NOT NULL
	DROP TABLE [dbo].[Contingency]
GO

IF OBJECT_ID('dbo.ContingencyReason') IS NOT NULL
	DROP TABLE [dbo].[ContingencyReason]
GO

IF OBJECT_ID('dbo.BranchNotification') IS NOT NULL
	DROP TABLE [dbo].[BranchNotification]
GO

IF OBJECT_ID('dbo.InvoiceType') IS NOT NULL
	DROP TABLE [dbo].[InvoiceType]
GO

IF OBJECT_ID('dbo.Pos') IS NOT NULL
	DROP TABLE [dbo].[Pos]
GO

IF OBJECT_ID('dbo.Branch') IS NOT NULL
	DROP TABLE [dbo].[Branch]
GO

IF OBJECT_ID('dbo.Company') IS NOT NULL
	DROP TABLE [dbo].[Company]
GO

IF OBJECT_ID('dbo.Responsible') IS NOT NULL
	DROP TABLE [dbo].[Responsible]
GO

IF OBJECT_ID('dbo.EnumDescription') IS NOT NULL
	DROP TABLE [dbo].[EnumDescription]
GO

CREATE TABLE [dbo].[EnumDescription]
(
	[Id] INT NOT NULL,
	[Desc] NVARCHAR(50) NOT NULL,
	[FullName] NVARCHAR(200) NOT NULL
)
GO

CREATE TABLE [dbo].[Responsible]
(
	[Id] SMALLINT NOT NULL IDENTITY(1, 1),
	[CompanyId] SMALLINT NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[Phone] NVARCHAR(MAX) NOT NULL,
	[Email] NVARCHAR(50) NOT NULL,
	[IdType] NVARCHAR(2) NOT NULL,
	[IdNumber] NVARCHAR(25) NOT NULL,
	[AuthorizeCancellation] BIT NOT NULL,
	[AuthorizeContingency] BIT NOT NULL
)
GO

CREATE TABLE [dbo].[Company]
(
	[Id] SMALLINT NOT NULL IDENTITY(1, 1),
	[Environment] NVARCHAR(2) NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[Code] NVARCHAR(50) NOT NULL,
	[BusinessName] NVARCHAR(100) NOT NULL,
	[TaxIdNumber] NVARCHAR(25) NOT NULL,
	[TaxRegistrationNumber] NVARCHAR(20) NOT NULL,
	[EconomicActivityId] NVARCHAR(10) NOT NULL,
	[EconomicActivity] NVARCHAR(50) NOT NULL,
	[CountryLevelId] SMALLINT NOT NULL,
	[Address] NVARCHAR(100) NOT NULL,
	[Phone] NVARCHAR(25) NOT NULL,
	[Email] NVARCHAR(50) NOT NULL,
	[Logo] VARBINARY(MAX) NULL,
	[Headquarters] INT NULL,
	[NonCustomerEmail] NVARCHAR(50) NULL
)
GO

CREATE TABLE [dbo].[Branch]
(
	[Id] SMALLINT NOT NULL IDENTITY(1, 1),
	[CompanyId] SMALLINT NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[EstablishmentPrefix] NVARCHAR(5) NOT NULL,
	[TaxAuthId] NVARCHAR(4) NULL,
	[Address] NVARCHAR(100) NOT NULL,
	[Phone] NVARCHAR(25) NOT NULL,
	[Email] NVARCHAR(50) NOT NULL,
	[Logo] VARBINARY(MAX) NULL,
	[Headquarters] INT NULL,
	[ResponsibleId] SMALLINT NULL,
	[NonCustomerEmail] NVARCHAR(50) NULL
)
GO

CREATE TABLE [dbo].[Pos]
(
	[Id] SMALLINT NOT NULL IDENTITY(1, 1),
	[BranchId] SMALLINT NOT NULL,
	[Name] NVARCHAR(25) NOT NULL,
	[Code] NVARCHAR(5) NOT NULL,
	[TaxAuthPos] NVARCHAR(4) NULL,
	[Description] NVARCHAR(MAX) NULL
)
GO

CREATE TABLE [dbo].[InvoiceType]
(
	[Id] SMALLINT NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[SchemaType] NVARCHAR(2) NOT NULL,
	[SchemaVersion] SMALLINT NOT NULL,
	[Current] BIT NOT NULL
)
GO

CREATE TABLE [dbo].[BranchNotification]
(
	[Id] SMALLINT NOT NULL IDENTITY(1, 1),
	[BranchId] SMALLINT NOT NULL,
	[InvoiceTypeId] SMALLINT NOT NULL,
	[Email] NVARCHAR(50) NOT NULL,
	[Bcc] BIT NOT NULL
)
GO

CREATE TABLE [dbo].[ContingencyReason]
(
	[Id] SMALLINT NOT NULL IDENTITY(1, 1),
	[Name] NVARCHAR(200) NOT NULL,
	[RequireDesc] BIT NOT NULL
)
GO

CREATE TABLE [dbo].[Contingency]
(
	[Id] SMALLINT NOT NULL IDENTITY(1, 1),
	[BranchId] SMALLINT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[StartDateTime] DATETIME NOT NULL,
	[EndDateTime] DATETIME NULL,
	[Enable] BIT NOT NULL,
	[GenerationCode] NVARCHAR(50) NULL,
	[SyncStatusId] SMALLINT NOT NULL,
	[TransmisionDateTime] DATETIME NULL,
	[ContingencyResponsibleId] SMALLINT NOT NULL,
	[ContingencyReasonId] SMALLINT NOT NULL,
	[ContingencyReasonDesc] NVARCHAR(500) NULL
)
GO

CREATE TABLE [dbo].[ContingencyProcessingLog]
(
	[Id] INT NOT NULL IDENTITY(1, 1),
	[ContingencyId] SMALLINT NOT NULL,
	[CreatedAt] DATETIME NOT NULL,
	[SyncStatusId] SMALLINT NOT NULL,
	[LogType] NVARCHAR(25) NOT NULL,
	[ContentType] NVARCHAR(100) NOT NULL,
	[Content] NVARCHAR(MAX) NOT NULL
)
GO

CREATE TABLE [dbo].[Invoice]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1),
	[ContingencyId] SMALLINT NULL,
	[PosId] SMALLINT NOT NULL,
	[CustomerId] NVARCHAR(30) NULL,
	[CustomerDocumentTypeId] NVARCHAR(2) NULL,
	[CustomerDocumentNumber] NVARCHAR(25) NULL,
	[CustomerWtId] NVARCHAR(5) NULL,
	[CustomerName] NVARCHAR(100) NULL,
	[CustomerCountryId] NVARCHAR(3) NULL,
	[CustomerCountryLevelId] SMALLINT NULL,
	[CustomerAddress] NVARCHAR(100) NULL,
	[CustomerPhone] NVARCHAR(25) NULL,
	[CustomerEmail] NVARCHAR(100) NULL,
	[CustomerLastUpdated] DATETIME NULL,
	[CreatedAt] DATETIME NOT NULL,
	[InvoiceTypeId] SMALLINT NOT NULL,
	[InvoiceNumber] BIGINT NOT NULL,
	[InvoiceDate] DATETIME NOT NULL,
	[InvoiceTotal] DECIMAL(14, 6) NOT NULL,
	[Lines] INT NOT NULL,
	[SchemaType] NVARCHAR(2) NULL,
	[SchemaVersion] SMALLINT NULL,
	[GenerationCode] NVARCHAR(50) NULL,
	[ControlNumber] NVARCHAR(50) NULL,
	[Payload] NVARCHAR(MAX) NULL,
	[ProcessingTypeId] SMALLINT NOT NULL,
	[ProcessingStatusId] SMALLINT NOT NULL,
	[RetryIn] INT NULL,
	[SyncAttempts] INT NULL,
	[ProcessingDateTime] DATETIME NULL,
	[ReceiptStamp] NVARCHAR(50) NULL,
	[ExternalUrl] NVARCHAR(125) NULL,
	[Notes] NVARCHAR(MAX) NULL
)
GO

CREATE TABLE [dbo].[InvoiceValidation]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1),
	[InvoiceId] BIGINT NOT NULL,
	[Field] NVARCHAR(25) NOT NULL,
	[Value] NVARCHAR(100) NULL,
	[Message] NVARCHAR(100) NOT NULL,
	[CreatedAt] DATETIME NOT NULL
)
GO

CREATE TABLE [dbo].[InvoiceProcessingStatusLog]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1),
	[InvoiceId] BIGINT NOT NULL,
	[CreatedAt] DATETIME NOT NULL,
	[ProcessingStatusId] SMALLINT NOT NULL
)
GO

CREATE TABLE [dbo].[InvoiceProcessingLog]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1),
	[InvoiceId] BIGINT NOT NULL,
	[CreatedAt] DATETIME NOT NULL,
	[ProcessingStatusId] SMALLINT NOT NULL,
	[LogType] NVARCHAR(25) NOT NULL,
	[ContentType] NVARCHAR(100) NOT NULL,
	[Content] NVARCHAR(MAX) NOT NULL
)
GO

CREATE TABLE [dbo].[InvoiceFile]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1),
	[InvoiceId] BIGINT NOT NULL,
	[Size] BIGINT NOT NULL,
	[MimeType] NVARCHAR(100) NOT NULL,
	[FileType] NVARCHAR(100) NOT NULL,
	[FileName] NVARCHAR(100) NOT NULL,
	[ExternalUrl] NVARCHAR(200) NULL,
	[CreatedAt] DATETIME NOT NULL,
	[File] VARBINARY(MAX) NOT NULL
)
GO

CREATE TABLE [dbo].[InvoiceNotification]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1),
	[InvoiceId] BIGINT NOT NULL,
	[Email] NVARCHAR(50) NOT NULL,
	[Bcc] BIT NOT NULL,
	[Files] SMALLINT NOT NULL,
	[Successful] BIT NOT NULL,
	[CreatedAt] DATETIME NOT NULL
)
GO

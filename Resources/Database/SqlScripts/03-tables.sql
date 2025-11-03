IF OBJECT_ID('dbo.InvoiceCancellationLog') IS NOT NULL
	DROP TABLE [dbo].[InvoiceCancellationLog]
GO

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

IF OBJECT_ID('dbo.FallbackFile') IS NOT NULL
	DROP TABLE [dbo].[FallbackFile]
GO

IF OBJECT_ID('dbo.FallbackProcessingLog') IS NOT NULL
	DROP TABLE [dbo].[FallbackProcessingLog]
GO

IF OBJECT_ID('dbo.Fallback') IS NOT NULL
	DROP TABLE [dbo].[Fallback]
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

IF OBJECT_ID('dbo.Responsible') IS NOT NULL
	DROP TABLE [dbo].[Responsible]
GO

IF OBJECT_ID('dbo.CompanyThirdPartyServiceParameter') IS NOT NULL
	DROP TABLE [dbo].[CompanyThirdPartyServiceParameter]
GO

IF OBJECT_ID('dbo.ThirdPartyServiceParameter') IS NOT NULL
	DROP TABLE [dbo].[ThirdPartyServiceParameter]
GO

IF OBJECT_ID('dbo.ThirdPartyService') IS NOT NULL
	DROP TABLE [dbo].[ThirdPartyService]
GO

IF OBJECT_ID('dbo.Company') IS NOT NULL
	DROP TABLE [dbo].[Company]
GO

IF OBJECT_ID('dbo.EnumDescription') IS NOT NULL
	DROP TABLE [dbo].[EnumDescription]
GO

CREATE TABLE [dbo].[EnumDescription]
(
	[Id] INT NOT NULL IDENTITY(1, 1),
	[FullName] NVARCHAR(200) NOT NULL,
	[Value] INT NOT NULL,
	[Desc] NVARCHAR(50) NOT NULL
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

CREATE TABLE [dbo].[ThirdPartyService]
(
	[Id] SMALLINT NOT NULL IDENTITY(1, 1),
	[EnvironmentId] NVARCHAR(2) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(200) NOT NULL
)
GO

CREATE TABLE [dbo].[ThirdPartyServiceParameter]
(
	[Id] SMALLINT NOT NULL IDENTITY(1, 1),
	[ThirdPartyServiceId] SMALLINT NOT NULL,
	[Category] NVARCHAR(25) NOT NULL,
	[Name] NVARCHAR(25) NOT NULL,
	[DefaultValue] NVARCHAR(100) NOT NULL,
	[RequiresEncryption] BIT NULL
)
GO

CREATE TABLE [dbo].[CompanyThirdPartyServiceParameter]
(
	[Id] SMALLINT NOT NULL IDENTITY(1, 1),
	[CompanyId] SMALLINT NOT NULL,
	[ThirdPartyServiceId] SMALLINT NOT NULL,
	[EnvironmentId] NVARCHAR(2) NOT NULL,
	[Category] NVARCHAR(25) NOT NULL,
	[Name] NVARCHAR(25) NOT NULL,
	[Value] NVARCHAR(100) NOT NULL
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
	[AuthorizeFallback] BIT NOT NULL
)
GO

CREATE TABLE [dbo].[Branch]
(
	[Id] SMALLINT NOT NULL IDENTITY(1, 1),
	[CompanyId] SMALLINT NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
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
	[TaxAuthId] NVARCHAR(4) NULL,
	[Description] NVARCHAR(MAX) NULL
)
GO

CREATE TABLE [dbo].[InvoiceType]
(
	[Id] SMALLINT NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[SchemaType] NVARCHAR(2) NOT NULL,
	[SchemaVersion] SMALLINT NOT NULL,
	[Current] BIT NOT NULL,
	[CancellationPeriodInDays] SMALLINT NOT NULL
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

CREATE TABLE [dbo].[Fallback]
(
	[Id] SMALLINT NOT NULL IDENTITY(1, 1),
	[CompanyId] SMALLINT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[StartDateTime] DATETIME NOT NULL,
	[EndDateTime] DATETIME NULL,
	[Enable] BIT NOT NULL,
	[FallbackGuid] NVARCHAR(50) NULL,
	[SyncStatusId] SMALLINT NOT NULL,
	[Payload] NVARCHAR(MAX) NOT NULL,
	[RetryIn] INT NOT NULL,
	[SyncAttempts] INT NOT NULL,
	[EmitDateTime] DATETIME NULL,
	[ReceiptStamp] NVARCHAR(50) NULL
)
GO

CREATE TABLE [dbo].[FallbackProcessingLog]
(
	[Id] INT NOT NULL IDENTITY(1, 1),
	[FallbackId] SMALLINT NOT NULL,
	[CreatedAt] DATETIME NOT NULL,
	[SyncStatusId] SMALLINT NOT NULL,
	[LogType] NVARCHAR(25) NOT NULL,
	[ContentType] NVARCHAR(100) NOT NULL,
	[Content] NVARCHAR(MAX) NOT NULL
)
GO

CREATE TABLE [dbo].[FallbackFile]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1),
	[FallbackId] SMALLINT NOT NULL,
	[Size] BIGINT NOT NULL,
	[MimeType] NVARCHAR(100) NOT NULL,
	[FileType] NVARCHAR(100) NOT NULL,
	[FileName] NVARCHAR(100) NOT NULL,
	[ExternalUrl] NVARCHAR(200) NULL,
	[CreatedAt] DATETIME NOT NULL,
	[File] VARBINARY(MAX) NOT NULL
)
GO

CREATE TABLE [dbo].[Invoice]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1),
	[FallbackId] SMALLINT NULL,
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
	[InvoiceGuid] NVARCHAR(50) NULL,
	[AuditNumber] NVARCHAR(50) NULL,
	[Payload] NVARCHAR(MAX) NULL,
	[ProcessingTypeId] SMALLINT NOT NULL,
	[ProcessingStatusId] SMALLINT NOT NULL,
	[RetryIn] INT NULL,
	[SyncAttempts] INT NULL,
	[EmitDateTime] DATETIME NULL,
	[ReceiptStamp] NVARCHAR(50) NULL,
	[CancellationPayload] NVARCHAR(MAX) NULL,
	[CancellationProcessingStatusId] SMALLINT NULL,
	[CancellationDateTime] DATETIME NULL,
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

CREATE TABLE [dbo].[InvoiceCancellationLog]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1),
	[InvoiceId] BIGINT NOT NULL,
	[ProcessingStatusId] SMALLINT NOT NULL,
	[CreatedAt] DATETIME NOT NULL,
	[LogType] NVARCHAR(25) NOT NULL,
	[ContentType] NVARCHAR(100) NOT NULL,
	[Payload] NVARCHAR(MAX) NOT NULL
)
GO

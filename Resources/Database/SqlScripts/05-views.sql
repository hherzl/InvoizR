IF OBJECT_ID('dbo.VInvoiceSyncStatus') IS NOT NULL
	DROP VIEW [dbo].[VInvoiceSyncStatus]
GO

CREATE VIEW [VInvoiceSyncStatus]
AS
	SELECT [Value] AS [Id], [Desc] FROM [EnumDescription] WHERE [FullName] = 'InvoizR.Domain.Enums.SyncStatus'
GO

IF OBJECT_ID('dbo.VInvoiceProcessingType') IS NOT NULL
	DROP VIEW [dbo].[VInvoiceProcessingType]
GO

CREATE VIEW [VInvoiceProcessingType]
AS
	SELECT [Value] AS [Id], [Desc] FROM [EnumDescription] WHERE [FullName] = 'InvoizR.Domain.Enums.InvoiceProcessingType'
GO

IF OBJECT_ID('dbo.VInvoice') IS NOT NULL
	DROP VIEW [dbo].[VInvoice]
GO

CREATE VIEW [dbo].[VInvoice]
AS
    SELECT [Id], [InvoiceTypeId], [InvoiceDate], [InvoiceGuid], [AuditNumber], [SyncStatusId] FROM [dbo].[Invoice]
GO

IF OBJECT_ID('dbo.VInvoiceProcessingStatus') IS NOT NULL
	DROP VIEW [dbo].[VInvoiceProcessingStatus]
GO

CREATE VIEW [VInvoiceProcessingStatus]
AS
	SELECT [Value] AS [Id], [Desc] FROM [EnumDescription] WHERE [FullName] = 'InvoizR.Domain.Enums.InvoiceProcessingStatus'
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
    SELECT [Id] FROM [dbo].[Invoice]
GO

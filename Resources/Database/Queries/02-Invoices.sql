USE InvoizR
GO

SELECT Id, PosId, InvoiceTypeId, InvoiceNumber, InvoiceDate, InvoiceTotal, Lines, ProcessingStatusId, CustomerName
FROM dbo.Invoice
ORDER BY Id DESC

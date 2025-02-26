USE InvoizR
GO

DECLARE @id BIGINT
SELECT @id = 10

SELECT * FROM dbo.Invoice WHERE Id = @id
SELECT * FROM dbo.InvoiceProcessingStatusLog WHERE InvoiceId = @id
SELECT * FROM dbo.InvoiceProcessingLog WHERE InvoiceId = @id
SELECT * FROM dbo.InvoiceNotification WHERE InvoiceId = @id
SELECT * FROM dbo.InvoiceFile WHERE InvoiceId = @id

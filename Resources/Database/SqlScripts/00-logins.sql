IF NOT EXISTS (SELECT name FROM master.sys.sql_logins WHERE name = 'invoizr.api.billing')
	BEGIN
		CREATE LOGIN [invoizr.api.billing] WITH PASSWORD = 'InvoizR2025$'
	END

IF NOT EXISTS (SELECT name FROM master.sys.sql_logins WHERE name = 'invoizr.api.reports')
	BEGIN
		CREATE LOGIN [invoizr.api.reports] WITH PASSWORD = 'InvoizR2025$'
	END

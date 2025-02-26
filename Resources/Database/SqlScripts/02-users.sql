IF NOT EXISTS(SELECT name FROM sys.sysusers WHERE name = 'invoizr.api.billing')
    BEGIN
        CREATE USER [invoizr.api.billing] FOR LOGIN [invoizr.api.billing]
        EXEC [sp_addrolemember] N'db_owner', N'invoizr.api.billing'
    END

IF NOT EXISTS(SELECT name FROM sys.sysusers WHERE name = 'invoizr.api.reports')
    BEGIN
        CREATE USER [invoizr.api.reports] FOR LOGIN [invoizr.api.reports]
        EXEC [sp_addrolemember] N'db_owner', N'invoizr.api.reports'
    END

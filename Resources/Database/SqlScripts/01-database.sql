IF NOT EXISTS(SELECT name FROM master.sys.databases WHERE name = 'InvoizR')
    BEGIN
        CREATE DATABASE [InvoizR]
    END

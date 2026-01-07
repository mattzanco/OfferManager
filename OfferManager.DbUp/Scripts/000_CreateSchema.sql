-- Create the offermanager schema
-- This script should run before any tables are created

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'offermanager')
BEGIN
    EXEC sp_executesql N'CREATE SCHEMA offermanager'
    PRINT 'Created schema: offermanager'
END
ELSE
BEGIN
    PRINT 'Schema offermanager already exists'
END

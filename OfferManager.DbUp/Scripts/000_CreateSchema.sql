-- Create the offermanager schema
-- This script should run before any tables are created

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'OfferManager')
BEGIN
    EXEC sp_executesql N'CREATE SCHEMA OfferManager'
    PRINT 'Created schema: OfferManager'
END
ELSE
BEGIN
    PRINT 'Schema OfferManager already exists'
END

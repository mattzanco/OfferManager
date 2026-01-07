-- Create User table
-- Stores user information for the OfferManager application

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'User' AND schema_id = SCHEMA_ID('OfferManager'))
BEGIN
    CREATE TABLE OfferManager.User (
        UserId INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(100) NOT NULL UNIQUE,
        Email NVARCHAR(100) NOT NULL UNIQUE,
        CreatedDate DATETIME2 DEFAULT GETUTCDATE(),
        UpdatedDate DATETIME2 DEFAULT GETUTCDATE()
    );
    PRINT 'Created table: OfferManager.User'
END
ELSE
BEGIN
    PRINT 'Table OfferManager.User already exists'
END

-- Create Users table
-- Stores user information for the OfferManager application

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users' AND schema_id = SCHEMA_ID('offermanager'))
BEGIN
    CREATE TABLE offermanager.Users (
        UserId INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(100) NOT NULL UNIQUE,
        Email NVARCHAR(100) NOT NULL UNIQUE,
        CreatedDate DATETIME2 DEFAULT GETUTCDATE(),
        UpdatedDate DATETIME2 DEFAULT GETUTCDATE()
    );
    PRINT 'Created table: offermanager.Users'
END
ELSE
BEGIN
    PRINT 'Table offermanager.Users already exists'
END

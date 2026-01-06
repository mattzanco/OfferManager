-- Initial database schema
-- This script creates the base tables for the OfferManager application

-- Users table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        UserId INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(100) NOT NULL UNIQUE,
        Email NVARCHAR(100) NOT NULL UNIQUE,
        CreatedDate DATETIME2 DEFAULT GETUTCDATE(),
        UpdatedDate DATETIME2 DEFAULT GETUTCDATE()
    );
END

-- Offers table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Offers')
BEGIN
    CREATE TABLE Offers (
        OfferId INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        Title NVARCHAR(255) NOT NULL,
        Description NVARCHAR(MAX),
        Status NVARCHAR(50) NOT NULL,
        CreatedDate DATETIME2 DEFAULT GETUTCDATE(),
        UpdatedDate DATETIME2 DEFAULT GETUTCDATE(),
        FOREIGN KEY (UserId) REFERENCES Users(UserId)
    );
END

-- Add index for common queries
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Offers_UserId' AND object_id = OBJECT_ID('Offers'))
BEGIN
    CREATE INDEX IX_Offers_UserId ON Offers(UserId);
END

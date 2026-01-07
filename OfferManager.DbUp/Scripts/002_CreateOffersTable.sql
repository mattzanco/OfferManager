-- Create Offers table
-- Stores offer information linked to users

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Offers' AND schema_id = SCHEMA_ID('OfferManager'))
BEGIN
    CREATE TABLE offermanager.Offers (
        OfferId INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        Title NVARCHAR(255) NOT NULL,
        Description NVARCHAR(MAX),
        Status NVARCHAR(50) NOT NULL,
        CreatedDate DATETIME2 DEFAULT GETUTCDATE(),
        UpdatedDate DATETIME2 DEFAULT GETUTCDATE(),
        FOREIGN KEY (UserId) REFERENCES offermanager.Users(UserId)
    );
    PRINT 'Created table: OfferManager.Offers'
END
ELSE
BEGIN
    PRINT 'Table OfferManager.Offers already exists'
END

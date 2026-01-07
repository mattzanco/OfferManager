-- Create indexes for Offers table
-- Improves query performance for common queries

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Offers_UserId' AND object_id = OBJECT_ID('OfferManager.Offers'))
BEGIN
    CREATE INDEX IX_Offers_UserId ON OfferManager.Offers(UserId);
    PRINT 'Created index: IX_Offers_UserId'
END
ELSE
BEGIN
    PRINT 'Index IX_Offers_UserId already exists'
END

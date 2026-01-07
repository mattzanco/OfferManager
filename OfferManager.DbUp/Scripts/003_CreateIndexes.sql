-- Create indexes for Offer table
-- Improves query performance for common queries

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Offer_UserId' AND object_id = OBJECT_ID('OfferManager.Offer'))
BEGIN
    CREATE INDEX IX_Offer_UserId ON OfferManager.Offer(UserId);
    PRINT 'Created index: IX_Offer_UserId'
END
ELSE
BEGIN
    PRINT 'Index IX_Offer_UserId already exists'
END

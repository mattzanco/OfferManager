IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Offer_CurrentRevision' AND parent_object_id = OBJECT_ID('offermanager.Offer'))
BEGIN
    ALTER TABLE offermanager.Offer
    ADD CONSTRAINT FK_Offer_CurrentRevision
    FOREIGN KEY (CurrentRevisionId) REFERENCES offermanager.OfferRevision(OfferRevisionId);
    PRINT 'Added foreign key: FK_Offer_CurrentRevision'
END
ELSE
BEGIN
    PRINT 'Foreign key FK_Offer_CurrentRevision already exists'
END

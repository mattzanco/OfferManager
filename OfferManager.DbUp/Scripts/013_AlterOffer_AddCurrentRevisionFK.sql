ALTER TABLE offermanager.Offer
ADD CONSTRAINT FK_Offer_CurrentRevision
FOREIGN KEY (CurrentRevisionId) REFERENCES offermanager.OfferRevision(OfferRevisionId);

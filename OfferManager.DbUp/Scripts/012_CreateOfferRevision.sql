
CREATE TABLE offermanager.OfferRevision (
    OfferRevisionId UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_OfferRevision PRIMARY KEY CLUSTERED
        DEFAULT NEWSEQUENTIALID(),
    OrganizationId UNIQUEIDENTIFIER NOT NULL,
    OfferId UNIQUEIDENTIFIER NOT NULL,
    RevisionNumber INT NOT NULL,
    ValidFromAt DATETIME2(3) NULL,
    ValidUntilAt DATETIME2(3) NULL,
    CurrencyCode CHAR(3) NOT NULL CONSTRAINT DF_OfferRevision_Currency DEFAULT 'USD',
    TransitDaysEstimate INT NULL,
    Terms NVARCHAR(MAX) NULL,
    InternalNotes NVARCHAR(MAX) NULL,
    CustomerVisibleNotes NVARCHAR(MAX) NULL,
    TotalSell DECIMAL(19,4) NULL,
    TotalCost DECIMAL(19,4) NULL,
    MarginAmount AS (CASE WHEN TotalSell IS NULL OR TotalCost IS NULL THEN NULL ELSE (TotalSell - TotalCost) END) PERSISTED,
    MarginPct AS (CASE
                    WHEN TotalSell IS NULL OR TotalSell = 0 OR TotalCost IS NULL THEN NULL
                    ELSE ((TotalSell - TotalCost) / NULLIF(TotalSell,0))
                  END) PERSISTED,
    CreatedByUserId UNIQUEIDENTIFIER NOT NULL,
    CreatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_OfferRevision_CreatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_OfferRevision_Organization FOREIGN KEY (OrganizationId)
        REFERENCES offermanager.Organization(OrganizationId),
    CONSTRAINT FK_OfferRevision_Offer FOREIGN KEY (OfferId)
        REFERENCES offermanager.Offer(OfferId),
    CONSTRAINT FK_OfferRevision_CreatedBy FOREIGN KEY (CreatedByUserId)
        REFERENCES offermanager.[User](UserId),
    CONSTRAINT CK_OfferRevision_Revision CHECK (RevisionNumber >= 1),
    CONSTRAINT CK_OfferRevision_Validity CHECK (
        ValidFromAt IS NULL OR ValidUntilAt IS NULL OR ValidUntilAt >= ValidFromAt
    ),
    CONSTRAINT CK_OfferRevision_Transit CHECK (TransitDaysEstimate IS NULL OR TransitDaysEstimate >= 0)
);

CREATE UNIQUE INDEX UX_OfferRevision_Offer_RevisionNumber ON offermanager.OfferRevision (OfferId, RevisionNumber);
CREATE INDEX IX_OfferRevision_Org_Offer ON offermanager.OfferRevision (OrganizationId, OfferId, RevisionNumber DESC);

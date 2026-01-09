
CREATE TABLE offermanager.OfferCharge (
    OfferChargeId UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_OfferCharge PRIMARY KEY CLUSTERED
        DEFAULT NEWSEQUENTIALID(),
    OrganizationId UNIQUEIDENTIFIER NOT NULL,
    OfferRevisionId UNIQUEIDENTIFIER NOT NULL,
    ChargeType NVARCHAR(20) NOT NULL,
    ChargeCode NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200) NULL,
    Quantity DECIMAL(12,4) NOT NULL CONSTRAINT DF_OfferCharge_Qty DEFAULT (1),
    UnitPriceSell DECIMAL(19,4) NOT NULL,
    UnitPriceCost DECIMAL(19,4) NULL,
    IsTaxable BIT NOT NULL CONSTRAINT DF_OfferCharge_IsTaxable DEFAULT (0),
    SortOrder INT NOT NULL CONSTRAINT DF_OfferCharge_SortOrder DEFAULT (0),
    LineTotalSell AS (Quantity * UnitPriceSell) PERSISTED,
    LineTotalCost AS (CASE WHEN UnitPriceCost IS NULL THEN NULL ELSE (Quantity * UnitPriceCost) END) PERSISTED,
    CONSTRAINT FK_OfferCharge_Organization FOREIGN KEY (OrganizationId)
        REFERENCES offermanager.Organization(OrganizationId),
    CONSTRAINT FK_OfferCharge_OfferRevision FOREIGN KEY (OfferRevisionId)
        REFERENCES offermanager.OfferRevision(OfferRevisionId),
    CONSTRAINT CK_OfferCharge_ChargeType CHECK (ChargeType IN (N'Linehaul', N'Fuel', N'Accessorial', N'Other')),
    CONSTRAINT CK_OfferCharge_Quantity CHECK (Quantity > 0),
    CONSTRAINT CK_OfferCharge_Prices CHECK (UnitPriceSell >= 0 AND (UnitPriceCost IS NULL OR UnitPriceCost >= 0))
);

CREATE INDEX IX_OfferCharge_Org_OfferRevision ON offermanager.OfferCharge (OrganizationId, OfferRevisionId, SortOrder);

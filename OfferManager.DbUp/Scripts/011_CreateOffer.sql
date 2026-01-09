
CREATE TABLE offermanager.Offer (
    OfferId UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_Offer PRIMARY KEY CLUSTERED
        DEFAULT NEWSEQUENTIALID(),
    OrganizationId UNIQUEIDENTIFIER NOT NULL,
    RfqId UNIQUEIDENTIFIER NULL,
    CustomerId UNIQUEIDENTIFIER NOT NULL,
    Status NVARCHAR(20) NOT NULL CONSTRAINT DF_Offer_Status DEFAULT N'Draft',
    CurrentRevisionId UNIQUEIDENTIFIER NULL,
    CreatedByUserId UNIQUEIDENTIFIER NOT NULL,
    CreatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_Offer_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_Offer_UpdatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Offer_Organization FOREIGN KEY (OrganizationId)
        REFERENCES offermanager.Organization(OrganizationId),
    CONSTRAINT FK_Offer_Rfq FOREIGN KEY (RfqId)
        REFERENCES offermanager.Rfq(RfqId),
    CONSTRAINT FK_Offer_Customer FOREIGN KEY (CustomerId)
        REFERENCES offermanager.Customer(CustomerId),
    CONSTRAINT FK_Offer_CreatedBy FOREIGN KEY (CreatedByUserId)
        REFERENCES offermanager.[User](UserId),
    CONSTRAINT CK_Offer_Status CHECK (Status IN (
        N'Draft', N'Sent', N'Revised', N'Accepted', N'Declined', N'Expired', N'Booked', N'Archived'
    ))
);

CREATE INDEX IX_Offer_Org_Status_UpdatedAt ON offermanager.Offer (OrganizationId, Status, UpdatedAt DESC);
CREATE INDEX IX_Offer_Org_Customer ON offermanager.Offer (OrganizationId, CustomerId);
CREATE INDEX IX_Offer_Org_Rfq ON offermanager.Offer (OrganizationId, RfqId);

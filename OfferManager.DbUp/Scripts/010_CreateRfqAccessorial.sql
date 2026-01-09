
CREATE TABLE offermanager.RfqAccessorial (
    RfqAccessorialId UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_RfqAccessorial PRIMARY KEY CLUSTERED
        DEFAULT NEWSEQUENTIALID(),
    OrganizationId UNIQUEIDENTIFIER NOT NULL,
    RfqId UNIQUEIDENTIFIER NOT NULL,
    AccessorialCode NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200) NULL,
    Quantity DECIMAL(12,2) NOT NULL CONSTRAINT DF_RfqAccessorial_Qty DEFAULT (1),
    CreatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_RfqAccessorial_CreatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_RfqAccessorial_Organization FOREIGN KEY (OrganizationId)
        REFERENCES offermanager.Organization(OrganizationId),
    CONSTRAINT FK_RfqAccessorial_Rfq FOREIGN KEY (RfqId)
        REFERENCES offermanager.Rfq(RfqId)
);

CREATE INDEX IX_RfqAccessorial_Org_Rfq ON offermanager.RfqAccessorial (OrganizationId, RfqId);

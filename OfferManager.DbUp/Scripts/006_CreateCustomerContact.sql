
CREATE TABLE offermanager.CustomerContact (
    ContactId UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_CustomerContact PRIMARY KEY CLUSTERED
        DEFAULT NEWSEQUENTIALID(),
    OrganizationId UNIQUEIDENTIFIER NOT NULL,
    CustomerId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Email NVARCHAR(320) NULL,
    Phone NVARCHAR(50) NULL,
    Title NVARCHAR(100) NULL,
    IsPrimary BIT NOT NULL CONSTRAINT DF_CustomerContact_IsPrimary DEFAULT (0),
    CreatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_CustomerContact_CreatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_CustomerContact_Organization FOREIGN KEY (OrganizationId)
        REFERENCES offermanager.Organization(OrganizationId),
    CONSTRAINT FK_CustomerContact_Customer FOREIGN KEY (CustomerId)
        REFERENCES offermanager.Customer(CustomerId)
);

CREATE INDEX IX_CustomerContact_Org_Customer ON offermanager.CustomerContact (OrganizationId, CustomerId);

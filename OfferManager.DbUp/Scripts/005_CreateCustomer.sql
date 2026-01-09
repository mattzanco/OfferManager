
CREATE TABLE offermanager.Customer (
    CustomerId UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_Customer PRIMARY KEY CLUSTERED
        DEFAULT NEWSEQUENTIALID(),
    OrganizationId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    AccountCode NVARCHAR(50) NULL,
    BillingTerms NVARCHAR(100) NULL,
    Status NVARCHAR(20) NOT NULL CONSTRAINT DF_Customer_Status DEFAULT N'Active',
    CreatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_Customer_CreatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Customer_Organization FOREIGN KEY (OrganizationId)
        REFERENCES offermanager.Organization(OrganizationId),
    CONSTRAINT CK_Customer_Status CHECK (Status IN (N'Active', N'Inactive'))
);

CREATE INDEX IX_Customer_Org_Name ON offermanager.Customer (OrganizationId, Name);

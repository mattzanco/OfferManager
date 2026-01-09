
CREATE TABLE offermanager.Location (
    LocationId UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_Location PRIMARY KEY CLUSTERED
        DEFAULT NEWSEQUENTIALID(),
    OrganizationId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Address1 NVARCHAR(200) NULL,
    Address2 NVARCHAR(200) NULL,
    City NVARCHAR(100) NULL,
    StateProvince NVARCHAR(100) NULL,
    PostalCode NVARCHAR(20) NULL,
    Country NVARCHAR(2) NULL,
    Latitude DECIMAL(9,6) NULL,
    Longitude DECIMAL(9,6) NULL,
    CreatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_Location_CreatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Location_Organization FOREIGN KEY (OrganizationId)
        REFERENCES offermanager.Organization(OrganizationId)
);

CREATE INDEX IX_Location_Org_Name ON offermanager.Location (OrganizationId, Name);

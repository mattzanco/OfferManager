
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Document' AND schema_id = SCHEMA_ID('offermanager'))
BEGIN
    CREATE TABLE offermanager.Document (
        DocumentId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Document PRIMARY KEY CLUSTERED,
        OrganizationId INT NOT NULL,
        EntityType NVARCHAR(30) NOT NULL,
        EntityId INT NOT NULL,
        FileName NVARCHAR(255) NOT NULL,
        ContentType NVARCHAR(100) NOT NULL,
        StorageProvider NVARCHAR(30) NOT NULL,
        StorageKey NVARCHAR(500) NOT NULL,
        UploadedByUserId INT NOT NULL,
        UploadedAt DATETIME2(3) NOT NULL CONSTRAINT DF_Document_UploadedAt DEFAULT SYSUTCDATETIME(),
        CONSTRAINT FK_Document_Organization FOREIGN KEY (OrganizationId) REFERENCES offermanager.Organization(OrganizationId),
        CONSTRAINT FK_Document_UploadedBy FOREIGN KEY (UploadedByUserId) REFERENCES offermanager.[User](UserId)
    );

    CREATE INDEX IX_Document_Org_Entity ON offermanager.Document (OrganizationId, EntityType, EntityId, UploadedAt DESC);
    PRINT 'Created table: offermanager.Document'
END
ELSE
BEGIN
    PRINT 'Table offermanager.Document already exists'
END

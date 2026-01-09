
CREATE TABLE offermanager.Document (
    DocumentId UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_Document PRIMARY KEY CLUSTERED
        DEFAULT NEWSEQUENTIALID(),
    OrganizationId UNIQUEIDENTIFIER NOT NULL,
    EntityType NVARCHAR(30) NOT NULL,
    EntityId UNIQUEIDENTIFIER NOT NULL,
    FileName NVARCHAR(255) NOT NULL,
    ContentType NVARCHAR(100) NOT NULL,
    StorageProvider NVARCHAR(30) NOT NULL,
    StorageKey NVARCHAR(500) NOT NULL,
    UploadedByUserId UNIQUEIDENTIFIER NOT NULL,
    UploadedAt DATETIME2(3) NOT NULL CONSTRAINT DF_Document_UploadedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Document_Organization FOREIGN KEY (OrganizationId) REFERENCES offermanager.Organization(OrganizationId),
    CONSTRAINT FK_Document_UploadedBy FOREIGN KEY (UploadedByUserId) REFERENCES offermanager.User(UserId)
);

CREATE INDEX IX_Document_Org_Entity ON offermanager.Document (OrganizationId, EntityType, EntityId, UploadedAt DESC);


CREATE TABLE offermanager.ActivityEvent (
    EventId UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_ActivityEvent PRIMARY KEY CLUSTERED
        DEFAULT NEWSEQUENTIALID(),
    OrganizationId UNIQUEIDENTIFIER NOT NULL,
    EntityType NVARCHAR(30) NOT NULL,
    EntityId UNIQUEIDENTIFIER NOT NULL,
    EventType NVARCHAR(50) NOT NULL,
    OldValue NVARCHAR(MAX) NULL,
    NewValue NVARCHAR(MAX) NULL,
    PerformedByUserId UNIQUEIDENTIFIER NOT NULL,
    CreatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_ActivityEvent_CreatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_ActivityEvent_Organization FOREIGN KEY (OrganizationId)
        REFERENCES offermanager.Organization(OrganizationId),
    CONSTRAINT FK_ActivityEvent_PerformedBy FOREIGN KEY (PerformedByUserId)
        REFERENCES offermanager.User(UserId)
);

CREATE INDEX IX_ActivityEvent_Org_Entity ON offermanager.ActivityEvent (OrganizationId, EntityType, EntityId, CreatedAt DESC);

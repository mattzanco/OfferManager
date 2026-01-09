
CREATE TABLE offermanager.LoadMilestone (
    MilestoneId UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_LoadMilestone PRIMARY KEY CLUSTERED
        DEFAULT NEWSEQUENTIALID(),
    OrganizationId UNIQUEIDENTIFIER NOT NULL,
    LoadId UNIQUEIDENTIFIER NOT NULL,
    MilestoneType NVARCHAR(30) NOT NULL,
    OccurredAt DATETIME2(3) NOT NULL,
    Notes NVARCHAR(500) NULL,
    CONSTRAINT FK_LoadMilestone_Organization FOREIGN KEY (OrganizationId) REFERENCES offermanager.Organization(OrganizationId),
    CONSTRAINT FK_LoadMilestone_Load FOREIGN KEY (LoadId) REFERENCES offermanager.Load(LoadId),
    CONSTRAINT CK_LoadMilestone_Type CHECK (MilestoneType IN (
        N'PickupConfirmed', N'Departed', N'Arrived', N'Delivered', N'PODReceived'
    ))
);

CREATE INDEX IX_LoadMilestone_Org_Load ON offermanager.LoadMilestone (OrganizationId, LoadId, OccurredAt DESC);

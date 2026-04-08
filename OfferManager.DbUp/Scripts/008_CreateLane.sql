
CREATE TABLE offermanager.Lane (
    LaneId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Lane PRIMARY KEY CLUSTERED,
    OrganizationId INT NOT NULL,
    OriginLocationId INT NOT NULL,
    DestinationLocationId INT NOT NULL,
    LaneCode NVARCHAR(50) NULL,
    DistanceMiles INT NULL,
    CreatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_Lane_CreatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Lane_Organization FOREIGN KEY (OrganizationId)
        REFERENCES offermanager.Organization(OrganizationId),
    CONSTRAINT FK_Lane_Origin FOREIGN KEY (OriginLocationId)
        REFERENCES offermanager.Location(LocationId),
    CONSTRAINT FK_Lane_Destination FOREIGN KEY (DestinationLocationId)
        REFERENCES offermanager.Location(LocationId),
    CONSTRAINT CK_Lane_Distance CHECK (DistanceMiles IS NULL OR DistanceMiles >= 0)
);

CREATE INDEX IX_Lane_Org_OD ON offermanager.Lane (OrganizationId, OriginLocationId, DestinationLocationId);

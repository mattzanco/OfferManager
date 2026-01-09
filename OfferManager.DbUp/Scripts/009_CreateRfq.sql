CREATE TABLE offermanager.Rfq (
    RfqId UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_Rfq PRIMARY KEY CLUSTERED
        DEFAULT NEWSEQUENTIALID(),
    OrganizationId UNIQUEIDENTIFIER NOT NULL,
    CustomerId UNIQUEIDENTIFIER NOT NULL,
    RequestedByContactId UNIQUEIDENTIFIER NULL,
    OriginLocationId UNIQUEIDENTIFIER NOT NULL,
    DestinationLocationId UNIQUEIDENTIFIER NOT NULL,
    Mode NVARCHAR(20) NOT NULL,
    EquipmentType NVARCHAR(30) NULL,
    ServiceLevel NVARCHAR(30) NULL,
    PickupEarliestAt DATETIME2(3) NULL,
    PickupLatestAt DATETIME2(3) NULL,
    DeliveryEarliestAt DATETIME2(3) NULL,
    DeliveryLatestAt DATETIME2(3) NULL,
    Commodity NVARCHAR(200) NULL,
    WeightLbs DECIMAL(12,2) NULL,
    PalletCount INT NULL,
    PieceCount INT NULL,
    Hazmat BIT NOT NULL CONSTRAINT DF_Rfq_Hazmat DEFAULT (0),
    TemperatureControlled BIT NOT NULL CONSTRAINT DF_Rfq_Temperature DEFAULT (0),
    Notes NVARCHAR(MAX) NULL,
    Status NVARCHAR(20) NOT NULL CONSTRAINT DF_Rfq_Status DEFAULT N'New',
    CreatedByUserId UNIQUEIDENTIFIER NOT NULL,
    CreatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_Rfq_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_Rfq_UpdatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Rfq_Organization FOREIGN KEY (OrganizationId)
        REFERENCES offermanager.Organization(OrganizationId),
    CONSTRAINT FK_Rfq_Customer FOREIGN KEY (CustomerId)
        REFERENCES offermanager.Customer(CustomerId),
    CONSTRAINT FK_Rfq_Contact FOREIGN KEY (RequestedByContactId)
        REFERENCES offermanager.CustomerContact(ContactId),
    CONSTRAINT FK_Rfq_Origin FOREIGN KEY (OriginLocationId)
        REFERENCES offermanager.Location(LocationId),
    CONSTRAINT FK_Rfq_Destination FOREIGN KEY (DestinationLocationId)
        REFERENCES offermanager.Location(LocationId),
    CONSTRAINT FK_Rfq_CreatedBy FOREIGN KEY (CreatedByUserId)
        REFERENCES offermanager.User(UserId),
    CONSTRAINT CK_Rfq_Status CHECK (Status IN (N'New', N'Qualified', N'Quoted', N'Won', N'Lost', N'Expired')),
    CONSTRAINT CK_Rfq_Mode CHECK (Mode IN (N'LTL', N'FTL', N'Intermodal', N'Ocean', N'Air')),
    CONSTRAINT CK_Rfqs_Weights CHECK (WeightLbs IS NULL OR WeightLbs >= 0),
    CONSTRAINT CK_Rfqs_Counts CHECK (
        (PalletCount IS NULL OR PalletCount >= 0) AND
        (PieceCount IS NULL OR PieceCount >= 0)
    )
);

CREATE INDEX IX_Rfqs_Org_Status_CreatedAt ON offermanager.Rfqs (OrganizationId, Status, CreatedAt DESC);
CREATE INDEX IX_Rfqs_Org_Customer ON offermanager.Rfqs (OrganizationId, CustomerId);

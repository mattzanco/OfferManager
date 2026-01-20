
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Load' AND schema_id = SCHEMA_ID('offermanager'))
BEGIN
    CREATE TABLE offermanager.Load (
        LoadId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Load PRIMARY KEY CLUSTERED,
        OrganizationId INT NOT NULL,
        OfferId INT NOT NULL,
        OfferRevisionId INT NOT NULL,
        CustomerId INT NOT NULL,
        OriginLocationId INT NOT NULL,
        DestinationLocationId INT NOT NULL,
        Status NVARCHAR(20) NOT NULL CONSTRAINT DF_Load_Status DEFAULT N'Planned',
        PickupAt DATETIME2(3) NULL,
        DeliveryAt DATETIME2(3) NULL,
        ReferenceNumber NVARCHAR(100) NULL,
        CreatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_Load_CreatedAt DEFAULT SYSUTCDATETIME(),
        UpdatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_Load_UpdatedAt DEFAULT SYSUTCDATETIME(),
        CONSTRAINT FK_Load_Organization FOREIGN KEY (OrganizationId) REFERENCES offermanager.Organization(OrganizationId),
        CONSTRAINT FK_Load_Offer FOREIGN KEY (OfferId) REFERENCES offermanager.Offer(OfferId),
        CONSTRAINT FK_Load_OfferRevision FOREIGN KEY (OfferRevisionId) REFERENCES offermanager.OfferRevision(OfferRevisionId),
        CONSTRAINT FK_Load_Customer FOREIGN KEY (CustomerId) REFERENCES offermanager.Customer(CustomerId),
        CONSTRAINT FK_Load_Origin FOREIGN KEY (OriginLocationId) REFERENCES offermanager.Location(LocationId),
        CONSTRAINT FK_Load_Destination FOREIGN KEY (DestinationLocationId) REFERENCES offermanager.Location(LocationId),
        CONSTRAINT CK_Load_Status CHECK (Status IN (N'Planned', N'Dispatched', N'InTransit', N'Delivered', N'Cancelled'))
    );

    CREATE INDEX IX_Load_Org_Status_UpdatedAt ON offermanager.Load (OrganizationId, Status, UpdatedAt DESC);
    PRINT 'Created table: offermanager.Load'
END
ELSE
BEGIN
    PRINT 'Table offermanager.Load already exists'
END


-- Drop and recreate Offer table with new schema
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Offer' AND schema_id = SCHEMA_ID('offermanager'))
BEGIN
    -- Drop dependent foreign key constraints first
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Offer_CurrentRevision')
    BEGIN
        ALTER TABLE offermanager.Offer DROP CONSTRAINT FK_Offer_CurrentRevision;
    END
    
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Offer_Organization')
    BEGIN
        ALTER TABLE offermanager.Offer DROP CONSTRAINT FK_Offer_Organization;
    END
    
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Offer_Rfq')
    BEGIN
        ALTER TABLE offermanager.Offer DROP CONSTRAINT FK_Offer_Rfq;
    END
    
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Offer_Customer')
    BEGIN
        ALTER TABLE offermanager.Offer DROP CONSTRAINT FK_Offer_Customer;
    END
    
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Offer_CreatedBy')
    BEGIN
        ALTER TABLE offermanager.Offer DROP CONSTRAINT FK_Offer_CreatedBy;
    END
    
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Offer' AND referenced_object_id = OBJECT_ID('offermanager.Offer'))
    BEGIN
        -- Drop any other foreign keys that reference Offer
        DECLARE @SQL NVARCHAR(MAX) = '';
        SELECT @SQL += 'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + '.' + QUOTENAME(OBJECT_NAME(parent_object_id)) + 
                       ' DROP CONSTRAINT ' + QUOTENAME(name) + ';' + CHAR(13)
        FROM sys.foreign_keys
        WHERE referenced_object_id = OBJECT_ID('offermanager.Offer');
        
        IF LEN(@SQL) > 0
        BEGIN
            EXEC sp_executesql @SQL;
        END
    END
    
    DROP TABLE offermanager.Offer;
    PRINT 'Dropped old offermanager.Offer table'
END

CREATE TABLE offermanager.Offer (
    OfferId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Offer PRIMARY KEY CLUSTERED,
    OrganizationId INT NOT NULL,
    RfqId INT NULL,
    CustomerId INT NOT NULL,
    Status NVARCHAR(20) NOT NULL CONSTRAINT DF_Offer_Status DEFAULT N'Draft',
    CurrentRevisionId INT NULL,
    CreatedByUserId INT NOT NULL,
    CreatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_Offer_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_Offer_UpdatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Offer_Organization FOREIGN KEY (OrganizationId)
        REFERENCES offermanager.Organization(OrganizationId),
    CONSTRAINT FK_Offer_Rfq FOREIGN KEY (RfqId)
        REFERENCES offermanager.Rfq(RfqId),
    CONSTRAINT FK_Offer_Customer FOREIGN KEY (CustomerId)
        REFERENCES offermanager.Customer(CustomerId),
    CONSTRAINT FK_Offer_CreatedBy FOREIGN KEY (CreatedByUserId)
        REFERENCES offermanager.[User](UserId),
    CONSTRAINT CK_Offer_Status CHECK (Status IN (
        N'Draft', N'Sent', N'Revised', N'Accepted', N'Declined', N'Expired', N'Booked', N'Archived'
    ))
);

CREATE INDEX IX_Offer_Org_Status_UpdatedAt ON offermanager.Offer (OrganizationId, Status, UpdatedAt DESC);
CREATE INDEX IX_Offer_Org_Customer ON offermanager.Offer (OrganizationId, CustomerId);
CREATE INDEX IX_Offer_Org_Rfq ON offermanager.Offer (OrganizationId, RfqId);
PRINT 'Created new offermanager.Offer table'

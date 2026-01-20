-- Seed test data for development and testing
-- This script populates the database with realistic test data

SET IDENTITY_INSERT offermanager.Organization ON;
INSERT INTO offermanager.Organization (OrganizationId, Name, CreatedAt) VALUES
(1, 'Acme Logistics Inc', SYSUTCDATETIME()),
(2, 'Global Freight Solutions', SYSUTCDATETIME()),
(3, 'Premier Transport Group', SYSUTCDATETIME());
SET IDENTITY_INSERT offermanager.Organization OFF;

SET IDENTITY_INSERT offermanager.[User] ON;
INSERT INTO offermanager.[User] (UserId, OrganizationId, Email, DisplayName, IsActive, CreatedAt) VALUES
(1, 1, 'admin@acmelogistics.com', 'Admin User', 1, SYSUTCDATETIME()),
(2, 1, 'sales@acmelogistics.com', 'Sales Manager', 1, SYSUTCDATETIME()),
(3, 1, 'pricing@acmelogistics.com', 'Pricing Analyst', 1, SYSUTCDATETIME()),
(4, 1, 'ops@acmelogistics.com', 'Operations User', 1, SYSUTCDATETIME()),
(5, 2, 'admin@globalfreight.com', 'Global Admin', 1, SYSUTCDATETIME()),
(6, 2, 'sales@globalfreight.com', 'Global Sales', 1, SYSUTCDATETIME()),
(7, 3, 'admin@premiergroup.com', 'Premier Admin', 1, SYSUTCDATETIME()),
(8, 3, 'ops@premiergroup.com', 'Premier Operations', 1, SYSUTCDATETIME());
SET IDENTITY_INSERT offermanager.[User] OFF;

SET IDENTITY_INSERT offermanager.Role ON;
INSERT INTO offermanager.Role (RoleId, Name) VALUES
(1, 'Admin'),
(2, 'Sales'),
(3, 'Pricing'),
(4, 'Operations'),
(5, 'Viewer');
SET IDENTITY_INSERT offermanager.Role OFF;

-- Assign roles to users
INSERT INTO offermanager.UserRole (UserId, RoleId) VALUES
(1, 1), (2, 2), (3, 3), (4, 4), (5, 1), (6, 2), (7, 1), (8, 4);

SET IDENTITY_INSERT offermanager.Customer ON;
INSERT INTO offermanager.Customer (CustomerId, OrganizationId, Name, Status, CreatedAt) VALUES
(1, 1, 'TechCore Manufacturing', 'Active', SYSUTCDATETIME()),
(2, 1, 'ElectroSupply Corp', 'Active', SYSUTCDATETIME()),
(3, 1, 'BuildRight Construction', 'Active', SYSUTCDATETIME()),
(4, 1, 'FreshFood Distributors', 'Active', SYSUTCDATETIME()),
(5, 2, 'AdvancedAuto Parts', 'Active', SYSUTCDATETIME()),
(6, 2, 'MediCare Pharma', 'Active', SYSUTCDATETIME()),
(7, 3, 'RetailMart Stores', 'Active', SYSUTCDATETIME()),
(8, 3, 'FashionHub Distribution', 'Active', SYSUTCDATETIME());
SET IDENTITY_INSERT offermanager.Customer OFF;

SET IDENTITY_INSERT offermanager.CustomerContact ON;
INSERT INTO offermanager.CustomerContact (ContactId, OrganizationId, CustomerId, Name, Phone, Email, CreatedAt) VALUES
(1, 1, 1, 'John Smith', '555-0101', 'john.smith@techcore.com', SYSUTCDATETIME()),
(2, 1, 1, 'Sarah Johnson', '555-0102', 'sarah.johnson@techcore.com', SYSUTCDATETIME()),
(3, 1, 2, 'Mike Chen', '555-0201', 'mike.chen@electrosupply.com', SYSUTCDATETIME()),
(4, 1, 3, 'Lisa Anderson', '555-0301', 'lisa.anderson@buildright.com', SYSUTCDATETIME()),
(5, 1, 4, 'David Martinez', '555-0401', 'david@freshfood.com', SYSUTCDATETIME()),
(6, 2, 5, 'Rachel Thompson', '555-0501', 'rachel@advancedauto.com', SYSUTCDATETIME()),
(7, 2, 6, 'Robert Kim', '555-0601', 'robert@medicare.com', SYSUTCDATETIME()),
(8, 3, 7, 'Jennifer Lee', '555-0701', 'jen@retailmart.com', SYSUTCDATETIME()),
(9, 3, 8, 'William Garcia', '555-0801', 'william@fashionhub.com', SYSUTCDATETIME());
SET IDENTITY_INSERT offermanager.CustomerContact OFF;

SET IDENTITY_INSERT offermanager.Location ON;
INSERT INTO offermanager.Location (LocationId, OrganizationId, Name, City, StateProvince, PostalCode, Country, CreatedAt) VALUES
(1, 1, 'Los Angeles Distribution Center', 'Los Angeles', 'CA', '90001', 'USA', SYSUTCDATETIME()),
(2, 1, 'Dallas Hub', 'Dallas', 'TX', '75001', 'USA', SYSUTCDATETIME()),
(3, 1, 'Chicago Warehouse', 'Chicago', 'IL', '60601', 'USA', SYSUTCDATETIME()),
(4, 1, 'New York Terminal', 'New York', 'NY', '10001', 'USA', SYSUTCDATETIME()),
(5, 2, 'Miami Port Terminal', 'Miami', 'FL', '33101', 'USA', SYSUTCDATETIME()),
(6, 2, 'Denver Distribution', 'Denver', 'CO', '80001', 'USA', SYSUTCDATETIME()),
(7, 3, 'Seattle Warehouse', 'Seattle', 'WA', '98101', 'USA', SYSUTCDATETIME()),
(8, 3, 'Atlanta Logistics Center', 'Atlanta', 'GA', '30303', 'USA', SYSUTCDATETIME());
SET IDENTITY_INSERT offermanager.Location OFF;

SET IDENTITY_INSERT offermanager.Lane ON;
INSERT INTO offermanager.Lane (LaneId, OrganizationId, OriginLocationId, DestinationLocationId, LaneCode, DistanceMiles, CreatedAt) VALUES
(1, 1, 1, 2, 'LA-DAL', 1435, SYSUTCDATETIME()),
(2, 1, 1, 3, 'LA-CHI', 2008, SYSUTCDATETIME()),
(3, 1, 1, 4, 'LA-NYC', 2800, SYSUTCDATETIME()),
(4, 1, 2, 1, 'DAL-LA', 1435, SYSUTCDATETIME()),
(5, 1, 2, 3, 'DAL-CHI', 926, SYSUTCDATETIME()),
(6, 1, 3, 4, 'CHI-NYC', 790, SYSUTCDATETIME()),
(7, 2, 5, 6, 'MIA-DEN', 2038, SYSUTCDATETIME()),
(8, 3, 7, 8, 'SEA-ATL', 2325, SYSUTCDATETIME());
SET IDENTITY_INSERT offermanager.Lane OFF;

SET IDENTITY_INSERT offermanager.Rfq ON;
INSERT INTO offermanager.Rfq (RfqId, OrganizationId, CustomerId, OriginLocationId, DestinationLocationId, Mode, WeightLbs, Status, CreatedByUserId, CreatedAt, UpdatedAt) VALUES
(1, 1, 1, 1, 2, 'FTL', 25000, 'New', 2, SYSUTCDATETIME(), SYSUTCDATETIME()),
(2, 1, 1, 1, 3, 'FTL', 35000, 'New', 2, SYSUTCDATETIME(), SYSUTCDATETIME()),
(3, 1, 2, 1, 4, 'LTL', 15000, 'New', 2, SYSUTCDATETIME(), SYSUTCDATETIME()),
(4, 1, 3, 2, 1, 'FTL', 42000, 'New', 2, SYSUTCDATETIME(), SYSUTCDATETIME()),
(5, 1, 4, 2, 3, 'FTL', 28000, 'New', 2, SYSUTCDATETIME(), SYSUTCDATETIME()),
(6, 2, 5, 5, 6, 'LTL', 20000, 'New', 6, SYSUTCDATETIME(), SYSUTCDATETIME()),
(7, 2, 6, 5, 6, 'LTL', 10000, 'New', 6, SYSUTCDATETIME(), SYSUTCDATETIME()),
(8, 3, 7, 7, 8, 'FTL', 55000, 'New', 8, SYSUTCDATETIME(), SYSUTCDATETIME()),
(9, 3, 8, 7, 8, 'FTL', 32000, 'New', 8, SYSUTCDATETIME(), SYSUTCDATETIME()),
(10, 1, 1, 3, 4, 'LTL', 18000, 'New', 2, SYSUTCDATETIME(), SYSUTCDATETIME());
SET IDENTITY_INSERT offermanager.Rfq OFF;

SET IDENTITY_INSERT offermanager.RfqAccessorial ON;
INSERT INTO offermanager.RfqAccessorial (RfqAccessorialId, RfqId, OrganizationId, AccessorialCode, Description, Quantity, CreatedAt) VALUES
(1, 1, 1, 'Liftgate', 'Liftgate delivery required', 1, SYSUTCDATETIME()),
(2, 1, 1, 'Residential', 'Residential delivery', 1, SYSUTCDATETIME()),
(3, 2, 1, 'Hazmat', 'Hazardous materials', 1, SYSUTCDATETIME()),
(4, 3, 1, 'Expedited', 'Expedited delivery', 1, SYSUTCDATETIME()),
(5, 4, 1, 'Detention', 'Detention charges may apply', 2, SYSUTCDATETIME()),
(6, 6, 2, 'Liftgate', 'Liftgate service', 1, SYSUTCDATETIME()),
(7, 7, 2, 'Inside', 'Inside delivery', 1, SYSUTCDATETIME()),
(8, 8, 3, 'Hazmat', 'Hazardous materials shipping', 1, SYSUTCDATETIME()),
(9, 9, 3, 'Residential', 'Residential delivery area', 1, SYSUTCDATETIME());
SET IDENTITY_INSERT offermanager.RfqAccessorial OFF;

SET IDENTITY_INSERT offermanager.Offer ON;
INSERT INTO offermanager.Offer (OfferId, OrganizationId, RfqId, CustomerId, Status, CreatedByUserId, CreatedAt) VALUES
(1, 1, 1, 1, 'Draft', 3, SYSUTCDATETIME()),
(2, 1, 2, 1, 'Sent', 3, SYSUTCDATETIME()),
(3, 1, 3, 2, 'Draft', 3, SYSUTCDATETIME()),
(4, 1, 4, 3, 'Sent', 3, SYSUTCDATETIME()),
(5, 1, 5, 4, 'Accepted', 3, DATEADD(day, -5, SYSUTCDATETIME())),
(6, 2, 6, 5, 'Draft', 6, SYSUTCDATETIME()),
(7, 2, 7, 6, 'Sent', 6, SYSUTCDATETIME()),
(8, 3, 8, 7, 'Accepted', 8, DATEADD(day, -3, SYSUTCDATETIME())),
(9, 3, 9, 8, 'Draft', 8, SYSUTCDATETIME()),
(10, 1, 10, 1, 'Sent', 3, SYSUTCDATETIME());
SET IDENTITY_INSERT offermanager.Offer OFF;

SET IDENTITY_INSERT offermanager.OfferRevision ON;
INSERT INTO offermanager.OfferRevision (OfferRevisionId, OfferId, OrganizationId, RevisionNumber, TotalSell, TotalCost, CreatedByUserId, CreatedAt) VALUES
(1, 1, 1, 1, 2650.00, 2500.00, 3, SYSUTCDATETIME()),
(2, 2, 1, 1, 3380.00, 3200.00, 3, SYSUTCDATETIME()),
(3, 2, 1, 2, 3275.00, 3100.00, 3, DATEADD(day, -2, SYSUTCDATETIME())),
(4, 3, 1, 1, 1920.00, 1800.00, 3, SYSUTCDATETIME()),
(5, 4, 1, 1, 4420.00, 4200.00, 3, SYSUTCDATETIME()),
(6, 5, 1, 1, 3600.00, 3400.00, 3, DATEADD(day, -5, SYSUTCDATETIME())),
(7, 6, 2, 1, 2340.00, 2200.00, 6, SYSUTCDATETIME()),
(8, 7, 2, 1, 1700.00, 1600.00, 6, SYSUTCDATETIME()),
(9, 8, 3, 1, 5800.00, 5500.00, 8, DATEADD(day, -3, SYSUTCDATETIME())),
(10, 9, 3, 1, 3280.00, 3100.00, 8, SYSUTCDATETIME()),
(11, 10, 1, 1, 2130.00, 2000.00, 3, SYSUTCDATETIME());
SET IDENTITY_INSERT offermanager.OfferRevision OFF;

-- Update offers with current revisions
UPDATE offermanager.Offer SET CurrentRevisionId = 1 WHERE OfferId = 1;
UPDATE offermanager.Offer SET CurrentRevisionId = 3 WHERE OfferId = 2;
UPDATE offermanager.Offer SET CurrentRevisionId = 4 WHERE OfferId = 3;
UPDATE offermanager.Offer SET CurrentRevisionId = 5 WHERE OfferId = 4;
UPDATE offermanager.Offer SET CurrentRevisionId = 6 WHERE OfferId = 5;
UPDATE offermanager.Offer SET CurrentRevisionId = 7 WHERE OfferId = 6;
UPDATE offermanager.Offer SET CurrentRevisionId = 8 WHERE OfferId = 7;
UPDATE offermanager.Offer SET CurrentRevisionId = 9 WHERE OfferId = 8;
UPDATE offermanager.Offer SET CurrentRevisionId = 10 WHERE OfferId = 9;
UPDATE offermanager.Offer SET CurrentRevisionId = 11 WHERE OfferId = 10;

SET IDENTITY_INSERT offermanager.OfferCharge ON;
INSERT INTO offermanager.OfferCharge (OfferChargeId, OfferRevisionId, OrganizationId, ChargeType, ChargeCode, UnitPriceSell, SortOrder) VALUES
(1, 1, 1, 'Linehaul', 'BASE', 2500.00, 1),
(2, 1, 1, 'Fuel', 'FUEL', 150.00, 2),
(3, 2, 1, 'Linehaul', 'BASE', 3200.00, 1),
(4, 2, 1, 'Fuel', 'FUEL', 180.00, 2),
(5, 3, 1, 'Linehaul', 'BASE', 3100.00, 1),
(6, 3, 1, 'Fuel', 'FUEL', 175.00, 2),
(7, 5, 1, 'Linehaul', 'BASE', 4200.00, 1),
(8, 5, 1, 'Fuel', 'FUEL', 220.00, 2),
(9, 6, 1, 'Linehaul', 'BASE', 3400.00, 1),
(10, 6, 1, 'Fuel', 'FUEL', 200.00, 2),
(11, 7, 2, 'Linehaul', 'BASE', 2200.00, 1),
(12, 7, 2, 'Fuel', 'FUEL', 140.00, 2),
(13, 9, 3, 'Linehaul', 'BASE', 5500.00, 1),
(14, 9, 3, 'Fuel', 'FUEL', 300.00, 2);
SET IDENTITY_INSERT offermanager.OfferCharge OFF;

SET IDENTITY_INSERT offermanager.Load ON;
INSERT INTO offermanager.Load (LoadId, OrganizationId, OfferId, OfferRevisionId, CustomerId, OriginLocationId, DestinationLocationId, Status, PickupAt, DeliveryAt, CreatedAt, UpdatedAt) VALUES
(1, 1, 5, 6, 4, 2, 1, 'InTransit', DATEADD(day, -3, SYSUTCDATETIME()), DATEADD(day, 1, SYSUTCDATETIME()), DATEADD(day, -5, SYSUTCDATETIME()), SYSUTCDATETIME()),
(2, 3, 8, 9, 7, 7, 8, 'Delivered', DATEADD(day, -5, SYSUTCDATETIME()), SYSUTCDATETIME(), DATEADD(day, -8, SYSUTCDATETIME()), SYSUTCDATETIME()),
(3, 1, 2, 3, 1, 1, 3, 'Planned', DATEADD(day, 10, SYSUTCDATETIME()), DATEADD(day, 12, SYSUTCDATETIME()), SYSUTCDATETIME(), SYSUTCDATETIME());
SET IDENTITY_INSERT offermanager.Load OFF;

SET IDENTITY_INSERT offermanager.LoadMilestone ON;
INSERT INTO offermanager.LoadMilestone (MilestoneId, LoadId, OrganizationId, MilestoneType, OccurredAt) VALUES
(1, 1, 1, 'PickupConfirmed', DATEADD(day, -3, SYSUTCDATETIME())),
(2, 1, 1, 'Departed', DATEADD(day, -2, SYSUTCDATETIME())),
(3, 1, 1, 'Arrived', DATEADD(day, 0, SYSUTCDATETIME())),
(4, 2, 3, 'PickupConfirmed', DATEADD(day, -8, SYSUTCDATETIME())),
(5, 2, 3, 'Departed', DATEADD(day, -7, SYSUTCDATETIME())),
(6, 2, 3, 'Delivered', SYSUTCDATETIME()),
(7, 3, 1, 'PickupConfirmed', DATEADD(day, 10, SYSUTCDATETIME())),
(8, 3, 1, 'Delivered', DATEADD(day, 12, SYSUTCDATETIME()));
SET IDENTITY_INSERT offermanager.LoadMilestone OFF;

SET IDENTITY_INSERT offermanager.Document ON;
INSERT INTO offermanager.Document (DocumentId, OrganizationId, EntityType, EntityId, FileName, ContentType, StorageProvider, StorageKey, UploadedByUserId, UploadedAt) VALUES
(1, 1, 'Rfq', 1, 'RFQ_001_TechCore.pdf', 'application/pdf', 'AzureBlob', 'documents/rfq/rfq_001.pdf', 2, SYSUTCDATETIME()),
(2, 1, 'Load', 1, 'Manifest_Load_001.pdf', 'application/pdf', 'AzureBlob', 'documents/manifests/manifest_001.pdf', 4, DATEADD(day, -5, SYSUTCDATETIME())),
(3, 1, 'Offer', 2, 'Quote_Offer_002_Revised.pdf', 'application/pdf', 'AzureBlob', 'documents/quotes/quote_002_rev.pdf', 3, DATEADD(day, -2, SYSUTCDATETIME())),
(4, 2, 'Rfq', 6, 'RFQ_AdvancedAuto_001.pdf', 'application/pdf', 'AzureBlob', 'documents/rfq/auto_001.pdf', 6, SYSUTCDATETIME()),
(5, 3, 'Load', 2, 'Manifest_Load_002_Delivered.pdf', 'application/pdf', 'AzureBlob', 'documents/manifests/manifest_002.pdf', 8, DATEADD(day, -3, SYSUTCDATETIME()));
SET IDENTITY_INSERT offermanager.Document OFF;

SET IDENTITY_INSERT offermanager.ActivityEvent ON;
INSERT INTO offermanager.ActivityEvent (EventId, OrganizationId, EntityType, EntityId, EventType, PerformedByUserId, CreatedAt) VALUES
(1, 1, 'Rfq', 1, 'RFQ_Created', 2, SYSUTCDATETIME()),
(2, 1, 'Offer', 1, 'Offer_Created', 3, SYSUTCDATETIME()),
(3, 1, 'Offer', 2, 'Offer_Revised', 3, DATEADD(day, -2, SYSUTCDATETIME())),
(4, 1, 'Offer', 5, 'Offer_Accepted', 2, DATEADD(day, -5, SYSUTCDATETIME())),
(5, 1, 'Load', 1, 'Load_Created', 4, DATEADD(day, -5, SYSUTCDATETIME())),
(6, 1, 'Load', 1, 'Milestone_Completed', 4, DATEADD(day, -3, SYSUTCDATETIME())),
(7, 2, 'Rfq', 6, 'RFQ_Created', 6, SYSUTCDATETIME()),
(8, 2, 'Offer', 7, 'Offer_Created', 6, SYSUTCDATETIME()),
(9, 3, 'Load', 2, 'Load_Created', 8, DATEADD(day, -3, SYSUTCDATETIME())),
(10, 3, 'Load', 2, 'Milestone_Completed', 8, SYSUTCDATETIME());
SET IDENTITY_INSERT offermanager.ActivityEvent OFF;

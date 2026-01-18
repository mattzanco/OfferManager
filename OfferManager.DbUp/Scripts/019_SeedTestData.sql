-- Seed test data for development and testing
-- This script populates the database with realistic test data

SET IDENTITY_INSERT offermanager.Organization ON;
INSERT INTO offermanager.Organization (OrganizationId, Name, CreatedAt) VALUES
(1, 'Acme Logistics Inc', GETUTCDATE()),
(2, 'Global Freight Solutions', GETUTCDATE()),
(3, 'Premier Transport Group', GETUTCDATE());
SET IDENTITY_INSERT offermanager.Organization OFF;

SET IDENTITY_INSERT offermanager.[User] ON;
INSERT INTO offermanager.[User] (UserId, Username, Email, OrganizationId, CreatedAt) VALUES
(1, 'admin_user', 'admin@acmelogistics.com', 1, GETUTCDATE()),
(2, 'sales_manager', 'sales@acmelogistics.com', 1, GETUTCDATE()),
(3, 'pricing_analyst', 'pricing@acmelogistics.com', 1, GETUTCDATE()),
(4, 'operations_user', 'ops@acmelogistics.com', 1, GETUTCDATE()),
(5, 'admin_global', 'admin@globalfreight.com', 2, GETUTCDATE()),
(6, 'sales_global', 'sales@globalfreight.com', 2, GETUTCDATE()),
(7, 'admin_premier', 'admin@premiergroup.com', 3, GETUTCDATE()),
(8, 'ops_premier', 'ops@premiergroup.com', 3, GETUTCDATE());
SET IDENTITY_INSERT offermanager.[User] OFF;

SET IDENTITY_INSERT offermanager.Role ON;
INSERT INTO offermanager.Role (RoleId, RoleName, Description) VALUES
(1, 'Admin', 'Full system access'),
(2, 'Sales', 'Sales and quote management'),
(3, 'Pricing', 'Pricing and cost analysis'),
(4, 'Operations', 'Operational tasks'),
(5, 'Viewer', 'Read-only access');
SET IDENTITY_INSERT offermanager.Role OFF;

-- Assign roles to users
INSERT INTO offermanager.UserRole (UserId, RoleId) VALUES
(1, 1), -- admin_user is Admin
(2, 2), -- sales_manager is Sales
(3, 3), -- pricing_analyst is Pricing
(4, 4), -- operations_user is Operations
(5, 1), -- admin_global is Admin
(6, 2), -- sales_global is Sales
(7, 1), -- admin_premier is Admin
(8, 4); -- ops_premier is Operations

SET IDENTITY_INSERT offermanager.Customer ON;
INSERT INTO offermanager.Customer (CustomerId, OrganizationId, CustomerName, CustomerCode, CreatedAt) VALUES
(1, 1, 'TechCore Manufacturing', 'TECH001', GETUTCDATE()),
(2, 1, 'ElectroSupply Corp', 'ELEC002', GETUTCDATE()),
(3, 1, 'BuildRight Construction', 'BUILD003', GETUTCDATE()),
(4, 1, 'FreshFood Distributors', 'FRESH004', GETUTCDATE()),
(5, 2, 'AdvancedAuto Parts', 'AUTO005', GETUTCDATE()),
(6, 2, 'MediCare Pharma', 'MED006', GETUTCDATE()),
(7, 3, 'RetailMart Stores', 'RETAIL007', GETUTCDATE()),
(8, 3, 'FashionHub Distribution', 'FASH008', GETUTCDATE());
SET IDENTITY_INSERT offermanager.Customer OFF;

SET IDENTITY_INSERT offermanager.CustomerContact ON;
INSERT INTO offermanager.CustomerContact (ContactId, OrganizationId, CustomerId, ContactName, Phone, Email, CreatedAt) VALUES
(1, 1, 1, 'John Smith', '555-0101', 'john.smith@techcore.com', GETUTCDATE()),
(2, 1, 1, 'Sarah Johnson', '555-0102', 'sarah.johnson@techcore.com', GETUTCDATE()),
(3, 1, 2, 'Mike Chen', '555-0201', 'mike.chen@electrosupply.com', GETUTCDATE()),
(4, 1, 3, 'Lisa Anderson', '555-0301', 'lisa.anderson@buildright.com', GETUTCDATE()),
(5, 1, 4, 'David Martinez', '555-0401', 'david@freshfood.com', GETUTCDATE()),
(6, 2, 5, 'Rachel Thompson', '555-0501', 'rachel@advancedauto.com', GETUTCDATE()),
(7, 2, 6, 'Robert Kim', '555-0601', 'robert@medicare.com', GETUTCDATE()),
(8, 3, 7, 'Jennifer Lee', '555-0701', 'jen@retailmart.com', GETUTCDATE()),
(9, 3, 8, 'William Garcia', '555-0801', 'william@fashionhub.com', GETUTCDATE());
SET IDENTITY_INSERT offermanager.CustomerContact OFF;

SET IDENTITY_INSERT offermanager.Location ON;
INSERT INTO offermanager.Location (LocationId, OrganizationId, LocationName, City, State, Country, PostalCode, CreatedAt) VALUES
(1, 1, 'Los Angeles Distribution Center', 'Los Angeles', 'CA', 'USA', '90001', GETUTCDATE()),
(2, 1, 'Dallas Hub', 'Dallas', 'TX', 'USA', '75001', GETUTCDATE()),
(3, 1, 'Chicago Warehouse', 'Chicago', 'IL', 'USA', '60601', GETUTCDATE()),
(4, 1, 'New York Terminal', 'New York', 'NY', 'USA', '10001', GETUTCDATE()),
(5, 2, 'Miami Port Terminal', 'Miami', 'FL', 'USA', '33101', GETUTCDATE()),
(6, 2, 'Denver Distribution', 'Denver', 'CO', 'USA', '80001', GETUTCDATE()),
(7, 3, 'Seattle Warehouse', 'Seattle', 'WA', 'USA', '98101', GETUTCDATE()),
(8, 3, 'Atlanta Logistics Center', 'Atlanta', 'GA', 'USA', '30303', GETUTCDATE());
SET IDENTITY_INSERT offermanager.Location OFF;

SET IDENTITY_INSERT offermanager.Lane ON;
INSERT INTO offermanager.Lane (LaneId, OrganizationId, OriginLocationId, DestinationLocationId, LaneName, Distance, EstimatedDays, CreatedAt) VALUES
(1, 1, 1, 2, 'LA to Dallas', 1435, 2, GETUTCDATE()),
(2, 1, 1, 3, 'LA to Chicago', 2008, 3, GETUTCDATE()),
(3, 1, 1, 4, 'LA to New York', 2800, 4, GETUTCDATE()),
(4, 1, 2, 1, 'Dallas to LA', 1435, 2, GETUTCDATE()),
(5, 1, 2, 3, 'Dallas to Chicago', 926, 1, GETUTCDATE()),
(6, 1, 3, 4, 'Chicago to New York', 790, 1, GETUTCDATE()),
(7, 2, 5, 6, 'Miami to Denver', 2038, 3, GETUTCDATE()),
(8, 3, 7, 8, 'Seattle to Atlanta', 2325, 3, GETUTCDATE());
SET IDENTITY_INSERT offermanager.Lane OFF;

SET IDENTITY_INSERT offermanager.Rfq ON;
INSERT INTO offermanager.Rfq (RfqId, OrganizationId, CustomerId, ShipmentDate, PickupLocationId, DeliveryLocationId, Weight, CreatedByUserId, CreatedAt) VALUES
(1, 1, 1, DATEADD(day, 7, GETUTCDATE()), 1, 2, 25000, 2, GETUTCDATE()),
(2, 1, 1, DATEADD(day, 14, GETUTCDATE()), 1, 3, 35000, 2, GETUTCDATE()),
(3, 1, 2, DATEADD(day, 10, GETUTCDATE()), 1, 4, 15000, 2, GETUTCDATE()),
(4, 1, 3, DATEADD(day, 5, GETUTCDATE()), 2, 1, 42000, 2, GETUTCDATE()),
(5, 1, 4, DATEADD(day, 21, GETUTCDATE()), 2, 3, 28000, 2, GETUTCDATE()),
(6, 2, 5, DATEADD(day, 8, GETUTCDATE()), 5, 6, 20000, 6, GETUTCDATE()),
(7, 2, 6, DATEADD(day, 12, GETUTCDATE()), 5, 6, 10000, 6, GETUTCDATE()),
(8, 3, 7, DATEADD(day, 6, GETUTCDATE()), 7, 8, 55000, 8, GETUTCDATE()),
(9, 3, 8, DATEADD(day, 15, GETUTCDATE()), 7, 8, 32000, 8, GETUTCDATE()),
(10, 1, 1, DATEADD(day, 30, GETUTCDATE()), 3, 4, 18000, 2, GETUTCDATE());
SET IDENTITY_INSERT offermanager.Rfq OFF;

SET IDENTITY_INSERT offermanager.RfqAccessorial ON;
INSERT INTO offermanager.RfqAccessorial (RfqAccessorialId, RfqId, OrganizationId, AccessorialType, Description, CreatedAt) VALUES
(1, 1, 1, 'Liftgate', 'Liftgate delivery required', GETUTCDATE()),
(2, 1, 1, 'Residential', 'Residential delivery', GETUTCDATE()),
(3, 2, 1, 'HazMat', 'Hazardous materials', GETUTCDATE()),
(4, 3, 1, 'Expedited', 'Expedited delivery', GETUTCDATE()),
(5, 4, 1, 'Detention', 'Detention charges may apply', GETUTCDATE()),
(6, 6, 2, 'Liftgate', 'Liftgate service', GETUTCDATE()),
(7, 7, 2, 'Inside', 'Inside delivery', GETUTCDATE()),
(8, 8, 3, 'HazMat', 'Hazardous materials shipping', GETUTCDATE()),
(9, 9, 3, 'Residential', 'Residential delivery area', GETUTCDATE());
SET IDENTITY_INSERT offermanager.RfqAccessorial OFF;

SET IDENTITY_INSERT offermanager.Offer ON;
INSERT INTO offermanager.Offer (OfferId, OrganizationId, RfqId, CustomerId, Status, CreatedByUserId, CreatedAt) VALUES
(1, 1, 1, 1, 'Draft', 3, GETUTCDATE()),
(2, 1, 2, 1, 'Quoted', 3, GETUTCDATE()),
(3, 1, 3, 2, 'Draft', 3, GETUTCDATE()),
(4, 1, 4, 3, 'Quoted', 3, GETUTCDATE()),
(5, 1, 5, 4, 'Accepted', 3, DATEADD(day, -5, GETUTCDATE())),
(6, 2, 6, 5, 'Draft', 6, GETUTCDATE()),
(7, 2, 7, 6, 'Quoted', 6, GETUTCDATE()),
(8, 3, 8, 7, 'Accepted', 8, DATEADD(day, -3, GETUTCDATE())),
(9, 3, 9, 8, 'Draft', 8, GETUTCDATE()),
(10, 1, 10, 1, 'Quoted', 3, GETUTCDATE());
SET IDENTITY_INSERT offermanager.Offer OFF;

SET IDENTITY_INSERT offermanager.OfferRevision ON;
INSERT INTO offermanager.OfferRevision (OfferRevisionId, OfferId, OrganizationId, RevisionNumber, BaseRate, FuelSurcharge, TotalCost, Notes, CreatedByUserId, CreatedAt) VALUES
(1, 1, 1, 1, 2500.00, 150.00, 2650.00, 'Initial quote', 3, GETUTCDATE()),
(2, 2, 1, 1, 3200.00, 180.00, 3380.00, 'Standard rate', 3, GETUTCDATE()),
(3, 2, 1, 2, 3100.00, 175.00, 3275.00, 'Revised rate', 3, DATEADD(day, -2, GETUTCDATE())),
(4, 3, 1, 1, 1800.00, 120.00, 1920.00, 'Competitive quote', 3, GETUTCDATE()),
(5, 4, 1, 1, 4200.00, 220.00, 4420.00, 'Full service rate', 3, GETUTCDATE()),
(6, 5, 1, 1, 3400.00, 200.00, 3600.00, 'Accepted rate', 3, DATEADD(day, -5, GETUTCDATE())),
(7, 6, 2, 1, 2200.00, 140.00, 2340.00, 'Initial offer', 6, GETUTCDATE()),
(8, 7, 2, 1, 1600.00, 100.00, 1700.00, 'Competitive bid', 6, GETUTCDATE()),
(9, 8, 3, 1, 5500.00, 300.00, 5800.00, 'Accepted - Large shipment', 8, DATEADD(day, -3, GETUTCDATE())),
(10, 9, 3, 1, 3100.00, 180.00, 3280.00, 'Draft quote', 8, GETUTCDATE()),
(11, 10, 1, 1, 2000.00, 130.00, 2130.00, 'Standard rate', 3, GETUTCDATE());
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
INSERT INTO offermanager.OfferCharge (OfferChargeId, OfferRevisionId, OrganizationId, ChargeType, Description, Amount, CreatedAt) VALUES
(1, 1, 1, 'Base Rate', 'Base transportation rate', 2500.00, GETUTCDATE()),
(2, 1, 1, 'Fuel Surcharge', 'Fuel surcharge', 150.00, GETUTCDATE()),
(3, 2, 1, 'Base Rate', 'Base transportation rate', 3200.00, GETUTCDATE()),
(4, 2, 1, 'Fuel Surcharge', 'Fuel surcharge', 180.00, GETUTCDATE()),
(5, 3, 1, 'Base Rate', 'Base transportation rate', 3100.00, GETUTCDATE()),
(6, 3, 1, 'Fuel Surcharge', 'Fuel surcharge', 175.00, GETUTCDATE()),
(7, 5, 1, 'Base Rate', 'Base transportation rate', 4200.00, GETUTCDATE()),
(8, 5, 1, 'Fuel Surcharge', 'Fuel surcharge', 220.00, GETUTCDATE()),
(9, 6, 1, 'Base Rate', 'Base transportation rate', 3400.00, GETUTCDATE()),
(10, 6, 1, 'Fuel Surcharge', 'Fuel surcharge', 200.00, GETUTCDATE()),
(11, 7, 2, 'Base Rate', 'Base transportation rate', 2200.00, GETUTCDATE()),
(12, 7, 2, 'Fuel Surcharge', 'Fuel surcharge', 140.00, GETUTCDATE()),
(13, 9, 3, 'Base Rate', 'Base transportation rate', 5500.00, GETUTCDATE()),
(14, 9, 3, 'Fuel Surcharge', 'Fuel surcharge', 300.00, GETUTCDATE());
SET IDENTITY_INSERT offermanager.OfferCharge OFF;

SET IDENTITY_INSERT offermanager.Load ON;
INSERT INTO offermanager.Load (LoadId, OrganizationId, OfferId, OfferRevisionId, CustomerId, LoadStatus, PickupDate, DeliveryDate, OriginLocationId, DestinationLocationId, Weight, TrackingNumber, CreatedAt) VALUES
(1, 1, 5, 6, 4, 'In Transit', DATEADD(day, -3, GETUTCDATE()), DATEADD(day, 1, GETUTCDATE()), 2, 1, 42000, 'TRK001234567', DATEADD(day, -5, GETUTCDATE())),
(2, 3, 8, 9, 7, 'Delivered', DATEADD(day, -5, GETUTCDATE()), GETUTCDATE(), 7, 8, 55000, 'TRK002345678', DATEADD(day, -8, GETUTCDATE())),
(3, 1, 2, 3, 1, 'Pending', DATEADD(day, 10, GETUTCDATE()), DATEADD(day, 12, GETUTCDATE()), 1, 3, 35000, 'TRK003456789', GETUTCDATE());
SET IDENTITY_INSERT offermanager.Load OFF;

SET IDENTITY_INSERT offermanager.LoadMilestone ON;
INSERT INTO offermanager.LoadMilestone (MilestoneId, LoadId, OrganizationId, MilestoneType, Location, ScheduledDate, ActualDate, Status, Notes, CreatedAt) VALUES
(1, 1, 1, 'Pickup', 'Dallas Hub', DATEADD(day, -3, GETUTCDATE()), DATEADD(day, -3, GETUTCDATE()), 'Completed', 'Pickup completed on schedule', GETUTCDATE()),
(2, 1, 1, 'In Transit', 'en route', DATEADD(day, -1, GETUTCDATE()), GETUTCDATE(), 'In Progress', 'Currently in transit to LA', GETUTCDATE()),
(3, 1, 1, 'Delivery', 'Los Angeles DC', DATEADD(day, 1, GETUTCDATE()), NULL, 'Scheduled', 'Expected delivery Jan 19', GETUTCDATE()),
(4, 2, 3, 'Pickup', 'Seattle Warehouse', DATEADD(day, -8, GETUTCDATE()), DATEADD(day, -8, GETUTCDATE()), 'Completed', 'Picked up on schedule', GETUTCDATE()),
(5, 2, 3, 'In Transit', 'en route', DATEADD(day, -6, GETUTCDATE()), DATEADD(day, -1, GETUTCDATE()), 'Completed', 'Completed in-transit portion', GETUTCDATE()),
(6, 2, 3, 'Delivery', 'Atlanta Logistics', DATEADD(day, -2, GETUTCDATE()), GETUTCDATE(), 'Completed', 'Delivered successfully', GETUTCDATE()),
(7, 3, 1, 'Pickup', 'Los Angeles DC', DATEADD(day, 10, GETUTCDATE()), NULL, 'Scheduled', 'Scheduled pickup', GETUTCDATE()),
(8, 3, 1, 'Delivery', 'Chicago Warehouse', DATEADD(day, 12, GETUTCDATE()), NULL, 'Scheduled', 'Scheduled delivery', GETUTCDATE());
SET IDENTITY_INSERT offermanager.LoadMilestone OFF;

SET IDENTITY_INSERT offermanager.Document ON;
INSERT INTO offermanager.Document (DocumentId, OrganizationId, EntityId, DocumentType, FileName, FilePath, FileSize, UploadedByUserId, CreatedAt) VALUES
(1, 1, 1, 'RFQ', 'RFQ_001_TechCore.pdf', '/documents/rfq/rfq_001.pdf', 245632, 2, GETUTCDATE()),
(2, 1, 5, 'LoadManifest', 'Manifest_Load_001.pdf', '/documents/manifests/manifest_001.pdf', 156748, 4, DATEADD(day, -5, GETUTCDATE())),
(3, 1, 2, 'Quote', 'Quote_Offer_002_Revised.pdf', '/documents/quotes/quote_002_rev.pdf', 320145, 3, DATEADD(day, -2, GETUTCDATE())),
(4, 2, 6, 'RFQ', 'RFQ_AdvancedAuto_001.pdf', '/documents/rfq/auto_001.pdf', 198765, 6, GETUTCDATE()),
(5, 3, 8, 'LoadManifest', 'Manifest_Load_002_Delivered.pdf', '/documents/manifests/manifest_002.pdf', 267890, 8, DATEADD(day, -3, GETUTCDATE()));
SET IDENTITY_INSERT offermanager.Document OFF;

SET IDENTITY_INSERT offermanager.ActivityEvent ON;
INSERT INTO offermanager.ActivityEvent (EventId, OrganizationId, EntityId, EventType, Description, PerformedByUserId, CreatedAt) VALUES
(1, 1, 1, 'RFQ_Created', 'RFQ created by sales manager', 2, GETUTCDATE()),
(2, 1, 1, 'Offer_Created', 'Initial offer created', 3, GETUTCDATE()),
(3, 1, 2, 'Offer_Revised', 'Offer revised with better rate', 3, DATEADD(day, -2, GETUTCDATE())),
(4, 1, 5, 'Offer_Accepted', 'Offer accepted by customer', 2, DATEADD(day, -5, GETUTCDATE())),
(5, 1, 1, 'Load_Created', 'Load created from accepted offer', 4, DATEADD(day, -5, GETUTCDATE())),
(6, 1, 1, 'Milestone_Completed', 'Pickup milestone completed', 4, DATEADD(day, -3, GETUTCDATE())),
(7, 2, 6, 'RFQ_Created', 'RFQ received from customer', 6, GETUTCDATE()),
(8, 2, 7, 'Offer_Created', 'Initial offer for AdvancedAuto', 6, GETUTCDATE()),
(9, 3, 8, 'Load_Created', 'Large shipment load created', 8, DATEADD(day, -3, GETUTCDATE())),
(10, 3, 2, 'Milestone_Completed', 'Delivery completed in Atlanta', 8, GETUTCDATE());
SET IDENTITY_INSERT offermanager.ActivityEvent OFF;

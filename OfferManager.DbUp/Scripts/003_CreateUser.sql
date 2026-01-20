-- Drop and recreate User table with new schema
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'User' AND schema_id = SCHEMA_ID('offermanager'))
BEGIN
    -- Drop dependent foreign key constraints first
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_UserRole_User')
    BEGIN
        ALTER TABLE offermanager.UserRole DROP CONSTRAINT FK_UserRole_User;
    END
    
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_User_Organization')
    BEGIN
        ALTER TABLE offermanager.[User] DROP CONSTRAINT FK_User_Organization;
    END
    
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE referenced_object_id = OBJECT_ID('offermanager.[User]'))
    BEGIN
        -- Drop any other foreign keys that reference User
        DECLARE @SQL NVARCHAR(MAX) = '';
        SELECT @SQL += 'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + '.' + QUOTENAME(OBJECT_NAME(parent_object_id)) + 
                       ' DROP CONSTRAINT ' + QUOTENAME(name) + ';' + CHAR(13)
        FROM sys.foreign_keys
        WHERE referenced_object_id = OBJECT_ID('offermanager.[User]');
        
        IF LEN(@SQL) > 0
        BEGIN
            EXEC sp_executesql @SQL;
        END
    END
    
    DROP TABLE offermanager.[User];
    PRINT 'Dropped old offermanager.[User] table'
END

CREATE TABLE offermanager.[User] (
    UserId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_User PRIMARY KEY CLUSTERED,
    OrganizationId INT NOT NULL,
    Email NVARCHAR(320) NOT NULL,
    DisplayName NVARCHAR(200) NOT NULL,
    PasswordHash NVARCHAR(500) NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_User_IsActive DEFAULT (1),
    CreatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_User_CreatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_User_Organization FOREIGN KEY (OrganizationId)
        REFERENCES offermanager.Organization(OrganizationId)
);

CREATE UNIQUE INDEX UX_User_Org_Email ON offermanager.[User] (OrganizationId, Email);
PRINT 'Created new offermanager.[User] table'

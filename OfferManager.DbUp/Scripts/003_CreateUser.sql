
CREATE TABLE offermanager.[User] (
    UserId UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_User PRIMARY KEY CLUSTERED
        DEFAULT NEWSEQUENTIALID(),
    OrganizationId UNIQUEIDENTIFIER NOT NULL,
    Email NVARCHAR(320) NOT NULL,
    DisplayName NVARCHAR(200) NOT NULL,
    PasswordHash NVARCHAR(500) NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_User_IsActive DEFAULT (1),
    CreatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_User_CreatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_User_Organization FOREIGN KEY (OrganizationId)
        REFERENCES offermanager.Organization(OrganizationId)
);

CREATE UNIQUE INDEX UX_User_Org_Email ON offermanager.[User] (OrganizationId, Email);

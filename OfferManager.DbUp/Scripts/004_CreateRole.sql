
CREATE TABLE offermanager.Role (
    RoleId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Role PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);
CREATE UNIQUE INDEX UX_Role_Name ON offermanager.Role(Name);

CREATE TABLE offermanager.UserRole (
    UserId UNIQUEIDENTIFIER NOT NULL,
    RoleId INT NOT NULL,
    CONSTRAINT PK_UserRole PRIMARY KEY (UserId, RoleId),
    CONSTRAINT FK_UserRole_User FOREIGN KEY (UserId) REFERENCES offermanager.[User](UserId),
    CONSTRAINT FK_UserRole_Role FOREIGN KEY (RoleId) REFERENCES offermanager.Role(RoleId)
);

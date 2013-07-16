/* ------------------------------------------------------------------ */
/* Users */
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Users'))
BEGIN

/* Altairis Users table */
CREATE TABLE dbo.Users (
    UserName                nvarchar(100)  NOT NULL,
    PasswordHash            binary(64)     NOT NULL,
    PasswordSalt            binary(128)    NOT NULL,
    Email                   nvarchar(max)  NOT NULL,
    Comment                 nvarchar(max)  NULL,
    IsApproved              bit            NOT NULL,
    DateCreated             datetime       NOT NULL,
    DateLastLogin           datetime       NULL,
    DateLastActivity        datetime       NULL,
    DateLastPasswordChange  datetime       NOT NULL,
    CONSTRAINT PK_Users PRIMARY KEY CLUSTERED (UserName)
)
/* end Altairis Users table */

END
/* ------------------------------------------------------------------ */


/* ------------------------------------------------------------------ */
/* Roles */
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Roles'))
BEGIN

/* Altairis Roles table */
CREATE TABLE dbo.Roles (
    RoleName                nvarchar(100)  NOT NULL,
    CONSTRAINT PK_Roles PRIMARY KEY CLUSTERED (RoleName)
)
/* end Altairis Roles table */

END
/* ------------------------------------------------------------------ */


/* ------------------------------------------------------------------ */
/* RoleMemberships */
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RoleMemberships'))
BEGIN

/* Altairis RoleMemberships table */
CREATE TABLE dbo.RoleMemberships (
    UserName                nvarchar(100)  NOT NULL,
    RoleName                nvarchar(100)  NOT NULL,
    CONSTRAINT PK_RoleMemberships PRIMARY KEY CLUSTERED (UserName, RoleName),
    CONSTRAINT FK_RoleMemberships_Roles 
        FOREIGN KEY (RoleName) REFERENCES dbo.Roles (RoleName) 
        ON UPDATE CASCADE ON DELETE CASCADE,
)
/* end Altairis RoleMemberships table */

END
/* ------------------------------------------------------------------ */


/* ------------------------------------------------------------------ */
/* FK_RoleMemberships_Users */
IF (
EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Users')
AND
EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RoleMemberships')
)
BEGIN

IF NOT EXISTS (SELECT * FROM sys.foreign_keys 
               WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleMemberships_Users]') 
               AND parent_object_id = OBJECT_ID(N'[dbo].[RoleMemberships]'))
BEGIN

/* Altairis FK_RoleMemberships_Users foreign key */
ALTER TABLE dbo.RoleMemberships 
    ADD CONSTRAINT FK_RoleMemberships_Users 
    FOREIGN KEY (UserName) REFERENCES dbo.Users (UserName) 
    ON UPDATE CASCADE ON DELETE CASCADE
/* end Altairis FK_RoleMemberships_Users foreign key */
    
END
END
/* ------------------------------------------------------------------ */
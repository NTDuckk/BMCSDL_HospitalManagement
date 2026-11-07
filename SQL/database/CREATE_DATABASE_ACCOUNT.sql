create database QLBenhVien_ACCOUNT
use QLBenhVien_ACCOUNT
go
-- Tài khoản
CREATE TABLE ACCOUNT (
    username VARCHAR(10) PRIMARY KEY,
    passwordHash VARBINARY(MAX),
	maNhanVien VARCHAR(10),
	typeID VARCHAR(2),
    createdDate DATETIME DEFAULT GETDATE(),
	hasKey VARCHAR(1) DEFAULT '0',
);

--Các quyền
CREATE TABLE PERMISSION (
    permissionID INT PRIMARY KEY IDENTITY(1,1),
    permissionName NVARCHAR(100) UNIQUE
);

CREATE TABLE ACCOUNT_PERMISSION (
    username VARCHAR(10),
    permissionID INT,
    PRIMARY KEY (username, PermissionID),
    FOREIGN KEY (username) REFERENCES Account(username),
    FOREIGN KEY (PermissionID) REFERENCES Permission(PermissionID)
);
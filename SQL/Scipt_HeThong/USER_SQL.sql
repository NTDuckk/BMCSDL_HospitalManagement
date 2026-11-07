CREATE LOGIN userBenhVien WITH PASSWORD = 'userBenhVien';
USE QLBenhVien;
GO
CREATE USER userBenhVien FOR LOGIN userBenhVien;

CREATE LOGIN userAccountBenhVien WITH PASSWORD = 'userAccountBenhVien';
USE QLBenhVien_ACCOUNT;
GO
CREATE USER userAccountBenhVien FOR LOGIN userAccountBenhVien;

USE QLBenhVien;
GO
CREATE USER userAccountBenhVien FOR LOGIN userAccountBenhVien;
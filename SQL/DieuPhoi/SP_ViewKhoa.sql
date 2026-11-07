Use QLBenhVien
go

CREATE VIEW viewKhoa AS
SELECT 
   maKhoa,
   tenKhoa
FROM KHOA
GO
	GRANT SELECT ON OBJECT::viewKhoa TO userBenhVien;
GO
--drop view viewKhoa

CREATE PROCEDURE sp_viewKhoa
as
begin
	select * from viewKhoa
end
GO
GRANT EXECUTE
    ON OBJECT::[dbo].sp_viewKhoa TO userBenhVien
    AS [dbo];
GO
--drop proc sp_viewKhoa

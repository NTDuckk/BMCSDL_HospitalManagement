Use QLBenhVien
GO
/*Dich Vu Kham Benh: TypeId = 1 | Dich Vu Xet Nghiem: TypeId = 2 */

CREATE OR ALTER VIEW ViewXetNghiemBS
AS
SELECT 
    maDichVu,
    tenDichVu
FROM DICHVU
WHERE typeId = 2;
GO
	GRANT SELECT ON OBJECT::ViewXetNghiemBS TO userBenhVien;
GO

--drop view ViewXetNghiemBS

CREATE OR ALTER PROCEDURE sp_LayDanhSachXetNghiem
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM ViewXetNghiemBS;
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].sp_LayDanhSachXetNghiem TO userBenhVien
    AS [dbo];
GO

--drop proc sp_LayDanhSachXetNghiem
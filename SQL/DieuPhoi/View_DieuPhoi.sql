Use QLBenhVien
Go
CREATE OR ALTER VIEW viewDieuPhoi AS
SELECT 
    ROW_NUMBER() OVER (ORDER BY ct.ID) AS STT,
    bn.hoTen AS tenBenhNhan,
    tt.hoTen AS tenBacSi,
    ct.maDichVu,
    dv.tenDichVu,
    ct.maKhoa,
    k.tenKhoa,
    kb.maKhamBenh
FROM CHITIET_KHAMBENH ct
JOIN KHAMBENH kb ON ct.maKhamBenh = kb.maKhamBenh
JOIN BENHNHAN bn ON kb.maBenhNhan = bn.maBenhNhan
LEFT JOIN BACSI bs ON ct.maBacSiYeuCau = bs.maBacSi
LEFT JOIN THONGTIN_CANHAN tt ON bs.maBacSi = tt.maNhanVien
LEFT JOIN DICHVU dv ON ct.maDichVu = dv.maDichVu
LEFT JOIN KHOA k ON ct.maKhoa = k.maKhoa
WHERE ct.maPhongKham IS NULL
  AND CONVERT(date, ct.thoiGianKham) = CONVERT(date, GETDATE());
GO
	GRANT SELECT ON OBJECT::viewDieuPhoi TO userBenhVien;
GO
--DROP VIEW viewDieuPhoi;

CREATE PROCEDURE sp_DieuPhoi
as
begin
	select * from viewDieuPhoi
end
GO
GRANT EXECUTE
    ON OBJECT::[dbo].sp_DieuPhoi TO userBenhVien
    AS [dbo];
GO

--DROP PROCEDURE sp_DieuPhoi;
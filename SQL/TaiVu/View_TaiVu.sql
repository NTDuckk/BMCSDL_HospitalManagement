Use QLBenhVien
Go
CREATE OR ALTER VIEW viewTaiVu AS
SELECT 
    ROW_NUMBER() OVER (ORDER BY kb.maKhamBenh, ct.ID) AS STT,
    kb.maBenhNhan,
    bn.hoTen AS hoTenBenhNhan,
    dv.tenDichVu AS TenDichVu,
    ct.ID AS maChiTietKham,
    kb.maKhamBenh,
    ct.maDichVu
FROM CHITIET_KHAMBENH ct
JOIN KHAMBENH kb ON ct.maKhamBenh = kb.maKhamBenh
JOIN BENHNHAN bn ON kb.maBenhNhan = bn.maBenhNhan
JOIN DICHVU dv ON ct.maDichVu = dv.maDichVu
WHERE 
    (
        NOT EXISTS (
            SELECT 1 FROM CHITIET_HOADON_KHAMCHUABENH cthd
            WHERE cthd.maChiTietKham = ct.ID
        )
        OR EXISTS (
            SELECT 1
            FROM CHITIET_HOADON_KHAMCHUABENH cthd
            JOIN HOADON hd ON cthd.maHoaDon = hd.maHoaDon
            WHERE cthd.maChiTietKham = ct.ID AND hd.thanhToan = '0'
        )
    )
    AND CONVERT(DATE, kb.ngayKham) = CONVERT(DATE, GETDATE());
GO
	GRANT SELECT ON OBJECT::viewTaiVu TO userBenhVien;
GO

--drop view viewTaiVu


CREATE PROCEDURE sp_viewTaiVu
as
begin
	select * from viewTaiVu
end
GO
GRANT EXECUTE
    ON OBJECT::[dbo].sp_viewTaiVu TO userBenhVien
    AS [dbo];
GO
--drop proc sp_viewTaiVu
select * from DANHSACH_BENHNHAN
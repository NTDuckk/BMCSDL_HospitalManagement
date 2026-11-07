Use QLBenhVien
Go
CREATE OR ALTER VIEW view_ChiTietHoaDonTaiVu AS
SELECT 
    hd.maHoaDon,
    hd.maKhamBenh,
    bn.hoTen AS tenBenhNhan,
    ck.maDichVu,
    dv.tenDichVu,
    ct.donGia,
    hd.ngayLap,
    ct.maChiTietKham
FROM HOADON hd
JOIN CHITIET_HOADON_KHAMCHUABENH ct ON hd.maHoaDon = ct.maHoaDon
JOIN CHITIET_KHAMBENH ck ON ct.maChiTietKham = ck.ID
JOIN KHAMBENH kb ON hd.maKhamBenh = kb.maKhamBenh
JOIN BENHNHAN bn ON kb.maBenhNhan = bn.maBenhNhan
JOIN DICHVU dv ON ck.maDichVu = dv.maDichVu
WHERE hd.thanhToan = '0';
GO
	GRANT SELECT ON OBJECT::view_ChiTietHoaDonTaiVu TO userBenhVien;
GO

--drop view view_ChiTietHoaDonTaiVu

CREATE OR ALTER PROCEDURE sp_XemChiTietHoaDonTheoKhamBenh
    @maKhamBenh INT,
    @certName NVARCHAR(100)
WITH EXECUTE AS OWNER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX);

    SET @sql = '
        SELECT 
            maHoaDon,
            maKhamBenh,
            tenBenhNhan,
            maDichVu,
            tenDichVu,
            TRY_CAST(
                CONVERT(NVARCHAR(MAX), DecryptByCert(Cert_ID(''' + @certName + '''), donGia))
                AS DECIMAL(15,2)
            ) AS donGia,
            ngayLap,
            maChiTietKham
        FROM view_ChiTietHoaDonTaiVu
        WHERE maKhamBenh = @maKhamBenh;
    ';

    EXEC sp_executesql @sql, N'@maKhamBenh INT', @maKhamBenh;
END;
GO
	GRANT EXECUTE ON OBJECT::sp_XemChiTietHoaDonTheoKhamBenh TO userBenhVien AS [dbo];
GO



--drop proc sp_XemChiTietHoaDonTheoKhamBenh
Use QLBenhVien
go

CREATE OR ALTER PROCEDURE sp_ThemVaoDanhSachBenhNhan
    @maHoaDon INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE 
        @maKhamBenh INT,
        @maBenhNhan VARCHAR(10),
        @ngayKham DATETIME;


    SELECT @maKhamBenh = maKhamBenh
    FROM HOADON
    WHERE maHoaDon = @maHoaDon;


    SELECT 
        @maBenhNhan = kb.maBenhNhan,
        @ngayKham = kb.ngayKham
    FROM KHAMBENH kb
    WHERE kb.maKhamBenh = @maKhamBenh;

    -- Duyet cac phong kham, them neu chua co BN
    INSERT INTO DANHSACH_BENHNHAN(maBenhNhan, maPhongKham, maKhamBenh, ngayKham)
    SELECT DISTINCT 
        @maBenhNhan,
        ct.maPhongKham,
        @maKhamBenh,
        @ngayKham
    FROM CHITIET_KHAMBENH ct
    WHERE ct.maKhamBenh = @maKhamBenh
      AND ct.maPhongKham IS NOT NULL
      AND NOT EXISTS (
          SELECT 1 FROM DANHSACH_BENHNHAN d
          WHERE d.maBenhNhan = @maBenhNhan
            AND d.maPhongKham = ct.maPhongKham
            AND d.maKhamBenh = @maKhamBenh
            AND CONVERT(DATE, d.ngayKham) = CONVERT(DATE, @ngayKham)
      );
END
GO
	GRANT EXECUTE ON OBJECT::[dbo].sp_ThemVaoDanhSachBenhNhan TO userBenhVien AS [dbo];
GO

--drop proc sp_ThemVaoDanhSachBenhNhan
--select * from DANHSACH_BENHNHAN

CREATE OR ALTER PROCEDURE sp_CapNhatThanhToanHoaDon
    @maHoaDon INT,
    @soTienNhan DECIMAL(15, 2),
    @soTienThoi DECIMAL(15, 2)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1
        FROM HOADON
        WHERE maHoaDon = @maHoaDon AND thanhToan = '0'
    )
    BEGIN
        RAISERROR(N'Hoa don khong ton tai hoac da thanh toan.', 16, 1);
        RETURN;
    END

    UPDATE HOADON
    SET 
        soTienNhan = @soTienNhan,
        soTienThoi = @soTienThoi,
        thanhToan = '1'
    WHERE maHoaDon = @maHoaDon;

	EXEC sp_ThemVaoDanhSachBenhNhan @maHoaDon;
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].sp_CapNhatThanhToanHoaDon TO userBenhVien
    AS [dbo];
GO

--drop proc sp_CapNhatThanhToanHoaDon
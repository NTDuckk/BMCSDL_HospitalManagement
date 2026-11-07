Use QLBenhVien
Go
/*----------------------------------------
  Can chay View_BenhNhanTrongNgay truoc
----------------------------------------*/
CREATE OR ALTER PROCEDURE sp_CapNhatDangKham
    @maBenhNhan VARCHAR(10),
    @maPhongKham VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE DANHSACH_BENHNHAN
    SET tinhTrang = '2'
    WHERE maBenhNhan = @maBenhNhan
      AND maPhongKham = @maPhongKham
      AND CONVERT(DATE, ngayKham) = CONVERT(DATE, GETDATE());
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].sp_CapNhatDangKham TO userBenhVien
    AS [dbo];
GO
--drop proc sp_CapNhatDangKham


CREATE OR ALTER PROCEDURE sp_LayBenhNhanDauTienTheoPhong
    @maPhongKham VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @maBenhNhan VARCHAR(10);

    SELECT TOP 1 
        @maBenhNhan = maBenhNhan
    FROM viewBenhNhanTrongNgay
    WHERE maPhongKham = @maPhongKham
      AND tinhTrang IN ('0','2') 
    ORDER BY STT;

    IF @maBenhNhan IS NOT NULL
    BEGIN
        EXEC sp_CapNhatDangKham @maBenhNhan, @maPhongKham;
    END

    SELECT TOP 1 
        STT,
        maBenhNhan,
        hoTen,
        namSinh,
        chieuCao,
        canNang,
        gioiTinh,
        maKhamBenh,
        maPhongKham,
        tinhTrang
    FROM viewBenhNhanTrongNgay
    WHERE maPhongKham = @maPhongKham
      AND maBenhNhan = @maBenhNhan
    ORDER BY STT;
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].sp_LayBenhNhanDauTienTheoPhong TO userBenhVien
    AS [dbo];
GO
--drop proc sp_LayBenhNhanDauTienTheoPhong
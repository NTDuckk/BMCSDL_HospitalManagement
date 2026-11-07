use QLBenhVien
Go
CREATE OR ALTER PROCEDURE sp_KiemTraLichLamViec
    @maNhanVien VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @today DATE = CAST(GETDATE() AS DATE);

    SELECT  
        maLich,
        maNhanVien,
        maPhongKham,
        ngayLam,
        gioBatDau,
        gioKetThuc,
        ghiChu
    FROM LICH_LAMVIEC
    WHERE 
        maNhanVien = @maNhanVien
        AND ngayLam = @today;
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].sp_KiemTraLichLamViec TO userBenhVien
    AS [dbo];
GO
--drop proc sp_KiemTraLichLamViec

CREATE OR ALTER PROCEDURE sp_LayPhongKham
    @maNhanVien VARCHAR(10),
    @ngayLam DATE,
    @maPhongKham VARCHAR(10) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1 @maPhongKham = maPhongKham
    FROM LICH_LAMVIEC
    WHERE maNhanVien = @maNhanVien
      AND ngayLam = @ngayLam
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].sp_LayPhongKham TO userBenhVien
    AS [dbo];
GO
--drop proc sp_LayPhongKham
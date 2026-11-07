USE QLBenhVien;
GO

CREATE OR ALTER PROCEDURE sp_XoaDonGiaDichVu
    @maDichVu INT,
    @ngayApDung DATE
AS
BEGIN
    SET NOCOUNT ON;

   DELETE FROM DONGIA_DICHVU
	WHERE maDichVu = @maDichVu AND CONVERT(DATE, ngayApDung) = @ngayApDung;

END;
GO
	GRANT EXECUTE ON OBJECT::[dbo].sp_XoaDonGiaDichVu TO userBenhVien AS [dbo];
GO
--drop proc sp_XoaDonGiaDichVu

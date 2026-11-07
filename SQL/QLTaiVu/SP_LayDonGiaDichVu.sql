Use QLBenhVien
go

CREATE OR ALTER VIEW viewDonGiaDichVu
AS
SELECT 
    dv.maDichVu,
    dv.tenDichVu,
    dg.donGia,      
    dg.ngayApDung
FROM DICHVU dv
LEFT JOIN DONGIA_DICHVU dg ON dv.maDichVu = dg.maDichVu;
GO
	GRANT SELECT ON OBJECT::viewDonGiaDichVu TO userBenhVien;
GO

--drop view viewDonGiaDichVu

CREATE OR ALTER PROCEDURE sp_LayDonGiaDichVu 
    @certName NVARCHAR(100)
WITH EXECUTE AS OWNER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX);

    SET @sql = '
        SELECT 
            maDichVu,
            tenDichVu,
            TRY_CAST(
                CONVERT(NVARCHAR(MAX), DecryptByCert(Cert_ID(''' + @certName + '''), donGia))
                AS DECIMAL(15,2)
            ) AS donGia,
            ngayApDung
        FROM viewDonGiaDichVu;
    ';

    EXEC sp_executesql @sql;
END;
GO

	GRANT EXECUTE
    ON OBJECT::[dbo].sp_LayDonGiaDichVu TO userBenhVien
    AS [dbo];
GO
--drop proc sp_LayDonGiaDichVu
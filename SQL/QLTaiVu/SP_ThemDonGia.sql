Use QLBenhVien
go

--CREATE OR ALTER PROCEDURE sp_ThemDonGiaThuoc
--    @maThuoc VARCHAR(10),
--    @donGia FLOAT,
--    @ngayApDung DATE,
--    @keyName NVARCHAR(100),         
--    @keyPassword NVARCHAR(100)      
--WITH EXECUTE AS OWNER
--AS
--BEGIN
--    SET NOCOUNT ON;

--    DECLARE @encryptedGia VARBINARY(MAX);
--    DECLARE @sqlOpenKey NVARCHAR(MAX);
--    DECLARE @sqlCloseKey NVARCHAR(MAX);

--    SET @sqlOpenKey = '
--        OPEN ASYMMETRIC KEY [' + @keyName + ']
--        DECRYPTION BY PASSWORD = ''' + @keyPassword + ''';';
--    EXEC (@sqlOpenKey);

--    SET @encryptedGia = EncryptByAsymKey(
--        AsymKey_ID(@keyName),
--        CONVERT(VARBINARY(MAX), @donGia)
--    );

--    SET @sqlCloseKey = 'CLOSE ASYMMETRIC KEY [' + @keyName + '];';
--    EXEC (@sqlCloseKey);

--    INSERT INTO DONGIA_THUOC (maThuoc, donGia, ngayApDung)
--    VALUES (@maThuoc, @encryptedGia, @ngayApDung);
--END;
--GO
--	GRANT EXECUTE ON OBJECT::[dbo].sp_ThemDonGiaThuoc TO userBenhVien AS [dbo];
--GO

--drop proc sp_ThemDonGiaThuoc


CREATE OR ALTER PROCEDURE sp_ThemDonGiaDichVu
    @maDichVu INT,
    @donGia DECIMAL(15,2),
    @ngayApDung DATE,
    @certName NVARCHAR(100)
WITH EXECUTE AS OWNER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX);
    SET @sql = '
        DECLARE @encryptedGia VARBINARY(MAX);
        SET @encryptedGia = EncryptByCert(
            Cert_ID(''' + @certName + '''),
            CONVERT(NVARCHAR(MAX), ' + CAST(@donGia AS NVARCHAR) + ')
        );

        INSERT INTO DONGIA_DICHVU (maDichVu, donGia, ngayApDung)
        VALUES (' + CAST(@maDichVu AS NVARCHAR) + ', @encryptedGia, ''' + CONVERT(NVARCHAR(10), @ngayApDung, 120) + ''');
    ';

    EXEC sp_executesql @sql;
END;
GO

GRANT EXECUTE ON OBJECT::[dbo].sp_ThemDonGiaDichVu TO userBenhVien AS [dbo];
GO

--drop proc sp_ThemDonGiaDichVu

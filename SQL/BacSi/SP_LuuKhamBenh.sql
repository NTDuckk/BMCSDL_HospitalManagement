Use QLBenhVien
Go
CREATE OR ALTER PROCEDURE sp_UpdateKhamBenh_MaHoa
    @ma NVARCHAR(100),    
    @BSCert NVARCHAR(100),
    @maKhamBenh INT,
    @trieuChung NVARCHAR(MAX),
    @chanDoanCuoiCung NVARCHAR(MAX)
WITH EXECUTE AS OWNER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX);

    SET @sql = '
    OPEN SYMMETRIC KEY [' + @ma + ']
    DECRYPTION BY CERTIFICATE [' + @BSCert + ']
    WITH PASSWORD = ''Cert_P@$$wOrd'';

    UPDATE KHAMBENH
    SET 
        trieuChung = EncryptByKey(
            Key_GUID(N''' + @ma + '''), 
            CONVERT(VARBINARY(MAX), @tc),
            1,
            CONVERT(NVARCHAR(100), N''' + @ma + ''')
        ),
        chanDoanCuoiCung = EncryptByKey(
            Key_GUID(N''' + @ma + '''), 
            CONVERT(VARBINARY(MAX), @cd),
            1,
            CONVERT(NVARCHAR(100), N''' + @ma + ''')
        )
    WHERE maKhamBenh = @mkb;

    CLOSE SYMMETRIC KEY [' + @ma + '];';

    EXEC sp_executesql @sql,
        N'@tc NVARCHAR(MAX), @cd NVARCHAR(MAX), @mkb INT',
        @tc = @trieuChung,
        @cd = @chanDoanCuoiCung,
        @mkb = @maKhamBenh;

    UPDATE DANHSACH_BENHNHAN
    SET TinhTrang = '1'
    WHERE maKhamBenh = @maKhamBenh;

    UPDATE CHITIET_KHAMBENH
    SET TrangThai = '1'
    WHERE maKhamBenh = @maKhamBenh;
END;
GO
	GRANT EXECUTE ON OBJECT::[dbo].sp_UpdateKhamBenh_MaHoa TO userBenhVien AS [dbo];
GO

--drop proc sp_UpdateKhamBenh_MaHoa


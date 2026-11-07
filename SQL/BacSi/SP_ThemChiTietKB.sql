Use QLBenhVien
Go

CREATE OR ALTER PROCEDURE sp_ThemChiTietKhamBenh
    @maKhamBenh INT,
    @maBSYeuCau VARCHAR(10),
    @maDichVu INT,
    @ghiChu NVARCHAR(MAX),
    @ma NVARCHAR(100),      
    @BSCert NVARCHAR(100)     
WITH EXECUTE AS OWNER
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM KHAMBENH WHERE maKhamBenh = @maKhamBenh)
    BEGIN
        RAISERROR(N'Mã khám bệnh không tồn tại', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM DICHVU WHERE maDichVu = @maDichVu)
    BEGIN
        RAISERROR(N'Mã dịch vụ không tồn tại', 16, 1);
        RETURN;
    END

    -- Tạo SYMMETRIC KEY nếu chưa tồn tại
    IF NOT EXISTS (SELECT 1 FROM sys.symmetric_keys WHERE name = @ma)
    BEGIN
        DECLARE @sqlCreateKey NVARCHAR(MAX);
        SET @sqlCreateKey = '
        CREATE SYMMETRIC KEY [' + @ma + '] 
        WITH ALGORITHM = AES_192 
        ENCRYPTION BY CERTIFICATE [' + @BSCert + '];';
        EXEC (@sqlCreateKey);

        DECLARE @sqlGrant NVARCHAR(MAX);
        SET @sqlGrant = '
        GRANT CONTROL ON SYMMETRIC KEY::[' + @ma + '] TO userBenhVien;';
        EXEC (@sqlGrant);
    END

    -- Mã hóa và thêm vào bảng CHITIET_KHAMBENH
    DECLARE @sql NVARCHAR(MAX);
    SET @sql = '
    OPEN SYMMETRIC KEY [' + @ma + '] 
    DECRYPTION BY CERTIFICATE [' + @BSCert + '] 
    WITH PASSWORD = ''Cert_P@$$wOrd'';

    INSERT INTO CHITIET_KHAMBENH (
        maKhamBenh,
        maBacSiYeuCau,
        maDichVu,
        ghiChuBacSiKham
    )
    VALUES (
        @maKhamBenh,
        @maBSYeuCau,
        @maDichVu,
        EncryptByKey(Key_GUID(''' + @ma + '''), CONVERT(VARBINARY(MAX), @ghiChu), 1, ''' + @ma + ''')
    );

    CLOSE SYMMETRIC KEY [' + @ma + '];';

    EXEC sp_executesql @sql,
        N'@maKhamBenh INT, @maBSYeuCau VARCHAR(10), @maDichVu INT, @ghiChu NVARCHAR(MAX)',
        @maKhamBenh = @maKhamBenh,
        @maBSYeuCau = @maBSYeuCau,
        @maDichVu = @maDichVu,
        @ghiChu = @ghiChu;

    -- Cap nhat benh nhan khong con trong phong kham
    UPDATE DANHSACH_BENHNHAN
    SET tinhTrang = '3'
    WHERE maKhamBenh = @maKhamBenh;
END
GO
	GRANT EXECUTE ON OBJECT::[dbo].sp_ThemChiTietKhamBenh TO userBenhVien AS [dbo];
GO

--drop proc sp_ThemChiTietKhamBenh


use QLBenhVien
go

CREATE OR ALTER PROCEDURE sp_ThemBenhNhan
    @hoTen NVARCHAR(100),
    @gioiTinh VARCHAR(3),
    @chieuCao FLOAT,
    @canNang FLOAT,
    @namSinh INT,
    @diaChi NVARCHAR(255),
    @soDienThoai VARCHAR(20),
    @maKhoa VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DECLARE @soLuong INT;
        DECLARE @maBenhNhan VARCHAR(10);
        DECLARE @maDichVu INT;
        DECLARE @newId INT;

        SELECT @soLuong = COUNT(*) FROM BENHNHAN;
        SET @soLuong = @soLuong + 1;
        SET @maBenhNhan = 'BN' + RIGHT('0000' + CAST(@soLuong AS VARCHAR), 4);


        INSERT INTO BENHNHAN (maBenhNhan, hoTen, gioiTinh, chieuCao, canNang, namSinh, diaChi, soDienThoai)
        VALUES (@maBenhNhan, @hoTen, @gioiTinh, @chieuCao, @canNang, @namSinh, @diaChi, @soDienThoai);

        SET @maDichVu = CASE 
                            WHEN @maKhoa = 'K01' THEN 1
                            ELSE 2
                       END;


        INSERT INTO KHAMBENH (maBenhNhan)
        VALUES (@maBenhNhan);

        SET @newId = SCOPE_IDENTITY();

        INSERT INTO CHITIET_KHAMBENH (maKhamBenh, maDichVu, maKhoa)
        VALUES (@newId, @maDichVu, @maKhoa);

    END TRY
    BEGIN CATCH
        DECLARE @errMsg NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @errLine INT = ERROR_LINE();
        DECLARE @errNum INT = ERROR_NUMBER();

        RAISERROR('Lỗi %d ở dòng %d: %s', 16, 1, @errNum, @errLine, @errMsg);
    END CATCH
END;
GO
GRANT EXECUTE
    ON OBJECT::[dbo].sp_ThemBenhNhan TO userBenhVien
    AS [dbo];
GO


--drop proc sp_ThemBenhNhan

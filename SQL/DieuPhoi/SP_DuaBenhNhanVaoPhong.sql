USE QLBenhVien;
GO
CREATE OR ALTER PROCEDURE sp_DieuPhoi_UpdateMaPhong
    @stt INT,
    @maPhongKham VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @id INT;

    SELECT @id = ID
    FROM (
        SELECT ROW_NUMBER() OVER (ORDER BY ID) AS STT, ID
        FROM CHITIET_KHAMBENH
        WHERE maPhongKham IS NULL
          AND CONVERT(DATE, thoiGianKham) = CONVERT(DATE, GETDATE())
    ) AS sub
    WHERE sub.STT = @stt;

    IF @id IS NOT NULL
    BEGIN
        UPDATE CHITIET_KHAMBENH
        SET maPhongKham = @maPhongKham
        WHERE ID = @id;
    END
END;
Go
GRANT EXECUTE
    ON OBJECT::[dbo].sp_DieuPhoi_UpdateMaPhong TO userBenhVien
    AS [dbo];
GO

--drop proc sp_DieuPhoi_UpdateMaPhong
Use QLBenhVien
go
CREATE VIEW viewPhongKhamDP AS
SELECT *
FROM PHONGKHAM
GO
	GRANT SELECT ON OBJECT::viewPhongKhamDP TO userBenhVien ;
GO
--DROP VIEW viewPhongKhamDP;

CREATE OR ALTER PROCEDURE sp_viewPhongKhamDP
    @maKhoa VARCHAR(10) = NULL,
    @maDichVu INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM viewPhongKhamDP
    WHERE maDichVu = @maDichVu
      AND (@maKhoa IS NULL OR @maKhoa = '' OR maKhoa = @maKhoa);
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].sp_viewPhongKhamDP TO userBenhVien
    AS [dbo];
GO
--drop proc sp_viewPhongKhamDP
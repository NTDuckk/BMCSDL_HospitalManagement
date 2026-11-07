Use QLBenhVien
go
CREATE OR ALTER VIEW viewBenhNhanTrongNgay AS
SELECT 
    ROW_NUMBER() OVER (ORDER BY dsb.maBenhNhan) AS STT,
    dsb.maBenhNhan,
    bn.hoTen,
    bn.namSinh,
    bn.chieuCao,
    bn.canNang,
    bn.gioiTinh,
    kb.maKhamBenh,
    dsb.maPhongKham,
    dsb.tinhTrang
FROM DANHSACH_BENHNHAN dsb
JOIN BENHNHAN bn ON dsb.maBenhNhan = bn.maBenhNhan
JOIN KHAMBENH kb ON kb.maBenhNhan = dsb.maBenhNhan 
                 AND CONVERT(DATE, kb.ngayKham) = CONVERT(DATE, dsb.ngayKham)
WHERE CONVERT(DATE, dsb.ngayKham) = CONVERT(DATE, GETDATE())
  AND dsb.tinhTrang <> '1';
GO
	GRANT SELECT ON OBJECT::viewBenhNhanTrongNgay TO userBenhVien;
GO
--drop view viewBenhNhanTrongNgay

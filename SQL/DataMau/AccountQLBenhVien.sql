USE QLBenhVien_ACCOUNT;
GO
INSERT INTO ACCOUNT (username, passwordHash, typeID)VALUES 
('admin1', HASHBYTES('SHA2_256', '123'), '0'),
('admin2', HASHBYTES('SHA2_256', '123'), '0'),
('admin3', HASHBYTES('SHA2_256', '123'), '0');

--Tài khoản QL Tài vụ
INSERT INTO ACCOUNT (username, passwordHash, typeID, maNhanVien)VALUES 
('bs1', HASHBYTES('SHA2_256', '123'), '1', 'BS00001'),
('bs2', HASHBYTES('SHA2_256', '123'), '1', 'BS00002'),
('bs3', HASHBYTES('SHA2_256', '123'), '1', 'BS00003'),
('bs7', HASHBYTES('SHA2_256', '123'), '1', 'BS00007');

INSERT INTO ACCOUNT (username, passwordHash, typeID, maNhanVien)VALUES 
('nvdp1', HASHBYTES('SHA2_256', '123'), '2', 'NVDP00001'),
('nvdp2', HASHBYTES('SHA2_256', '123'), '2', 'NVDP00002');
INSERT INTO ACCOUNT (username, passwordHash, typeID, maNhanVien)VALUES 
('nvtv1', HASHBYTES('SHA2_256', '123'), '3', 'NVTV00001'),
('nvtv2', HASHBYTES('SHA2_256', '123'), '3', 'NVTV00001');

INSERT INTO ACCOUNT (username, passwordHash, typeID, maNhanVien)VALUES 
('qlns1', HASHBYTES('SHA2_256', '123'), '4', 'QLNS00001'),
('qlns2', HASHBYTES('SHA2_256', '123'), '4', 'QLNS00002');

INSERT INTO ACCOUNT (username, passwordHash, typeID, maNhanVien)VALUES 
('qltv1', HASHBYTES('SHA2_256', '123'), '5', 'QLTV00001'),
('qltv2', HASHBYTES('SHA2_256', '123'), '5', 'QLTV00002');

INSERT INTO PERMISSION VALUES
('admin');
--Quyền quản lý nhân sự
INSERT INTO PERMISSION VALUES
('Modify_PhongBan') /*2*/,
('Modify_Khoa')	/*3*/,
('Modify_BacSi')/*4*/,
('Modify_NhanVien')/*5*/,
('Modify_LichLamViec')/*6*/,
('View_ChamCong') /*7*/,
('View_Luong') /*8*/;

--Quyền quản lý tài vụ
INSERT INTO PERMISSION VALUES
('Modify_DonGiaDichVu') /*9*/,
('Modify_DonGiaThuoc') /*10*/,
('View_HoaDonThuoc') /*11*/,
('View_HoaDonKCB') /*12*/,
--('View_ChamCong')/*7*/,
--('View_Luong') /*8*/,
('View_BacSi') /*13*/,
('View_NhanVien') /*14*/;

--Quyền nhân viên điều phối
INSERT INTO PERMISSION VALUES
('Modify_BenhNhan') /*15*/,
('Cordinate_BenhNhan') /*16*/;

--Quyền nhân viên tài vụ
INSERT INTO PERMISSION VALUES
('View_ThuTucKCB') /*17*/,
('View_Coordinate_BenhNhan') /*18*/,
('Modify_HoaDonKCB') /*19*/;

--Quyền bác sĩ
INSERT INTO PERMISSION VALUES
('View_LichLamViec') /*20*/,
('View_BenhNhan') /*21*/,
('KhamBenh') /*22*/,
('View_DanhSachBenhNhan') /*23*/;


INSERT INTO ACCOUNT_PERMISSION (username, permissionID)
VALUES 
    ('bs1', 20),
    ('bs1', 21),
    ('bs1', 22),
    ('bs1', 23),
    ('bs2', 20),
	('bs2', 21),
    ('bs2', 22),
    ('bs2', 23);
INSERT INTO ACCOUNT_PERMISSION (username, permissionID)
VALUES 
    ('admin1', 1),
    ('admin2', 1),
    ('admin3', 1);
	INSERT INTO ACCOUNT_PERMISSION (username, permissionID)
VALUES 
	('nvdp1', 15),
	('nvdp1', 16);

INSERT INTO ACCOUNT_PERMISSION (username, permissionID)
VALUES 
	('nvtv1', 17),
	('nvtv1', 18),
	('nvtv1', 19);

INSERT INTO ACCOUNT_PERMISSION (username, permissionID)
VALUES 
	('qltv1', 9),
	('qltv1', 10);
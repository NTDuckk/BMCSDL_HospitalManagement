use QLBenhVien
go

--------KHOA--------
INSERT INTO KHOA VALUES 
('K01',N'KHOA KHÁM BỆNH TỔNG QUÁT'),
('K02',N'KHOA MŨI XOANG'),
('K03',N'KHOA TAI');
--test
INSERT INTO KHOA VALUES 
('K04',N'KHOA KHÁM BỆNH 1'),
('K05',N'KHOA TỔNG HỢP 1'),
('K06',N'KHOA PHẪU THUẬT 1'),
('K07',N'KHOA KHÁM BỆNH 2'),
('K08',N'KHOA TỔNG HỢP 2'),
('K09',N'KHOA PHẪU THUẬT 2'),
('K10',N'KHOA KHÁM BỆNH 3'),
('K11',N'KHOA TỔNG HỢP 3'),
('K12',N'KHOA PHẪU THUẬT 3');

--------DICHVU--------
INSERT INTO DICHVU (tenDichVu, typeID) VALUES 
(N'Khám Bệnh Sàng Lọc', '1'),
(N'Khám Bệnh Chuyên Khoa','1');
INSERT INTO DICHVU (tenDichVu, typeID) VALUES 
(N'Xét Nghiệm Dịch Tai','2'),
(N'Chụp CT tai','2'),
(N'Nội soi mũi xoang','2'),
(N'CT scan xoang','2');


--------PHONGKHAM--------
INSERT INTO PHONGKHAM (maPhongKham, tenPhongKham, maKhoa, maDichVu)
VALUES 
('PK01', N'P.Khám sàng lọc 1', 'K01', 1),
('PK02', N'P.Khám sàng lọc 2', 'K01', 1),
('PK03', N'P.Khám sàng lọc 3', 'K01', 1);

INSERT INTO PHONGKHAM (maPhongKham, tenPhongKham, maKhoa, maDichVu)
VALUES 
('PK04', N'P.Khám chuyên khoa mũi xoang 1', 'K02', 2),
('PK05', N'P.Khám chuyên khoa mũi xoang 2', 'K02', 2),
('PK06', N'P.Khám chuyên khoa mũi xoang 3', 'K02', 2);
INSERT INTO PHONGKHAM (maPhongKham, tenPhongKham, maKhoa, maDichVu)
VALUES 
('PK07', N'P.Khám chuyên khoa tai 1', 'K03', 2),
('PK08', N'P.Khám chuyên khoa tai 2', 'K03', 2),
('PK09', N'P.Khám chuyên khoa tai 3', 'K03', 2);
INSERT INTO PHONGKHAM (maPhongKham, tenPhongKham, maKhoa, maDichVu)
VALUES 
('PCN01', N'P.Nội soi mũi xoang', 'K02', 5),
('PCN02', N'P.CT scan xoang', 'K02', 6),
('PCN03', N'P.Xét nghiệm dịch tai', 'K03', 3),
('PCN04', N'P.Chụp CT tai', 'K03', 4);

--------DONGIA_DICHVU--------

DECLARE @certName NVARCHAR(100) = 'CertQLTV';

INSERT INTO DONGIA_DICHVU(maDichVu, donGia, ngayApDung)
VALUES 
(1, EncryptByCert(Cert_ID(@certName), CONVERT(NVARCHAR, 150000)), '2025-01-01'),
(2, EncryptByCert(Cert_ID(@certName), CONVERT(NVARCHAR, 250000)), '2025-01-01'),
(3, EncryptByCert(Cert_ID(@certName), CONVERT(NVARCHAR, 180000)), '2025-01-01'),
(4, EncryptByCert(Cert_ID(@certName), CONVERT(NVARCHAR, 500000)), '2025-01-01'),
(5, EncryptByCert(Cert_ID(@certName), CONVERT(NVARCHAR, 300000)), '2025-01-01'),
(6, EncryptByCert(Cert_ID(@certName), CONVERT(NVARCHAR, 700000)), '2025-01-01');


--------THONGTIN_CANHAN--------
INSERT INTO THONGTIN_CANHAN (maNhanVien, hoTen, ngaySinh, diaChi, soDienThoai, email, luongCoBan, phuCap)
VALUES 
('BS00001', N'Nguyễn Văn A', '1980-05-10', N'123 Đường ABC, Quận 1, TP.HCM', '0909123456', 'a.nguyen@example.com',
 CONVERT(VARBINARY(MAX), 20000000), CONVERT(VARBINARY(MAX), 3000000)),

('BS00002', N'Trần Thị B', '1985-08-22', N'456 Đường DEF, Quận 3, TP.HCM', '0912345678', 'b.tran@example.com',
 CONVERT(VARBINARY(MAX), 22000000), CONVERT(VARBINARY(MAX), 3500000)),

('BS00003', N'Lê Văn C', '1978-12-01', N'789 Đường GHI, Quận 5, TP.HCM', '0987654321', 'c.le@example.com',
 CONVERT(VARBINARY(MAX), 25000000), CONVERT(VARBINARY(MAX), 4000000));
 INSERT INTO THONGTIN_CANHAN (maNhanVien, hoTen, ngaySinh, diaChi, soDienThoai, email, luongCoBan, phuCap)
VALUES 
('BS00004', N'Phạm Thị D', '1982-03-15', N'12 Đường Trần Hưng Đạo, Quận 4, TP.HCM', '0911122334', 'd.pham@example.com',
 CONVERT(VARBINARY(MAX), 21000000), CONVERT(VARBINARY(MAX), 3200000)),

('BS00005', N'Hoàng Văn E', '1979-11-30', N'34 Đường Lê Lai, Quận 10, TP.HCM', '0933221100', 'e.hoang@example.com',
 CONVERT(VARBINARY(MAX), 24000000), CONVERT(VARBINARY(MAX), 3600000)),

('BS00006', N'Ngô Thị F', '1987-07-19', N'56 Đường Nguyễn Du, Quận 7, TP.HCM', '0966112233', 'f.ngo@example.com',
 CONVERT(VARBINARY(MAX), 23000000), CONVERT(VARBINARY(MAX), 3100000)),

('BS00007', N'Tăng Văn G', '1981-04-04', N'78 Đường CMT8, Quận 11, TP.HCM', '0977888999', 'g.tang@example.com',
 CONVERT(VARBINARY(MAX), 25500000), CONVERT(VARBINARY(MAX), 4100000)),

('BS00008', N'Bùi Thị H', '1986-09-12', N'90 Đường Hoàng Hoa Thám, Quận Phú Nhuận, TP.HCM', '0922334455', 'h.bui@example.com',
 CONVERT(VARBINARY(MAX), 21500000), CONVERT(VARBINARY(MAX), 3050000)),

('BS00009', N'Đặng Văn I', '1983-02-28', N'101 Đường Phan Xích Long, Quận Bình Thạnh, TP.HCM', '0944556677', 'i.dang@example.com',
 CONVERT(VARBINARY(MAX), 24500000), CONVERT(VARBINARY(MAX), 3950000)),

('BS00010', N'Võ Thị K', '1984-06-18', N'202 Đường Lý Thường Kiệt, Quận Tân Bình, TP.HCM', '0977999888', 'k.vo@example.com',
 CONVERT(VARBINARY(MAX), 23500000), CONVERT(VARBINARY(MAX), 3700000));
 INSERT INTO THONGTIN_CANHAN (maNhanVien, hoTen, ngaySinh, diaChi, soDienThoai, email, luongCoBan, phuCap)
VALUES 
('BS00011', N'Nguyễn Thị L', '1988-03-22', N'111 Đường Nguyễn Tri Phương, Quận 10, TP.HCM', '0901112233', 'l.nguyen@example.com',
 CONVERT(VARBINARY(MAX), 22500000), CONVERT(VARBINARY(MAX), 3300000)),

('BS00012', N'Phạm Văn M', '1975-10-05', N'222 Đường Nguyễn Văn Cừ, Quận 5, TP.HCM', '0911223344', 'm.pham@example.com',
 CONVERT(VARBINARY(MAX), 25500000), CONVERT(VARBINARY(MAX), 4200000)),

('BS00013', N'Lê Thị N', '1990-07-17', N'333 Đường Điện Biên Phủ, Quận Bình Thạnh, TP.HCM', '0922334455', 'n.le@example.com',
 CONVERT(VARBINARY(MAX), 23500000), CONVERT(VARBINARY(MAX), 3100000));


INSERT INTO THONGTIN_CANHAN ( maNhanVien, hoTen, ngaySinh, diaChi, soDienThoai, email, luongCoBan, phuCap)
VALUES (
    'NVDP00001',
    N'Nguyễn Văn DP',
    '1990-01-01',
    N'123 Đường ABC, TP.HCM',
    '0909123456',
    'nvdp01@example.com',
    CONVERT(VARBINARY(MAX), 8000000),
    CONVERT(VARBINARY(MAX), 1000000)
);

INSERT INTO THONGTIN_CANHAN ( maNhanVien, hoTen, ngaySinh, diaChi, soDienThoai, email, luongCoBan, phuCap)
VALUES (
    'NVTV00001',
    N'Nguyễn Văn TV',
    '1990-01-01',
    N'123 Đường ABC, TP.HCM',
    '0909123456',
    'nvtv01@example.com',
    CONVERT(VARBINARY(MAX), 8000000),
    CONVERT(VARBINARY(MAX), 1000000)
);
--------BACSI--------
INSERT INTO BACSI (maBacSi, maKhoa, chuyenMon) VALUES 
('BS00001', 'K01', N'khám tổng quát lâm sàng'),
('BS00002', 'K01', N'khám tổng quát lâm sàng');
INSERT INTO BACSI (maBacSi, maKhoa, chuyenMon) VALUES
('BS00011', 'K01', N'khám tổng quát lâm sàng');

INSERT INTO BACSI (maBacSi, maKhoa, chuyenMon)
VALUES 
('BS00003', 'K02', N'chuyên khoa mũi xoang'),
('BS00004', 'K02', N'chuyên khoa mũi xoang');
INSERT INTO BACSI (maBacSi, maKhoa, chuyenMon) VALUES
('BS00012', 'K02', N'chuyên khoa mũi xoang');

 INSERT INTO BACSI (maBacSi, maKhoa, chuyenMon)
VALUES 
('BS00005', 'K03', N'chuyên khoa tai'),
('BS00006', 'K03', N'chuyên khoa tai');
INSERT INTO BACSI (maBacSi, maKhoa, chuyenMon) VALUES
('BS00013', 'K03', N'chuyên khoa tai');

INSERT INTO BACSI (maBacSi, maKhoa, chuyenMon)
VALUES 
('BS00007', 'K02', N'Nội soi mũi'),
('BS00008', 'K02', N'Chụp CT scan xoang'),
('BS00009', 'K03', N'Xét nghiệm tai'),
('BS00010', 'K03', N'Chụp CT tai');

--------PHONGBAN--------
INSERT INTO PHONGBAN (maPhongBan, tenPhongBan)
VALUES 
('PB01', N'Phòng tài vụ'),
('PB02', N'Phòng điều phối bệnh nhân');

--------NHANVIEN--------
INSERT INTO NHANVIEN (maNhanVien, maPhongBan)
VALUES ('NVTV00001', 'PB01')

--------LICH_LAMVIEC--------
INSERT INTO LICH_LAMVIEC (maNhanVien, maPhongKham, ngayLam, gioBatDau, gioKetThuc)
VALUES
-- PK01
('BS00001', 'PK01', '2025-05-27', '07:00', '16:00'),
('BS00001', 'PK01', '2025-05-30', '07:00', '16:00'),

('BS00002', 'PK01', '2025-05-28', '07:00', '16:00'),
('BS00002', 'PK01', '2025-05-31', '07:00', '16:00'),

('BS00011', 'PK01', '2025-05-29', '07:00', '16:00'),
('BS00011', 'PK01', '2025-06-01', '07:30', '14:00'),

-- PK02
('BS00001', 'PK02', '2025-05-28', '07:00', '16:00'),
('BS00001', 'PK02', '2025-05-31', '07:00', '16:00'),

('BS00002', 'PK02', '2025-05-29', '07:00', '16:00'),
('BS00002', 'PK02', '2025-06-01', '07:30', '14:00'),

('BS00011', 'PK02', '2025-05-27', '07:00', '16:00'),
('BS00011', 'PK02', '2025-05-30', '07:00', '16:00');
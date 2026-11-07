using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QLBenhVien.Models;

namespace QLBenhVien.Entities;

public partial class QlbenhVienContext : DbContext
{
    public QlbenhVienContext()
    {
    }

    public QlbenhVienContext(DbContextOptions<QlbenhVienContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bacsi> Bacsis { get; set; }

    public virtual DbSet<Benhnhan> Benhnhans { get; set; }

    public virtual DbSet<Chamcong> Chamcongs { get; set; }

    public virtual DbSet<ChitietHoadonKhamchuabenh> ChitietHoadonKhamchuabenhs { get; set; }

    public virtual DbSet<ChitietHoadonThuoc> ChitietHoadonThuocs { get; set; }

    public virtual DbSet<ChitietKhambenh> ChitietKhambenhs { get; set; }

    public virtual DbSet<ChitietToathuoc> ChitietToathuocs { get; set; }

    public virtual DbSet<DanhsachBenhnhan> DanhsachBenhnhans { get; set; }

    public virtual DbSet<Dichvu> Dichvus { get; set; }

    public virtual DbSet<DongiaDichvu> DongiaDichvus { get; set; }

    public virtual DbSet<DongiaThuoc> DongiaThuocs { get; set; }

    public virtual DbSet<Hoadon> Hoadons { get; set; }

    public virtual DbSet<Khambenh> Khambenhs { get; set; }

    public virtual DbSet<Khoa> Khoas { get; set; }

    public virtual DbSet<LichLamviec> LichLamviecs { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Phongban> Phongbans { get; set; }

    public virtual DbSet<Phongkham> Phongkhams { get; set; }

    public virtual DbSet<ThongtinCanhan> ThongtinCanhans { get; set; }

    public virtual DbSet<Thuoc> Thuocs { get; set; }

    public virtual DbSet<Toathuoc> Toathuocs { get; set; }

    public virtual DbSet<ViewBenhNhanTrongNgay> ViewBenhNhanTrongNgays { get; set; }

    public virtual DbSet<ViewChiTietHoaDonTaiVu> ViewChiTietHoaDonTaiVus { get; set; }

    public virtual DbSet<ViewDieuPhoi> ViewDieuPhois { get; set; }

    public virtual DbSet<ViewDonGiaDichVu> ViewDonGiaDichVus { get; set; }

    public virtual DbSet<ViewKhoa> ViewKhoas { get; set; }

    public virtual DbSet<ViewPhongKhamDp> ViewPhongKhamDps { get; set; }

    public virtual DbSet<ViewTaiVu> ViewTaiVus { get; set; }

    public virtual DbSet<ViewThongTinNhanVien> ViewThongTinNhanViens { get; set; }

    public virtual DbSet<ViewXetNghiemB> ViewXetNghiemBs { get; set; }
    public virtual DbSet<DonGiaDichVuGiaiMa> DonGiaDichVuGiaiMas { get; set; }
    public DbSet<MaNhanVien> MaNhanViens { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.; Database=QLBenhVien; Integrated Security=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DonGiaDichVuGiaiMa>().HasNoKey();
        modelBuilder.Entity<Bacsi>(entity =>
        {
            entity.HasKey(e => e.MaBacSi).HasName("PK__BACSI__F48AA2379C1A4ADF");

            entity.ToTable("BACSI");

            entity.Property(e => e.MaBacSi)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maBacSi");
            entity.Property(e => e.ChuyenMon)
                .HasMaxLength(50)
                .HasColumnName("chuyenMon");
            entity.Property(e => e.MaKhoa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maKhoa");

            entity.HasOne(d => d.MaBacSiNavigation).WithOne(p => p.Bacsi)
                .HasForeignKey<Bacsi>(d => d.MaBacSi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BACSI__maBacSi__5070F446");

            entity.HasOne(d => d.MaKhoaNavigation).WithMany(p => p.Bacsis)
                .HasForeignKey(d => d.MaKhoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BACSI__maKhoa__4F7CD00D");
        });

        modelBuilder.Entity<Benhnhan>(entity =>
        {
            entity.HasKey(e => e.MaBenhNhan).HasName("PK__BENHNHAN__E623122AFB440BD9");

            entity.ToTable("BENHNHAN");

            entity.Property(e => e.MaBenhNhan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maBenhNhan");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(255)
                .HasColumnName("diaChi");
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.HoTen)
                .HasMaxLength(100)
                .HasColumnName("hoTen");
            entity.Property(e => e.NamSinh).HasColumnName("namSinh");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ngayTao");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("soDienThoai");
        });

        modelBuilder.Entity<Chamcong>(entity =>
        {
            entity.HasKey(e => e.MaChamCong).HasName("PK__CHAMCONG__6851BA22305EF9C9");

            entity.ToTable("CHAMCONG");

            entity.Property(e => e.MaChamCong).HasColumnName("maChamCong");
            entity.Property(e => e.GhiChu).HasColumnName("ghiChu");
            entity.Property(e => e.GioRa).HasColumnName("gioRa");
            entity.Property(e => e.GioVao).HasColumnName("gioVao");
            entity.Property(e => e.MaNhanVien)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maNhanVien");
            entity.Property(e => e.NgayChamCong).HasColumnName("ngayChamCong");

            entity.HasOne(d => d.MaNhanVienNavigation).WithMany(p => p.Chamcongs)
                .HasForeignKey(d => d.MaNhanVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHAMCONG__maNhan__534D60F1");
        });

        modelBuilder.Entity<ChitietHoadonKhamchuabenh>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CHITIET___3214EC27693285B5");

            entity.ToTable("CHITIET_HOADON_KHAMCHUABENH");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DonGia).HasColumnName("donGia");
            entity.Property(e => e.MaChiTietKham).HasColumnName("maChiTietKham");
            entity.Property(e => e.MaHoaDon).HasColumnName("maHoaDon");

            entity.HasOne(d => d.MaChiTietKhamNavigation).WithMany(p => p.ChitietHoadonKhamchuabenhs)
                .HasForeignKey(d => d.MaChiTietKham)
                .HasConstraintName("FK__CHITIET_H__maChi__375B2DB9");

            entity.HasOne(d => d.MaHoaDonNavigation).WithMany(p => p.ChitietHoadonKhamchuabenhs)
                .HasForeignKey(d => d.MaHoaDon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIET_H__maHoa__36670980");
        });

        modelBuilder.Entity<ChitietHoadonThuoc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CHITIET___3214EC27102B081C");

            entity.ToTable("CHITIET_HOADON_THUOC");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DonGia).HasColumnName("donGia");
            entity.Property(e => e.DonViTinh)
                .HasMaxLength(20)
                .HasColumnName("donViTinh");
            entity.Property(e => e.MaHoaDon).HasColumnName("maHoaDon");
            entity.Property(e => e.MaThuoc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maThuoc");
            entity.Property(e => e.SoLuong).HasColumnName("soLuong");

            entity.HasOne(d => d.MaHoaDonNavigation).WithMany(p => p.ChitietHoadonThuocs)
                .HasForeignKey(d => d.MaHoaDon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIET_H__soLuo__3296789C");

            entity.HasOne(d => d.MaThuocNavigation).WithMany(p => p.ChitietHoadonThuocs)
                .HasForeignKey(d => d.MaThuoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIET_H__maThu__338A9CD5");
        });

        modelBuilder.Entity<ChitietKhambenh>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CHITIET___3214EC27B3D17D9D");

            entity.ToTable("CHITIET_KHAMBENH");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GhiChuBacSiKham).HasColumnName("ghiChuBacSiKham");
            entity.Property(e => e.MaBacSiKham)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maBacSiKham");
            entity.Property(e => e.MaBacSiYeuCau)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maBacSiYeuCau");
            entity.Property(e => e.MaDichVu).HasColumnName("maDichVu");
            entity.Property(e => e.MaKhamBenh).HasColumnName("maKhamBenh");
            entity.Property(e => e.MaKhoa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maKhoa");
            entity.Property(e => e.MaPhongKham)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maPhongKham");
            entity.Property(e => e.ThoiGianKham)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("thoiGianKham");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("0");

            entity.HasOne(d => d.MaBacSiKhamNavigation).WithMany(p => p.ChitietKhambenhMaBacSiKhamNavigations)
                .HasForeignKey(d => d.MaBacSiKham)
                .HasConstraintName("FK__CHITIET_K__maBac__5BCD9859");

            entity.HasOne(d => d.MaBacSiYeuCauNavigation).WithMany(p => p.ChitietKhambenhMaBacSiYeuCauNavigations)
                .HasForeignKey(d => d.MaBacSiYeuCau)
                .HasConstraintName("FK__CHITIET_K__maBac__5AD97420");

            entity.HasOne(d => d.MaDichVuNavigation).WithMany(p => p.ChitietKhambenhs)
                .HasForeignKey(d => d.MaDichVu)
                .HasConstraintName("FK__CHITIET_K__maDic__5708E33C");

            entity.HasOne(d => d.MaKhamBenhNavigation).WithMany(p => p.ChitietKhambenhs)
                .HasForeignKey(d => d.MaKhamBenh)
                .HasConstraintName("FK__CHITIET_K__maKha__5614BF03");

            entity.HasOne(d => d.MaKhoaNavigation).WithMany(p => p.ChitietKhambenhs)
                .HasForeignKey(d => d.MaKhoa)
                .HasConstraintName("FK__CHITIET_K__Trang__59E54FE7");

            entity.HasOne(d => d.MaPhongKhamNavigation).WithMany(p => p.ChitietKhambenhs)
                .HasForeignKey(d => d.MaPhongKham)
                .HasConstraintName("FK__CHITIET_K__maPho__5CC1BC92");
        });

        modelBuilder.Entity<ChitietToathuoc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CHITIET___3214EC2702A6339F");

            entity.ToTable("CHITIET_TOATHUOC");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GhiChu).HasColumnName("ghiChu");
            entity.Property(e => e.LieuDung)
                .HasMaxLength(100)
                .HasColumnName("lieuDung");
            entity.Property(e => e.MaToaThuoc).HasColumnName("maToaThuoc");
            entity.Property(e => e.SoLuong).HasColumnName("soLuong");
            entity.Property(e => e.TenThuoc).HasColumnName("tenThuoc");

            entity.HasOne(d => d.MaToaThuocNavigation).WithMany(p => p.ChitietToathuocs)
                .HasForeignKey(d => d.MaToaThuoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIET_T__maToa__1AF3F935");
        });

        modelBuilder.Entity<DanhsachBenhnhan>(entity =>
        {
            entity.HasKey(e => new { e.MaBenhNhan, e.MaPhongKham, e.NgayKham }).HasName("PK__DANHSACH__CA58DE52ADEEFE39");

            entity.ToTable("DANHSACH_BENHNHAN");

            entity.Property(e => e.MaBenhNhan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maBenhNhan");
            entity.Property(e => e.MaPhongKham)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maPhongKham");
            entity.Property(e => e.NgayKham)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ngayKham");
            entity.Property(e => e.MaKhamBenh).HasColumnName("maKhamBenh");
            entity.Property(e => e.TinhTrang)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("0")
                .HasColumnName("tinhTrang");

            entity.HasOne(d => d.MaBenhNhanNavigation).WithMany(p => p.DanhsachBenhnhans)
                .HasForeignKey(d => d.MaBenhNhan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DANHSACH___maBen__247D636F");

            entity.HasOne(d => d.MaKhamBenhNavigation).WithMany(p => p.DanhsachBenhnhans)
                .HasForeignKey(d => d.MaKhamBenh)
                .HasConstraintName("FK__DANHSACH___maKha__23893F36");

            entity.HasOne(d => d.MaPhongKhamNavigation).WithMany(p => p.DanhsachBenhnhans)
                .HasForeignKey(d => d.MaPhongKham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DANHSACH___maPho__22951AFD");
        });

        modelBuilder.Entity<Dichvu>(entity =>
        {
            entity.HasKey(e => e.MaDichVu).HasName("PK__DICHVU__80F48B0963609D7E");

            entity.ToTable("DICHVU");

            entity.Property(e => e.MaDichVu).HasColumnName("maDichVu");
            entity.Property(e => e.TenDichVu)
                .HasMaxLength(100)
                .HasColumnName("tenDichVu");
            entity.Property(e => e.TypeId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("typeID");
        });

        modelBuilder.Entity<DongiaDichvu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DONGIA_D__3214EC2711AFC1A9");

            entity.ToTable("DONGIA_DICHVU");

            entity.HasIndex(e => new { e.MaDichVu, e.NgayApDung }, "UQ__DONGIA_D__FFA8CB38347EE286").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DonGia).HasColumnName("donGia");
            entity.Property(e => e.MaDichVu).HasColumnName("maDichVu");
            entity.Property(e => e.NgayApDung).HasColumnName("ngayApDung");

            entity.HasOne(d => d.MaDichVuNavigation).WithMany(p => p.DongiaDichvus)
                .HasForeignKey(d => d.MaDichVu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DONGIA_DI__maDic__0C70CFB4");
        });

        modelBuilder.Entity<DongiaThuoc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DONGIA_T__3214EC2777DFF92E");

            entity.ToTable("DONGIA_THUOC");

            entity.HasIndex(e => new { e.MaThuoc, e.NgayApDung }, "UQ__DONGIA_T__560C3A51F8FED441").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DonGia).HasColumnName("donGia");
            entity.Property(e => e.MaThuoc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maThuoc");
            entity.Property(e => e.NgayApDung).HasColumnName("ngayApDung");

            entity.HasOne(d => d.MaThuocNavigation).WithMany(p => p.DongiaThuocs)
                .HasForeignKey(d => d.MaThuoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DONGIA_TH__maThu__10416098");
        });

        modelBuilder.Entity<Hoadon>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon).HasName("PK__HOADON__026B4D9AD52C85F9");

            entity.ToTable("HOADON");

            entity.Property(e => e.MaHoaDon).HasColumnName("maHoaDon");
            entity.Property(e => e.LoaiHoaDon)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("loaiHoaDon");
            entity.Property(e => e.MaKhamBenh).HasColumnName("maKhamBenh");
            entity.Property(e => e.NgayLap)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ngayLap");
            entity.Property(e => e.NhanVienThu)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("nhanVienThu");
            entity.Property(e => e.SoTienNhan)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("soTienNhan");
            entity.Property(e => e.SoTienThoi)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("soTienThoi");
            entity.Property(e => e.ThanhToan)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("0")
                .HasColumnName("thanhToan");

            entity.HasOne(d => d.MaKhamBenhNavigation).WithMany(p => p.Hoadons)
                .HasForeignKey(d => d.MaKhamBenh)
                .HasConstraintName("FK__HOADON__maKhamBe__48EFCE0F");

            entity.HasOne(d => d.NhanVienThuNavigation).WithMany(p => p.Hoadons)
                .HasForeignKey(d => d.NhanVienThu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOADON__nhanVien__49E3F248");
        });

        modelBuilder.Entity<Khambenh>(entity =>
        {
            entity.HasKey(e => e.MaKhamBenh).HasName("PK__KHAMBENH__D347EDAD9E59BB06");

            entity.ToTable("KHAMBENH");

            entity.HasIndex(e => new { e.MaBenhNhan, e.NgayKham }, "UQ__KHAMBENH__435EA95F2F394397").IsUnique();

            entity.Property(e => e.MaKhamBenh).HasColumnName("maKhamBenh");
            entity.Property(e => e.ChanDoanCuoiCung).HasColumnName("chanDoanCuoiCung");
            entity.Property(e => e.MaBacSi)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maBacSi");
            entity.Property(e => e.MaBenhNhan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maBenhNhan");
            entity.Property(e => e.NgayKham)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ngayKham");
            entity.Property(e => e.NgayTaiKham)
                .HasColumnType("datetime")
                .HasColumnName("ngayTaiKham");
            entity.Property(e => e.TrieuChung).HasColumnName("trieuChung");

            entity.HasOne(d => d.MaBacSiNavigation).WithMany(p => p.Khambenhs)
                .HasForeignKey(d => d.MaBacSi)
                .HasConstraintName("FK__KHAMBENH__maBacS__2A6B46EF");

            entity.HasOne(d => d.MaBenhNhanNavigation).WithMany(p => p.Khambenhs)
                .HasForeignKey(d => d.MaBenhNhan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KHAMBENH__maBenh__297722B6");
        });

        modelBuilder.Entity<Khoa>(entity =>
        {
            entity.HasKey(e => e.MaKhoa).HasName("PK__KHOA__C79B8C2251DB5804");

            entity.ToTable("KHOA");

            entity.Property(e => e.MaKhoa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maKhoa");
            entity.Property(e => e.TenKhoa)
                .HasMaxLength(100)
                .HasColumnName("tenKhoa");
        });

        modelBuilder.Entity<LichLamviec>(entity =>
        {
            entity.HasKey(e => e.MaLich).HasName("PK__LICH_LAM__F0B8217B26059AC1");

            entity.ToTable("LICH_LAMVIEC");

            entity.Property(e => e.MaLich).HasColumnName("maLich");
            entity.Property(e => e.GhiChu).HasColumnName("ghiChu");
            entity.Property(e => e.GioBatDau).HasColumnName("gioBatDau");
            entity.Property(e => e.GioKetThuc).HasColumnName("gioKetThuc");
            entity.Property(e => e.MaNhanVien)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maNhanVien");
            entity.Property(e => e.MaPhongKham)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maPhongKham");
            entity.Property(e => e.NgayLam).HasColumnName("ngayLam");

            entity.HasOne(d => d.MaNhanVienNavigation).WithMany(p => p.LichLamviecs)
                .HasForeignKey(d => d.MaNhanVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LICH_LAMV__maNha__0504B816");

            entity.HasOne(d => d.MaPhongKhamNavigation).WithMany(p => p.LichLamviecs)
                .HasForeignKey(d => d.MaPhongKham)
                .HasConstraintName("FK__LICH_LAMV__maPho__05F8DC4F");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("PK__NHANVIEN__BDDEF20D985ADD1A");

            entity.ToTable("NHANVIEN");

            entity.Property(e => e.MaNhanVien)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maNhanVien");
            entity.Property(e => e.ChucVu)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("chucVu");
            entity.Property(e => e.MaPhongBan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maPhongBan");

            entity.HasOne(d => d.MaNhanVienNavigation).WithOne(p => p.Nhanvien)
                .HasForeignKey<Nhanvien>(d => d.MaNhanVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NHANVIEN__maNhan__4CA06362");

            entity.HasOne(d => d.MaPhongBanNavigation).WithMany(p => p.Nhanviens)
                .HasForeignKey(d => d.MaPhongBan)
                .HasConstraintName("FK__NHANVIEN__maPhon__4BAC3F29");
        });

        modelBuilder.Entity<Phongban>(entity =>
        {
            entity.HasKey(e => e.MaPhongBan).HasName("PK__PHONGBAN__3A946B08EE18610A");

            entity.ToTable("PHONGBAN");

            entity.Property(e => e.MaPhongBan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maPhongBan");
            entity.Property(e => e.TenPhongBan)
                .HasMaxLength(100)
                .HasColumnName("tenPhongBan");
        });

        modelBuilder.Entity<Phongkham>(entity =>
        {
            entity.HasKey(e => e.MaPhongKham).HasName("PK__PHONGKHA__62C17CF71DB610CE");

            entity.ToTable("PHONGKHAM");

            entity.Property(e => e.MaPhongKham)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maPhongKham");
            entity.Property(e => e.MaDichVu).HasColumnName("maDichVu");
            entity.Property(e => e.MaKhoa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maKhoa");
            entity.Property(e => e.TenPhongKham)
                .HasMaxLength(100)
                .HasColumnName("tenPhongKham");

            entity.HasOne(d => d.MaDichVuNavigation).WithMany(p => p.Phongkhams)
                .HasForeignKey(d => d.MaDichVu)
                .HasConstraintName("FK_PhongKham_DichVu");

            entity.HasOne(d => d.MaKhoaNavigation).WithMany(p => p.Phongkhams)
                .HasForeignKey(d => d.MaKhoa)
                .HasConstraintName("FK__PHONGKHAM__maKho__48CFD27E");
        });

        modelBuilder.Entity<ThongtinCanhan>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("PK__THONGTIN__BDDEF20D6D9AFCA6");

            entity.ToTable("THONGTIN_CANHAN");

            entity.Property(e => e.MaNhanVien)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maNhanVien");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(255)
                .HasColumnName("diaChi");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.HoTen)
                .HasMaxLength(100)
                .HasColumnName("hoTen");
            entity.Property(e => e.LuongCoBan).HasColumnName("luongCoBan");
            entity.Property(e => e.NgaySinh).HasColumnName("ngaySinh");
            entity.Property(e => e.PhuCap).HasColumnName("phuCap");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("soDienThoai");
        });

        modelBuilder.Entity<Thuoc>(entity =>
        {
            entity.HasKey(e => e.MaThuoc).HasName("PK__THUOC__29507A60375A4FCD");

            entity.ToTable("THUOC");

            entity.Property(e => e.MaThuoc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maThuoc");
            entity.Property(e => e.DonViTinh)
                .HasMaxLength(20)
                .HasColumnName("donViTinh");
            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ngayCapNhat");
            entity.Property(e => e.SoLuongTon).HasColumnName("soLuongTon");
            entity.Property(e => e.TenThuoc)
                .HasMaxLength(100)
                .HasColumnName("tenThuoc");
            entity.Property(e => e.ThongTin).HasColumnName("thongTin");
        });

        modelBuilder.Entity<Toathuoc>(entity =>
        {
            entity.HasKey(e => e.MaToaThuoc).HasName("PK__TOATHUOC__571828C71AB78BAA");

            entity.ToTable("TOATHUOC");

            entity.Property(e => e.MaToaThuoc).HasColumnName("maToaThuoc");
            entity.Property(e => e.MaKhamBenh).HasColumnName("maKhamBenh");
            entity.Property(e => e.NgayKe)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ngayKe");

            entity.HasOne(d => d.MaKhamBenhNavigation).WithMany(p => p.Toathuocs)
                .HasForeignKey(d => d.MaKhamBenh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TOATHUOC__maKham__1352D76D");
        });

        modelBuilder.Entity<ViewBenhNhanTrongNgay>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewBenhNhanTrongNgay");

            entity.Property(e => e.CanNang).HasColumnName("canNang");
            entity.Property(e => e.ChieuCao).HasColumnName("chieuCao");
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("gioiTinh");
            entity.Property(e => e.HoTen)
                .HasMaxLength(100)
                .HasColumnName("hoTen");
            entity.Property(e => e.MaBenhNhan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maBenhNhan");
            entity.Property(e => e.MaKhamBenh).HasColumnName("maKhamBenh");
            entity.Property(e => e.MaPhongKham)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maPhongKham");
            entity.Property(e => e.NamSinh).HasColumnName("namSinh");
            entity.Property(e => e.Stt).HasColumnName("STT");
            entity.Property(e => e.TinhTrang)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tinhTrang");
        });

        modelBuilder.Entity<ViewChiTietHoaDonTaiVu>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_ChiTietHoaDonTaiVu");

            entity.Property(e => e.DonGia)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("donGia");
            entity.Property(e => e.MaChiTietKham).HasColumnName("maChiTietKham");
            entity.Property(e => e.MaDichVu).HasColumnName("maDichVu");
            entity.Property(e => e.MaHoaDon).HasColumnName("maHoaDon");
            entity.Property(e => e.MaKhamBenh).HasColumnName("maKhamBenh");
            entity.Property(e => e.NgayLap)
                .HasColumnType("datetime")
                .HasColumnName("ngayLap");
            entity.Property(e => e.TenBenhNhan)
                .HasMaxLength(100)
                .HasColumnName("tenBenhNhan");
            entity.Property(e => e.TenDichVu)
                .HasMaxLength(100)
                .HasColumnName("tenDichVu");
        });

        modelBuilder.Entity<ViewDieuPhoi>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewDieuPhoi");

            entity.Property(e => e.MaDichVu).HasColumnName("maDichVu");
            entity.Property(e => e.MaKhamBenh).HasColumnName("maKhamBenh");
            entity.Property(e => e.MaKhoa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maKhoa");
            entity.Property(e => e.Stt).HasColumnName("STT");
            entity.Property(e => e.TenBacSi)
                .HasMaxLength(100)
                .HasColumnName("tenBacSi");
            entity.Property(e => e.TenBenhNhan)
                .HasMaxLength(100)
                .HasColumnName("tenBenhNhan");
            entity.Property(e => e.TenDichVu)
                .HasMaxLength(100)
                .HasColumnName("tenDichVu");
            entity.Property(e => e.TenKhoa)
                .HasMaxLength(100)
                .HasColumnName("tenKhoa");
        });

        modelBuilder.Entity<ViewDonGiaDichVu>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewDonGiaDichVu");

            entity.Property(e => e.DonGia).HasColumnName("donGia");
            entity.Property(e => e.MaDichVu).HasColumnName("maDichVu");
            entity.Property(e => e.NgayApDung).HasColumnName("ngayApDung");
            entity.Property(e => e.TenDichVu)
                .HasMaxLength(100)
                .HasColumnName("tenDichVu");
        });

        modelBuilder.Entity<ViewKhoa>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewKhoa");

            entity.Property(e => e.MaKhoa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maKhoa");
            entity.Property(e => e.TenKhoa)
                .HasMaxLength(100)
                .HasColumnName("tenKhoa");
        });

        modelBuilder.Entity<ViewPhongKhamDp>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewPhongKhamDP");

            entity.Property(e => e.MaDichVu).HasColumnName("maDichVu");
            entity.Property(e => e.MaKhoa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maKhoa");
            entity.Property(e => e.MaPhongKham)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maPhongKham");
            entity.Property(e => e.TenPhongKham)
                .HasMaxLength(100)
                .HasColumnName("tenPhongKham");
        });

        modelBuilder.Entity<ViewTaiVu>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewTaiVu");

            entity.Property(e => e.HoTenBenhNhan)
                .HasMaxLength(100)
                .HasColumnName("hoTenBenhNhan");
            entity.Property(e => e.MaBenhNhan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maBenhNhan");
            entity.Property(e => e.MaChiTietKham).HasColumnName("maChiTietKham");
            entity.Property(e => e.MaDichVu).HasColumnName("maDichVu");
            entity.Property(e => e.MaKhamBenh).HasColumnName("maKhamBenh");
            entity.Property(e => e.Stt).HasColumnName("STT");
            entity.Property(e => e.TenDichVu).HasMaxLength(100);
        });

        modelBuilder.Entity<ViewThongTinNhanVien>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewThongTinNhanVien");

            entity.Property(e => e.DiaChi)
                .HasMaxLength(255)
                .HasColumnName("diaChi");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.HoTen)
                .HasMaxLength(100)
                .HasColumnName("hoTen");
            entity.Property(e => e.LuongCoBan).HasColumnName("luongCoBan");
            entity.Property(e => e.MaNhanVien)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maNhanVien");
            entity.Property(e => e.NgaySinh).HasColumnName("ngaySinh");
            entity.Property(e => e.PhuCap).HasColumnName("phuCap");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("soDienThoai");
        });

        modelBuilder.Entity<ViewXetNghiemB>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewXetNghiemBS");

            entity.Property(e => e.MaDichVu)
                .ValueGeneratedOnAdd()
                .HasColumnName("maDichVu");
            entity.Property(e => e.TenDichVu)
                .HasMaxLength(100)
                .HasColumnName("tenDichVu");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

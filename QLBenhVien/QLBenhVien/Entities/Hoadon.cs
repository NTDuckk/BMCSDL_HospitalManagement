using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class Hoadon
{
    public int MaHoaDon { get; set; }

    public DateTime? NgayLap { get; set; }

    public decimal? SoTienNhan { get; set; }

    public decimal? SoTienThoi { get; set; }

    public string? LoaiHoaDon { get; set; }

    public int? MaKhamBenh { get; set; }

    public string? ThanhToan { get; set; }

    public string NhanVienThu { get; set; } = null!;

    public virtual ICollection<ChitietHoadonKhamchuabenh> ChitietHoadonKhamchuabenhs { get; set; } = new List<ChitietHoadonKhamchuabenh>();

    public virtual ICollection<ChitietHoadonThuoc> ChitietHoadonThuocs { get; set; } = new List<ChitietHoadonThuoc>();

    public virtual Khambenh? MaKhamBenhNavigation { get; set; }

    public virtual Nhanvien NhanVienThuNavigation { get; set; } = null!;
}

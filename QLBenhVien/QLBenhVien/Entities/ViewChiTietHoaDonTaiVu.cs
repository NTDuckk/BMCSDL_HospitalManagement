using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class ViewChiTietHoaDonTaiVu
{
    public int MaHoaDon { get; set; }

    public int? MaKhamBenh { get; set; }

    public string TenBenhNhan { get; set; } = null!;

    public int? MaDichVu { get; set; }

    public string TenDichVu { get; set; } = null!;

    public decimal? DonGia { get; set; }

    public DateTime? NgayLap { get; set; }

    public int? MaChiTietKham { get; set; }
}

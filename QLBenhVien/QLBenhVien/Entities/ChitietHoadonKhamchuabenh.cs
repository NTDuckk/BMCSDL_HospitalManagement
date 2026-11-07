using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class ChitietHoadonKhamchuabenh
{
    public int Id { get; set; }

    public int MaHoaDon { get; set; }

    public int? MaChiTietKham { get; set; }

    public byte[]? DonGia { get; set; }

    public virtual ChitietKhambenh? MaChiTietKhamNavigation { get; set; }

    public virtual Hoadon MaHoaDonNavigation { get; set; } = null!;
}

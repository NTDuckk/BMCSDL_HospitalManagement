using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class ChitietHoadonThuoc
{
    public int Id { get; set; }

    public int MaHoaDon { get; set; }

    public string MaThuoc { get; set; } = null!;

    public byte[]? DonGia { get; set; }

    public string? DonViTinh { get; set; }

    public int SoLuong { get; set; }

    public virtual Hoadon MaHoaDonNavigation { get; set; } = null!;

    public virtual Thuoc MaThuocNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class Nhanvien
{
    public string MaNhanVien { get; set; } = null!;

    public string? MaPhongBan { get; set; }

    public string? ChucVu { get; set; }

    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();

    public virtual ThongtinCanhan MaNhanVienNavigation { get; set; } = null!;

    public virtual Phongban? MaPhongBanNavigation { get; set; }
}

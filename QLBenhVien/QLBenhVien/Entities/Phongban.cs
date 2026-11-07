using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class Phongban
{
    public string MaPhongBan { get; set; } = null!;

    public string TenPhongBan { get; set; } = null!;

    public virtual ICollection<Nhanvien> Nhanviens { get; set; } = new List<Nhanvien>();
}

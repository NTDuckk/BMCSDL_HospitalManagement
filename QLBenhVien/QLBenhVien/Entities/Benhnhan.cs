using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class Benhnhan
{
    public string MaBenhNhan { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public int? NamSinh { get; set; }

    public string? DiaChi { get; set; }

    public string? SoDienThoai { get; set; }

    public DateTime? NgayTao { get; set; }

    public string? GioiTinh { get; set; }

    public double? ChieuCao { get; set; }

    public double? CanNang { get; set; }

    public virtual ICollection<DanhsachBenhnhan> DanhsachBenhnhans { get; set; } = new List<DanhsachBenhnhan>();

    public virtual ICollection<Khambenh> Khambenhs { get; set; } = new List<Khambenh>();
}

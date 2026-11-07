using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class ViewBenhNhanTrongNgay
{
    public long? Stt { get; set; }

    public string MaBenhNhan { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public int? NamSinh { get; set; }

    public double? ChieuCao { get; set; }

    public double? CanNang { get; set; }

    public string? GioiTinh { get; set; }

    public int MaKhamBenh { get; set; }

    public string MaPhongKham { get; set; } = null!;

    public string? TinhTrang { get; set; }
}

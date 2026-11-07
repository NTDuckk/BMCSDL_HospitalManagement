using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class ViewBenhNhanTheoPhong
{
    public string MaPhongKham { get; set; } = null!;

    public string TenBenhNhan { get; set; } = null!;

    public int? MaDichVu { get; set; }

    public string? TinhTrang { get; set; }
}

using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class ViewPhongKhamDp
{
    public string MaPhongKham { get; set; } = null!;

    public string? TenPhongKham { get; set; }

    public string? MaKhoa { get; set; }

    public int? MaDichVu { get; set; }
}

using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class ViewThuTucKcb
{
    public int Stt { get; set; }

    public string? HoTenBn { get; set; }

    public string? HoTenBs { get; set; }

    public string? TenDichVu { get; set; }

    public string? TenKhoa { get; set; }

    public string TinhTrang { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class Chamcong
{
    public int MaChamCong { get; set; }

    public string MaNhanVien { get; set; } = null!;

    public DateOnly NgayChamCong { get; set; }

    public TimeOnly? GioVao { get; set; }

    public TimeOnly? GioRa { get; set; }

    public string? GhiChu { get; set; }

    public virtual ThongtinCanhan MaNhanVienNavigation { get; set; } = null!;
}

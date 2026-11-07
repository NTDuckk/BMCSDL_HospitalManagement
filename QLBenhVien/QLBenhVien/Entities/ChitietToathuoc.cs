using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class ChitietToathuoc
{
    public int Id { get; set; }

    public int MaToaThuoc { get; set; }

    public byte[]? TenThuoc { get; set; }

    public int SoLuong { get; set; }

    public string? LieuDung { get; set; }

    public byte[]? GhiChu { get; set; }

    public virtual Toathuoc MaToaThuocNavigation { get; set; } = null!;
}

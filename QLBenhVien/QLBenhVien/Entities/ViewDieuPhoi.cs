using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class ViewDieuPhoi
{
    public long? Stt { get; set; }

    public string TenBenhNhan { get; set; } = null!;

    public string? TenBacSi { get; set; }

    public int? MaDichVu { get; set; }

    public string? TenDichVu { get; set; }

    public string? MaKhoa { get; set; }

    public string? TenKhoa { get; set; }

    public int MaKhamBenh { get; set; }
}
